using Vserv.Accounting.Data.Entity;

namespace Vserv.Accounting.Core.Services
{
    public interface IUsersService
    {
        // UserProfiles 
        UserProfile GetUserProfile(string userName);

        UserProfile GetUserProfile(int userId);

        void Save(UserProfile userProfile);

        // OAuth Membership
        OAuthMembership GetOAuthMembership(string provider, string providerUserId);

        void SaveOAuthMembership(string provider, string providerUserId, int userId);

        // Membership
        void Save(Membership membership, bool add);

        Membership GetMembership(int userId);

        Membership GetMembershipByConfirmToken(string token, bool withUserProfile);

        Membership GetMembershipByVerificationToken(string token, bool withUserProfile);

        void ChangePassword(Membership membership, string newPassword);

        // Emails
        void SendAccountActivationMail(string email);

        void SentChangePasswordEmail(string email);

        // Roles
        string[] GetRoles(string userName);

        // Helper to do 
        string GetHash(string text);
    }
}
