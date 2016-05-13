using System;
using System.Collections.Generic;

namespace Vserv.Accounting.Business.Entities
{
    public partial class UserProfile
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string LoweredUserName { get; set; }
        public string DisplayName { get; set; }
        public string MobileAlias { get; set; }
        public Nullable<bool> IsAnonymous { get; set; }
        public Nullable<System.DateTime> LastActivityDate { get; set; }
    
        public virtual ICollection<Role> Roles { get; set; }
    }
}
