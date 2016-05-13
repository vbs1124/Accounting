using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using Vserv.Accounting.Core.Models;
using Vserv.Accounting.Data.Entity;

namespace Vserv.Accounting.Core.Services
{
    public class UsersService : IUsersService
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IConfigService _configService;
        private readonly IEmailService _emailService;

        public UsersService(IDatabaseContext databaseContext, IConfigService configService, IEmailService emailService)
        {
            this._databaseContext = databaseContext;
            this._configService = configService;
            this._emailService = emailService;
        }

        UserProfile IUsersService.GetUserProfile(string userName)
        {
            return this._databaseContext.UserProfiles.FirstOrDefault(x => x.UserName.Equals(userName));
        }

        UserProfile IUsersService.GetUserProfile(int userId)
        {
            return this._databaseContext.UserProfiles.FirstOrDefault(x => x.UserId.Equals(userId));
        }

        void IUsersService.Save(UserProfile userProfile)
        {
            try
            {
                if (userProfile.UserId == 0)
                {
                    this._databaseContext.Add(userProfile);
                }
                this._databaseContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        OAuthMembership IUsersService.GetOAuthMembership(string provider, string providerUserId)
        {
            return this._databaseContext.OAuthMemberships.FirstOrDefault(x => x.Provider.Equals(provider) && x.ProviderUserId.Equals(providerUserId));
        }

        void IUsersService.SaveOAuthMembership(string provider, string providerUserId, int userId)
        {
            var oAuthMembership = this._databaseContext.OAuthMemberships.FirstOrDefault(x => x.Provider.Equals(provider) && x.ProviderUserId.Equals(providerUserId));
            if (oAuthMembership == null)
            {
                oAuthMembership = new OAuthMembership { Provider = provider, ProviderUserId = providerUserId };
                this._databaseContext.Add(oAuthMembership);
            }
            oAuthMembership.UserId = userId;
            this._databaseContext.SaveChanges();
        }

        Vserv.Accounting.Data.Entity.Membership IUsersService.GetMembership(int userId)
        {
            return this._databaseContext.Memberships.FirstOrDefault(x => x.UserId == userId);
        }

        Vserv.Accounting.Data.Entity.Membership IUsersService.GetMembershipByConfirmToken(string token, bool withUserProfile)
        {
            var membership = this._databaseContext.Memberships.FirstOrDefault(x => x.ConfirmationToken.Equals(token.ToLower()));
            if (membership != null && withUserProfile)
            {
                membership.UserProfile = this._databaseContext.UserProfiles.First(x => x.UserId == membership.UserId);
            }
            return membership;
        }

        Vserv.Accounting.Data.Entity.Membership IUsersService.GetMembershipByVerificationToken(string token, bool withUserProfile)
        {
            var membership = this._databaseContext.Memberships.FirstOrDefault(x => x.PasswordVerificationToken.Equals(token.ToLower()));
            if (membership != null && withUserProfile)
            {
                membership.UserProfile = this._databaseContext.UserProfiles.First(x => x.UserId == membership.UserId);
            }
            return membership;
        }

        void IUsersService.Save(Vserv.Accounting.Data.Entity.Membership membership, bool add)
        {
            try
            {
                if (add)
                {
                    this._databaseContext.Add(membership);
                }
                this._databaseContext.SaveChanges();
            }
            catch
            {

                throw;
            }
        }

        // to do: transfer it to service
        void IUsersService.SendAccountActivationMail(string email)
        {
            var userProfile = (this as IUsersService).GetUserProfile(email);
            if (userProfile == null)
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.ProviderError);
            }

            var membership = (this as IUsersService).GetMembership(userProfile.UserId);
            if (membership == null)
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.ProviderError);
            }

            var configValues = this._configService.GetValues(new ConfigName[] { ConfigName.WebsiteUrlName, ConfigName.WebsiteTitle, ConfigName.WebsiteUrl });
            var viewData = new ViewDataDictionary { Model = userProfile };
            viewData.Add("Membership", membership);
            this._emailService.SendEmail(
                new SendEmailModel
                {
                    EmailAddress = email,
                    Subject = configValues[ConfigName.WebsiteUrlName.ToString()] + ": Confirm your registration",
                    WebsiteUrlName = configValues[ConfigName.WebsiteUrlName.ToString()],
                    WebsiteTitle = configValues[ConfigName.WebsiteTitle.ToString()],
                    WebsiteURL = configValues[ConfigName.WebsiteUrl.ToString()]
                },
                "ConfirmRegistration",
                viewData
            );
        }

        string[] IUsersService.GetRoles(string userName)
        {
            var userProfile = this._databaseContext.UserProfiles.FirstOrDefault(x => x.UserName.Equals(userName));
            if (userProfile != null)
            {
                return userProfile.Roles.Select(x => x.RoleName).ToArray();
            }
            return new string[] { };
        }

        void IUsersService.SentChangePasswordEmail(string email)
        {
            var userProfile = (this as IUsersService).GetUserProfile(email);
            if (userProfile == null)
            {
                throw new MembershipCreateUserException("User not found.");
            }

            var membership = (this as IUsersService).GetMembership(userProfile.UserId);
            if (membership == null)
            {
                throw new MembershipCreateUserException("User not found.");
            }

            membership.PasswordVerificationToken = Guid.NewGuid().ToString();
            this._databaseContext.SaveChanges();

            var configValues = this._configService.GetValues(new ConfigName[] { ConfigName.WebsiteUrlName, ConfigName.WebsiteTitle, ConfigName.WebsiteUrl });
            var viewData = new ViewDataDictionary { Model = membership };
            _emailService.SendEmail(
                new SendEmailModel
                {
                    EmailAddress = email,
                    Subject = configValues[ConfigName.WebsiteUrlName.ToString()] + ": Change password.",
                    WebsiteUrlName = configValues[ConfigName.WebsiteUrlName.ToString()],
                    WebsiteTitle = configValues[ConfigName.WebsiteTitle.ToString()],
                    WebsiteURL = configValues[ConfigName.WebsiteUrl.ToString()]
                },
                "ChangePassword",
                viewData
            );
        }

        private string salt = "HJIO6589";
        string IUsersService.GetHash(string text)
        {
            var buffer = Encoding.UTF8.GetBytes(String.Concat(text, salt));
            var cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
            return hash;
        }

        void IUsersService.ChangePassword(Vserv.Accounting.Data.Entity.Membership membership, string newPassword)
        {
            if (membership == null)
            {
                throw new Exception("User not found.");
            }

            membership.PasswordVerificationToken = null;
            membership.PasswordSalt = (this as IUsersService).GetHash(newPassword);

            this._databaseContext.SaveChanges();
        }
    }
}
