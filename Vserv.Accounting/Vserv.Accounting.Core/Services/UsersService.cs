using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using Vserv.Accounting.Core.Models;
using Vserv.Accounting.Data.Entity;
using Membership = Vserv.Accounting.Data.Entity.Membership;

namespace Vserv.Accounting.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Vserv.Accounting.Core.Services.IUsersService" />
    public class UsersService : IUsersService
    {
        /// <summary>
        /// The _database context
        /// </summary>
        private readonly IDatabaseContext _databaseContext;
        /// <summary>
        /// The _config service
        /// </summary>
        private readonly IConfigService _configService;
        /// <summary>
        /// The _email service
        /// </summary>
        private readonly IEmailService _emailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersService"/> class.
        /// </summary>
        /// <param name="databaseContext">The database context.</param>
        /// <param name="configService">The configuration service.</param>
        /// <param name="emailService">The email service.</param>
        public UsersService(IDatabaseContext databaseContext, IConfigService configService, IEmailService emailService)
        {
            _databaseContext = databaseContext;
            _configService = configService;
            _emailService = emailService;
        }

        /// <summary>
        /// Gets the user profile.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        UserProfile IUsersService.GetUserProfile(string userName)
        {
            return _databaseContext.UserProfiles.FirstOrDefault(x => x.UserName.Equals(userName));
        }

        /// <summary>
        /// Gets the user profile.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        UserProfile IUsersService.GetUserProfile(int userId)
        {
            return _databaseContext.UserProfiles.FirstOrDefault(x => x.UserId.Equals(userId));
        }

        /// <summary>
        /// Saves the specified user profile.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        void IUsersService.Save(UserProfile userProfile)
        {
            if (userProfile.UserId == 0)
            {
                _databaseContext.Add(userProfile);
            }
            _databaseContext.SaveChanges();
        }

        /// <summary>
        /// Gets the o authentication membership.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="providerUserId">The provider user identifier.</param>
        /// <returns></returns>
        OAuthMembership IUsersService.GetOAuthMembership(string provider, string providerUserId)
        {
            return _databaseContext.OAuthMemberships.FirstOrDefault(x => x.Provider.Equals(provider) && x.ProviderUserId.Equals(providerUserId));
        }

        /// <summary>
        /// Saves the o authentication membership.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="providerUserId">The provider user identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void IUsersService.SaveOAuthMembership(string provider, string providerUserId, int userId)
        {
            var oAuthMembership = _databaseContext.OAuthMemberships.FirstOrDefault(x => x.Provider.Equals(provider) && x.ProviderUserId.Equals(providerUserId));
            if (oAuthMembership == null)
            {
                oAuthMembership = new OAuthMembership { Provider = provider, ProviderUserId = providerUserId };
                _databaseContext.Add(oAuthMembership);
            }
            oAuthMembership.UserId = userId;
            _databaseContext.SaveChanges();
        }

        /// <summary>
        /// Gets the membership.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Membership IUsersService.GetMembership(int userId)
        {
            return _databaseContext.Memberships.FirstOrDefault(x => x.UserId == userId);
        }

        /// <summary>
        /// Gets the membership by confirm token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="withUserProfile">if set to <c>true</c> [with user profile].</param>
        /// <returns></returns>
        Membership IUsersService.GetMembershipByConfirmToken(string token, bool withUserProfile)
        {
            var membership = _databaseContext.Memberships.FirstOrDefault(x => x.ConfirmationToken.Equals(token.ToLower()));
            if (membership != null && withUserProfile)
            {
                membership.UserProfile = _databaseContext.UserProfiles.First(x => x.UserId == membership.UserId);
            }
            return membership;
        }

        /// <summary>
        /// Gets the membership by verification token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="withUserProfile">if set to <c>true</c> [with user profile].</param>
        /// <returns></returns>
        Membership IUsersService.GetMembershipByVerificationToken(string token, bool withUserProfile)
        {
            var membership = _databaseContext.Memberships.FirstOrDefault(x => x.PasswordVerificationToken.Equals(token.ToLower()));
            if (membership != null && withUserProfile)
            {
                membership.UserProfile = _databaseContext.UserProfiles.First(x => x.UserId == membership.UserId);
            }
            return membership;
        }

        /// <summary>
        /// Saves the specified membership.
        /// </summary>
        /// <param name="membership">The membership.</param>
        /// <param name="add">if set to <c>true</c> [add].</param>
        void IUsersService.Save(Membership membership, bool add)
        {
            if (add)
            {
                _databaseContext.Add(membership);
            }
            _databaseContext.SaveChanges();
        }

        // to do: transfer it to service
        /// <summary>
        /// Sends the account activation mail.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <exception cref="System.Web.Security.MembershipCreateUserException">
        /// </exception>
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

            var configValues = _configService.GetValues(new[] { ConfigName.WebsiteUrlName, ConfigName.WebsiteTitle, ConfigName.WebsiteUrl });
            var viewData = new ViewDataDictionary { Model = userProfile };
            viewData.Add("Membership", membership);
            _emailService.SendEmail(
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

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        string[] IUsersService.GetRoles(string userName)
        {
            var userProfile = _databaseContext.UserProfiles.FirstOrDefault(x => x.UserName.Equals(userName));
            if (userProfile != null)
            {
                return userProfile.Roles.Select(x => x.RoleName).ToArray();
            }
            return new string[] { };
        }

        /// <summary>
        /// Sents the change password email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <exception cref="System.Web.Security.MembershipCreateUserException">
        /// User not found.
        /// or
        /// User not found.
        /// </exception>
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
            _databaseContext.SaveChanges();

            var configValues = _configService.GetValues(new[] { ConfigName.WebsiteUrlName, ConfigName.WebsiteTitle, ConfigName.WebsiteUrl });
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

        /// <summary>
        /// The salt
        /// </summary>
        private const string Salt = "HJIO6589";

        /// <summary>
        /// Gets the hash.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        string IUsersService.GetHash(string text)
        {
            var buffer = Encoding.UTF8.GetBytes(String.Concat(text, Salt));
            var cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
            return hash;
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="membership">The membership.</param>
        /// <param name="newPassword">The new password.</param>
        /// <exception cref="Exception">User not found.</exception>
        void IUsersService.ChangePassword(Membership membership, string newPassword)
        {
            if (membership == null)
            {
                throw new Exception("User not found.");
            }

            membership.PasswordVerificationToken = null;
            membership.PasswordSalt = (this as IUsersService).GetHash(newPassword);

            _databaseContext.SaveChanges();
        }


        void IUsersService.Save(List<UserSecurityQuestion> userSecurityQuestions, string userName, bool add)
        {
            if (add)
            {
                // Get UserId by username..
                userName = userName.Trim().ToLower();
                UserProfile userProfile = _databaseContext.UserProfiles.FirstOrDefault(user => user.LoweredUserName == userName);
                if (userProfile != null)
                {
                    int userId = userProfile.UserId;

                    foreach (var item in userSecurityQuestions)
                    {
                        item.UserId = userId;
                        _databaseContext.Add(item);
                    }
                }
            }

            _databaseContext.SaveChanges();
        }
    }
}
