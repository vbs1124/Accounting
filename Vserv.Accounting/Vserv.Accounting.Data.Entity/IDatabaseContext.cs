using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Vserv.Accounting.Data.Entity
{
    public interface IDatabaseContext
    {
        IDbSet<UserProfile> UserProfiles { get; }

        IDbSet<OAuthMembership> OAuthMemberships { get; }

        IDbSet<Membership> Memberships { get; }

        IDbSet<Config> Configs { get; }

        IDbSet<Role> Roles { get; }

        void Add(UserProfile userProfile);

        void Add(OAuthMembership oAuthMembership);

        void Add(Membership membership);

        void Add(UserSecurityQuestion userSecurityQuestion);

        void SaveChanges();

    }
}
