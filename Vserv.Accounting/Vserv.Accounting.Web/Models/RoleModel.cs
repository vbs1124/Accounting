using System.Collections.Generic;

namespace Vserv.Accounting.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleModel
    {
        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public int RoleId { get; set; }
        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        /// <value>
        /// The name of the role.
        /// </value>
        public string RoleName { get; set; }
        /// <summary>
        /// Gets or sets the name of the lowered role.
        /// </summary>
        /// <value>
        /// The name of the lowered role.
        /// </value>
        public string LoweredRoleName { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the user profiles.
        /// </summary>
        /// <value>
        /// The user profiles.
        /// </value>
        public virtual ICollection<UserProfileModel> UserProfiles { get; set; }
    }
}