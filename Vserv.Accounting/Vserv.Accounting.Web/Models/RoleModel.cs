using System.Collections.Generic;

namespace Vserv.Accounting.Web.Models
{
    public class RoleModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string LoweredRoleName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserProfileModel> UserProfiles { get; set; }
    }
}