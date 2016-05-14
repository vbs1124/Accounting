using System;
using System.Collections.Generic;

namespace Vserv.Accounting.Business.Entities
{
    public partial class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string LoweredRoleName { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
