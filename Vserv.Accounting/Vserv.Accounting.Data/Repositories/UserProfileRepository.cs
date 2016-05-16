#region Namespaces

using System.ComponentModel.Composition;
using System.Linq;
using Vserv.Accounting.Data.Entity;

#endregion

namespace Vserv.Accounting.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Vserv.Accounting.Data.DataRepositoryBase{Vserv.Accounting.Data.Entity.UserProfile}" />
    /// <seealso cref="Vserv.Accounting.Data.IUserProfileRepository" />
    [Export(typeof(IUserProfileRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserProfileRepository : DataRepositoryBase<UserProfile>, IUserProfileRepository
    {
        #region Methods

        #region Public Methods

        /// <summary>
        /// Gets the user profile.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public UserProfile GetUserProfile(string userName)
        {
            using (var context = new VservAccountingDBEntities())
            {
                var result = context.UserProfiles.FirstOrDefault(userProfile => userProfile.UserName == userName);
                return result;
            }
        }

        /// <summary>
        /// Gets the user profile.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public UserProfile GetUserProfile(int userId)
        {
            using (var _vservAccountingDBEntities = new VservAccountingDBEntities())
            {
                return _vservAccountingDBEntities.UserProfiles.FirstOrDefault(x => x.UserId.Equals(userId));
            }
        }

        /// <summary>
        /// Saves the specified user profile.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        public void Save(UserProfile userProfile)
        {
            try
            {
                using (var _vservAccountingDBEntities = new VservAccountingDBEntities())
                {
                    if (userProfile.UserId == 0)
                    {
                        _vservAccountingDBEntities.UserProfiles.Add(userProfile);
                    }
                    _vservAccountingDBEntities.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the o authentication membership.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="providerUserId">The provider user identifier.</param>
        /// <returns></returns>
        public OAuthMembership GetOAuthMembership(string provider, string providerUserId)
        {
            using (var _vservAccountingDBEntities = new VservAccountingDBEntities())
            {
                return _vservAccountingDBEntities.OAuthMemberships.FirstOrDefault(x => x.Provider.Equals(provider) && x.ProviderUserId.Equals(providerUserId));
            }
        }

        /// <summary>
        /// Saves the o authentication membership.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="providerUserId">The provider user identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void SaveOAuthMembership(string provider, string providerUserId, int userId)
        {
            using (var _vservAccountingDBEntities = new VservAccountingDBEntities())
            {
                var oAuthMembership = _vservAccountingDBEntities.OAuthMemberships.FirstOrDefault(x => x.Provider.Equals(provider) && x.ProviderUserId.Equals(providerUserId));

                if (oAuthMembership == null)
                {
                    oAuthMembership = new OAuthMembership { Provider = provider, ProviderUserId = providerUserId };
                    _vservAccountingDBEntities.OAuthMemberships.Add(oAuthMembership);
                }

                oAuthMembership.UserId = userId;
                _vservAccountingDBEntities.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the membership.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Membership GetMembership(int userId)
        {
            using (var _vservAccountingDBEntities = new VservAccountingDBEntities())
            {
                return _vservAccountingDBEntities.Memberships.FirstOrDefault(x => x.UserId == userId);
            }
        }

        /// <summary>
        /// Gets the membership by confirm token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="withUserProfile">if set to <c>true</c> [with user profile].</param>
        /// <returns></returns>
        public Membership GetMembershipByConfirmToken(string token, bool withUserProfile)
        {
            using (var _vservAccountingDBEntities = new VservAccountingDBEntities())
            {
                var membership = _vservAccountingDBEntities.Memberships.FirstOrDefault(x => x.ConfirmationToken.Equals(token.ToLower()));
                if (membership != null && withUserProfile)
                {
                    membership.UserProfile = _vservAccountingDBEntities.UserProfiles.First(x => x.UserId == membership.UserId);
                }
                return membership;
            }

        }

        /// <summary>
        /// Gets the membership by verification token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="withUserProfile">if set to <c>true</c> [with user profile].</param>
        /// <returns></returns>
        public Membership GetMembershipByVerificationToken(string token, bool withUserProfile)
        {
            using (var _vservAccountingDBEntities = new VservAccountingDBEntities())
            {
                var membership = _vservAccountingDBEntities.Memberships.FirstOrDefault(x => x.PasswordVerificationToken.Equals(token.ToLower()));
                if (membership != null && withUserProfile)
                {
                    membership.UserProfile = _vservAccountingDBEntities.UserProfiles.First(x => x.UserId == membership.UserId);
                }
                return membership;
            }
        }

        /// <summary>
        /// Saves the specified membership.
        /// </summary>
        /// <param name="membership">The membership.</param>
        /// <param name="add">if set to <c>true</c> [add].</param>
        public void Save(Membership membership, bool add)
        {
            try
            {
                using (var _vservAccountingDBEntities = new VservAccountingDBEntities())
                {
                    if (add)
                    {
                        _vservAccountingDBEntities.Memberships.Add(membership);
                    }
                    _vservAccountingDBEntities.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public string[] GetRoles(string userName)
        {
            using (var _vservAccountingDBEntities = new VservAccountingDBEntities())
            {
                var userProfile = _vservAccountingDBEntities.UserProfiles.FirstOrDefault(x => x.UserName.Equals(userName));
                if (userProfile != null)
                {
                    return userProfile.Roles.Select(x => x.RoleName).ToArray();
                }
                return new string[] { };
            }
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="membership">The membership.</param>
        public void ChangePassword(Membership membership)
        {
            using (var _vservAccountingDBEntities = new VservAccountingDBEntities())
            {
                _vservAccountingDBEntities.SaveChanges();
            }
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            //    Dispose(true);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Converts to user profile dc.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        /// <returns></returns>
        private UserProfile ConvertToUserProfileDC(UserProfile userProfile)
        {
            return new UserProfile
            {
                UserId = userProfile.UserId,
                UserName = userProfile.UserName,
                LoweredUserName = userProfile.LoweredUserName,
                DisplayName = userProfile.DisplayName,
                MobileAlias = userProfile.MobileAlias,
                IsAnonymous = userProfile.IsAnonymous,
                LastActivityDate = userProfile.LastActivityDate,
            };

        }

        #endregion Private Methods

        #endregion
    }
}
