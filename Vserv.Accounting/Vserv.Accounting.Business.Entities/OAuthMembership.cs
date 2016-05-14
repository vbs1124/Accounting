using System;
using System.Collections.Generic;

namespace Vserv.Accounting.Business.Entities
{
    public partial class OAuthMembership
    {
        public string Provider { get; set; }
        public string ProviderUserId { get; set; }
        public int UserId { get; set; }
    }
}
