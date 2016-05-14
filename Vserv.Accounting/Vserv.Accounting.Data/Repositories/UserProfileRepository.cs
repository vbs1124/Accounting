#region Namespaces

using System.ComponentModel.Composition;
using System.Linq;
using Vserv.Accounting.Data.Entity;

#endregion

namespace Vserv.Accounting.Data
{
    [Export(typeof(IUserProfileRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserProfileRepository : DataRepositoryBase<UserProfile>, IUserProfileRepository
    {
        #region Methods

        #region Public Methods

        public UserProfile GetUserProfile(string userName)
        {
            using (var context = new VservAccountingDBEntities())
            {
                var result = context.UserProfiles.FirstOrDefault(userProfile => userProfile.UserName == userName);
                return result;
            }
        }

        public UserProfile GetUserProfile(int userId)
        {
            using (var _vservAccountingDBEntities = new VservAccountingDBEntities())
            {
                return _vservAccountingDBEntities.UserProfiles.FirstOrDefault(x => x.UserId.Equals(userId));
            }
        }

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

        public OAuthMembership GetOAuthMembership(string provider, string providerUserId)
        {
            using (var _vservAccountingDBEntities = new VservAccountingDBEntities())
            {
                return _vservAccountingDBEntities.OAuthMemberships.FirstOrDefault(x => x.Provider.Equals(provider) && x.ProviderUserId.Equals(providerUserId));
            }
        }

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

        public Membership GetMembership(int userId)
        {
            using (var _vservAccountingDBEntities = new VservAccountingDBEntities())
            {
                return _vservAccountingDBEntities.Memberships.FirstOrDefault(x => x.UserId == userId);
            }
        }

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

        public void ChangePassword(Membership membership)
        {
            using (var _vservAccountingDBEntities = new VservAccountingDBEntities())
            {
                _vservAccountingDBEntities.SaveChanges();
            }
        }

        public void Dispose()
        {
        //    Dispose(true);
        }

        #endregion Public Methods

        #region Private Methods

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
