#region Namespaces
using System;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts; 
#endregion

namespace Vserv.Accounting.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Vserv.Common.Contracts.IDataRepository{Vserv.Accounting.Data.Entity.UserProfile}" />
    public interface IUserProfileRepository : IDataRepository<UserProfile>
    {
        /// <summary>
        /// Gets the user profile.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        UserProfile GetUserProfile(string userName);

        /// <summary>
        /// Gets the user profile.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        UserProfile GetUserProfile(int userId);

        /// <summary>
        /// Saves the specified user profile.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        void Save(UserProfile userProfile);

        /// <summary>
        /// Gets the o authentication membership.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="providerUserId">The provider user identifier.</param>
        /// <returns></returns>
        OAuthMembership GetOAuthMembership(string provider, string providerUserId);

        /// <summary>
        /// Saves the o authentication membership.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="providerUserId">The provider user identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void SaveOAuthMembership(string provider, string providerUserId, int userId);

        /// <summary>
        /// Gets the membership.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Membership GetMembership(int userId);

        /// <summary>
        /// Gets the membership by confirm token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="withUserProfile">if set to <c>true</c> [with user profile].</param>
        /// <returns></returns>
        Membership GetMembershipByConfirmToken(string token, bool withUserProfile);

        /// <summary>
        /// Gets the membership by verification token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="withUserProfile">if set to <c>true</c> [with user profile].</param>
        /// <returns></returns>
        Membership GetMembershipByVerificationToken(string token, bool withUserProfile);

        /// <summary>
        /// Saves the specified membership.
        /// </summary>
        /// <param name="membership">The membership.</param>
        /// <param name="add">if set to <c>true</c> [add].</param>
        void Save(Membership membership, bool add);

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        string[] GetRoles(string userName);

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="membership">The membership.</param>
        void ChangePassword(Membership membership);
    }
}
