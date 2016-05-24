using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Vserv.Accounting.Core.Services;
using Vserv.Accounting.Data.Entity;
using WebMatrix.WebData;
using Vserv.Common.Extensions;

namespace Vserv.Accounting.Web.Code.Membership
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="WebMatrix.WebData.ExtendedMembershipProvider" />
    public class VservMembershipProvider : ExtendedMembershipProvider
    {
        /// <summary>
        /// The users service
        /// </summary>
        private readonly IUsersService usersService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VservMembershipProvider"/> class.
        /// </summary>
        public VservMembershipProvider()
        {
            this.usersService = (IUsersService)MvcApplication.Container.Resolve(typeof(IUsersService), null);
        }

        #region MembershipProvider

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Processes a request to update the password for a membership user.
        /// </summary>
        /// <param name="username">The user to update the password for.</param>
        /// <param name="oldPassword">The current password for the specified user.</param>
        /// <param name="newPassword">The new password for the specified user.</param>
        /// <returns>
        /// true if the password was updated successfully; otherwise, false.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Processes a request to update the password question and answer for a membership user.
        /// </summary>
        /// <param name="username">The user to change the password question and answer for.</param>
        /// <param name="password">The password for the specified user.</param>
        /// <param name="newPasswordQuestion">The new password question for the specified user.</param>
        /// <param name="newPasswordAnswer">The new password answer for the specified user.</param>
        /// <returns>
        /// true if the password question and answer are updated successfully; otherwise, false.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a new membership user to the data source.
        /// </summary>
        /// <param name="username">The user name for the new user.</param>
        /// <param name="password">The password for the new user.</param>
        /// <param name="email">The e-mail address for the new user.</param>
        /// <param name="passwordQuestion">The password question for the new user.</param>
        /// <param name="passwordAnswer">The password answer for the new user</param>
        /// <param name="isApproved">Whether or not the new user is approved to be validated.</param>
        /// <param name="providerUserKey">The unique identifier from the membership data source for the user.</param>
        /// <param name="status">A <see cref="T:System.Web.Security.MembershipCreateStatus" /> enumeration value indicating whether the user was created successfully.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser" /> object populated with the information for the newly created user.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a user from the membership data source.
        /// </summary>
        /// <param name="username">The name of the user to delete.</param>
        /// <param name="deleteAllRelatedData">true to delete data related to the user from the database; false to leave data related to the user in the database.</param>
        /// <returns>
        /// true if the user was successfully deleted; otherwise, false.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Indicates whether the membership provider is configured to allow users to reset their passwords.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Indicates whether the membership provider is configured to allow users to retrieve their passwords.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets a collection of membership users where the e-mail address contains the specified e-mail address to match.
        /// </summary>
        /// <param name="emailToMatch">The e-mail address to search for.</param>
        /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched users.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection" /> collection that contains a page of <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> objects beginning at the page specified by <paramref name="pageIndex" />.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override System.Web.Security.MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a collection of membership users where the user name contains the specified user name to match.
        /// </summary>
        /// <param name="usernameToMatch">The user name to search for.</param>
        /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched users.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection" /> collection that contains a page of <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> objects beginning at the page specified by <paramref name="pageIndex" />.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override System.Web.Security.MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a collection of all the users in the data source in pages of data.
        /// </summary>
        /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched users.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection" /> collection that contains a page of <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> objects beginning at the page specified by <paramref name="pageIndex" />.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override System.Web.Security.MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the number of users currently accessing the application.
        /// </summary>
        /// <returns>
        /// The number of users currently accessing the application.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the password for the specified user name from the data source.
        /// </summary>
        /// <param name="username">The user to retrieve the password for.</param>
        /// <param name="answer">The password answer for the user.</param>
        /// <returns>
        /// The password for the specified user name.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets information from the data source for a user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <param name="username">The name of the user to get information for.</param>
        /// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser" /> object populated with the specified user's information from the data source.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets user information from the data source based on the unique identifier for the membership user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <param name="providerUserKey">The unique identifier for the membership user to get information for.</param>
        /// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser" /> object populated with the specified user's information from the data source.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the user name associated with the specified e-mail address.
        /// </summary>
        /// <param name="email">The e-mail address to search for.</param>
        /// <returns>
        /// The user name associated with the specified e-mail address. If no match is found, return null.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the minimum number of special characters that must be present in a valid password.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the minimum length required for a password.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets a value indicating the format for storing passwords in the membership data store.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override System.Web.Security.MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the regular expression used to evaluate a password.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require the user to answer a password question for password reset and retrieval.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require a unique e-mail address for each user name.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Resets a user's password to a new, automatically generated password.
        /// </summary>
        /// <param name="username">The user to reset the password for.</param>
        /// <param name="answer">The password answer for the specified user.</param>
        /// <returns>
        /// The new password for the specified user.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears a lock so that the membership user can be validated.
        /// </summary>
        /// <param name="userName">The membership user whose lock status you want to clear.</param>
        /// <returns>
        /// true if the membership user was successfully unlocked; otherwise, false.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates information about a user in the data source.
        /// </summary>
        /// <param name="user">A <see cref="T:System.Web.Security.MembershipUser" /> object that represents the user to update and the updated information for the user.</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void UpdateUser(System.Web.Security.MembershipUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verifies that the specified user name and password exist in the data source.
        /// </summary>
        /// <param name="username">The name of the user to validate.</param>
        /// <param name="password">The password for the specified user.</param>
        /// <returns>
        /// true if the specified username and password are valid; otherwise, false.
        /// </returns>
        public override bool ValidateUser(string username, string password)
        {
            var userProfile = this.usersService.GetUserProfile(username);
            if (userProfile == null)
            {
                return false;
            }
            var membership = this.usersService.GetMembership(userProfile.UserId);
            if (membership == null)
            {
                return false;
            }
            if (!membership.IsConfirmed)
            {
                return false;
            }
            if (membership.PasswordSalt == this.usersService.GetHash(password))
            {
                return true;
            }
            // first once time we can validate through membership ConfirmationToken, 
            // to be logged in immediately after confirmation
            if (membership.ConfirmationToken != null)
            {
                if (membership.ConfirmationToken == password)
                {
                    membership.ConfirmationToken = null;
                    this.usersService.Save(membership, add: false);
                    return true;
                }
            }
            return false;
        }

        #endregion MembershipProvider

        #region ExtendedMembershipProvider

        /// <summary>
        /// Activates a pending membership account.
        /// </summary>
        /// <param name="accountConfirmationToken">A confirmation token to pass to the authentication provider.</param>
        /// <returns>
        /// true if the account is confirmed; otherwise, false.
        /// </returns>
        /// <exception cref="Exception">
        /// Activation code is incorrect.
        /// or
        /// Your account is already activated.
        /// </exception>
        public override bool ConfirmAccount(string accountConfirmationToken)
        {
            var membership = this.usersService.GetMembershipByConfirmToken(accountConfirmationToken, withUserProfile: false);
            if (membership == null)
            {
                throw new Exception("Activation code is incorrect.");
            }
            if (membership.IsConfirmed)
            {
                throw new Exception("Your account is already activated.");
            }
            membership.IsConfirmed = true;
            this.usersService.Save(membership, add: false);
            return true;
        }

        /// <summary>
        /// Activates a pending membership account for the specified user.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="accountConfirmationToken">A confirmation token to pass to the authentication provider.</param>
        /// <returns>
        /// true if the account is confirmed; otherwise, false.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool ConfirmAccount(string userName, string accountConfirmationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, creates a new user account using the specified user name and password, optionally requiring that the new account must be confirmed before the account is available for use.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        /// <param name="requireConfirmationToken">(Optional) true to specify that the account must be confirmed; otherwise, false. The default is false.</param>
        /// <returns>
        /// A token that can be sent to the user to confirm the account.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override string CreateAccount(string userName, string password, bool requireConfirmationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, creates a new user profile and a new membership account.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        /// <param name="requireConfirmation">(Optional) true to specify that the user account must be confirmed; otherwise, false. The default is false.</param>
        /// <param name="values">(Optional) A dictionary that contains additional user attributes to store in the user profile. The default is null.</param>
        /// <returns>
        /// A token that can be sent to the user to confirm the user account.
        /// </returns>
        /// <exception cref="MembershipCreateUserException"></exception>
        public override string CreateUserAndAccount(string userName, string password, bool requireConfirmation, IDictionary<string, object> values)
        {
            userName = userName.Trim().ToLower();

            var userProfile = this.usersService.GetUserProfile(userName);

            if (userProfile.IsNotNull())
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.DuplicateEmail);
            }

            var newUserProfile = new UserProfile
            {
                UserName = userName,
                LoweredUserName = values.ContainsKey("LoweredUserName") ? values["LoweredUserName"].ToString() : String.Empty,
                MobileAlias = values.ContainsKey("MobileAlias") ? values["MobileAlias"].ToString() : String.Empty,
                DisplayName = values.ContainsKey("DisplayName") ? values["DisplayName"].ToString() : String.Empty,
            };

            this.usersService.Save(newUserProfile);

            var membership = new Vserv.Accounting.Data.Entity.Membership
            {
                UserId = newUserProfile.UserId,
                CreateDate = DateTime.Now,
                PasswordSalt = this.usersService.GetHash(password),
                IsConfirmed = true, // Turn it false if you want to send an email for activation link.
                ConfirmationToken = Guid.NewGuid().ToString().ToLower(),
                Email = values.ContainsKey("Email") ? values["Email"].ToString() : String.Empty,
                LoweredEmail = values.ContainsKey("LoweredEmail") ? values["LoweredEmail"].ToString() : String.Empty,
                Comment = values.ContainsKey("Comment") ? Convert.ToString(values["Comment"]) : String.Empty,
            };

            this.usersService.Save(membership, add: true);

            List<UserSecurityQuestion> userSecurityQuestions = new List<UserSecurityQuestion>();
            UserSecurityQuestion userSecurityQuestion = null;

            // Save Security Questions for each user.
            if (values.ContainsKey(Common.CommonConstants.USER_SECURITY_QUESTION_1))
            {
                userSecurityQuestion = (UserSecurityQuestion)values[Common.CommonConstants.USER_SECURITY_QUESTION_1];
                userSecurityQuestion.CreatedBy = userName;
                userSecurityQuestion.CreatedDate = DateTime.Now;
                userSecurityQuestion.IsActive = true;
                userSecurityQuestions.Add(userSecurityQuestion);
            }

            if (values.ContainsKey(Common.CommonConstants.USER_SECURITY_QUESTION_2))
            {
                userSecurityQuestion = (UserSecurityQuestion)values[Common.CommonConstants.USER_SECURITY_QUESTION_2];
                userSecurityQuestion.CreatedBy = userName;
                userSecurityQuestion.CreatedDate = DateTime.Now;
                userSecurityQuestion.IsActive = true;
                userSecurityQuestions.Add(userSecurityQuestion);
            }

            if (values.ContainsKey(Common.CommonConstants.USER_SECURITY_QUESTION_3))
            {
                userSecurityQuestion = (UserSecurityQuestion)values[Common.CommonConstants.USER_SECURITY_QUESTION_3];
                userSecurityQuestion.CreatedBy = userName;
                userSecurityQuestion.CreatedDate = DateTime.Now;
                userSecurityQuestion.IsActive = true;
                userSecurityQuestions.Add(userSecurityQuestion);
            }

            this.usersService.Save(userSecurityQuestions, userName, add: true);
            return membership.ConfirmationToken;
        }

        /// <summary>
        /// When overridden in a derived class, deletes the specified membership account.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>
        /// true if the user account was deleted; otherwise, false.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool DeleteAccount(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, generates a password reset token that can be sent to a user in email.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="tokenExpirationInMinutesFromNow">(Optional) The time, in minutes, until the password reset token expires. The default is 1440 (24 hours).</param>
        /// <returns>
        /// A token to send to the user.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, returns all OAuth membership accounts associated with the specified user name.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>
        /// A list of all OAuth membership accounts associated with the specified user name.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override ICollection<OAuthAccountData> GetAccountsForUser(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, returns the date and time when the specified user account was created.
        /// </summary>
        /// <param name="userName">The user name of the account.</param>
        /// <returns>
        /// The date and time the account was created, or <see cref="F:System.DateTime.MinValue" /> if the account creation date is not available.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override DateTime GetCreateDate(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, returns the date and time when an incorrect password was most recently entered for the specified user account.
        /// </summary>
        /// <param name="userName">The user name of the account.</param>
        /// <returns>
        /// The date and time when an incorrect password was most recently entered for this user account, or <see cref="F:System.DateTime.MinValue" /> if an incorrect password has not been entered for this user account.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override DateTime GetLastPasswordFailureDate(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, returns the date and time when the password was most recently changed for the specified membership account.
        /// </summary>
        /// <param name="userName">The user name of the account.</param>
        /// <returns>
        /// The date and time when the password was more recently changed for membership account, or <see cref="F:System.DateTime.MinValue" /> if the password has never been changed for this user account.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override DateTime GetPasswordChangedDate(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, returns the number of times that the password for the specified user account was incorrectly entered since the most recent successful login or since the user account was created.
        /// </summary>
        /// <param name="userName">The user name of the account.</param>
        /// <returns>
        /// The count of failed password attempts for the specified user account.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override int GetPasswordFailuresSinceLastSuccess(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, returns an ID for a user based on a password reset token.
        /// </summary>
        /// <param name="token">The password reset token.</param>
        /// <returns>
        /// The user ID.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override int GetUserIdFromPasswordResetToken(string token)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, returns a value that indicates whether the user account has been confirmed by the provider.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>
        /// true if the user is confirmed; otherwise, false.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool IsConfirmed(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, resets a password after verifying that the specified password reset token is valid.
        /// </summary>
        /// <param name="token">A password reset token.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>
        /// true if the password was changed; otherwise, false.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool ResetPasswordWithToken(string token, string newPassword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, returns the user ID for the specified OAuth or OpenID provider and provider user ID.
        /// </summary>
        /// <param name="provider">The name of the OAuth or OpenID provider.</param>
        /// <param name="providerUserId">The OAuth or OpenID provider user ID. This is not the user ID of the user account, but the user ID on the OAuth or Open ID provider.</param>
        /// <returns></returns>
        public override int GetUserIdFromOAuth(string provider, string providerUserId)
        {
            var oAuthMembership = this.usersService.GetOAuthMembership(provider, providerUserId);
            if (oAuthMembership != null)
            {
                return oAuthMembership.UserId;
            }
            return -1;
        }

        /// <summary>
        /// When overridden in a derived class, creates a new OAuth membership account, or updates an existing OAuth Membership account.
        /// </summary>
        /// <param name="provider">The OAuth or OpenID provider.</param>
        /// <param name="providerUserId">The OAuth or OpenID provider user ID. This is not the user ID of the user account, but the user ID on the OAuth or Open ID provider.</param>
        /// <param name="userName">The user name.</param>
        /// <exception cref="Exception">User profile was not created.</exception>
        public override void CreateOrUpdateOAuthAccount(string provider, string providerUserId, string userName)
        {
            var userProfile = this.usersService.GetUserProfile(userName);
            if (userProfile == null)
            {
                throw new Exception("User profile was not created.");
            }
            this.usersService.SaveOAuthMembership(provider, providerUserId, userProfile.UserId);
        }

        /// <summary>
        /// Returns the user name that is associated with the specified user ID.
        /// </summary>
        /// <param name="userId">The user ID to get the name for.</param>
        /// <returns>
        /// The user name.
        /// </returns>
        public override string GetUserNameFromId(int userId)
        {
            var userProfile = this.usersService.GetUserProfile(userId);
            if (userProfile != null)
            {
                return userProfile.UserName;
            }
            return null;
        }

        #endregion ExtendedMembershipProvider

        /*#region Helpers
        private const string salt = "HJIO6589";
        public static string GetHash(string text)
        {
            var buffer = Encoding.UTF8.GetBytes(String.Concat(text, salt));
            var cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
            return hash;
        }
        #endregion Helpers*/
    }
}