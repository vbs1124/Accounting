using System;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

namespace Vserv.Accounting.Data
{
    public interface IUserProfileRepository : IDisposable
    {
        UserProfile GetUserProfile(string userName);

        UserProfile GetUserProfile(int userId);

        void Save(UserProfile userProfile);

        OAuthMembership GetOAuthMembership(string provider, string providerUserId);

        void SaveOAuthMembership(string provider, string providerUserId, int userId);

        Membership GetMembership(int userId);

        Membership GetMembershipByConfirmToken(string token, bool withUserProfile);

        Membership GetMembershipByVerificationToken(string token, bool withUserProfile);

        void Save(Membership membership, bool add);

        string[] GetRoles(string userName);

        void ChangePassword(Membership membership);
    }
}
