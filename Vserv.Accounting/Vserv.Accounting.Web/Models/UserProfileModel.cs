using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vserv.Accounting.Web.Code;

namespace Vserv.Accounting.Web.Models
{
    public class UserProfileModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [Display(Name = "User Id")]
        public int UserId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Display(Name = "User Name")]
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the lowered user.
        /// </summary>
        /// <value>
        /// The name of the lowered user.
        /// </value>
        [Display(Name = "Lowered User Name")]
        public string LoweredUserName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        [Display(Name = "Full Name")]
        public string DisplayName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the mobile alias.
        /// </summary>
        /// <value>
        /// The mobile alias.
        /// </value>
        [Display(Name = "Mobile")]
        public string MobileAlias
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the is anonymous.
        /// </summary>
        /// <value>
        /// The is anonymous.
        /// </value>
        [Display(Name = "Is Anonymous User")]
        public Nullable<bool> IsAnonymous
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last activity date.
        /// </summary>
        /// <value>
        /// The last activity date.
        /// </value>
        [Display(Name = "Last Activity Date")]
        public Nullable<DateTime> LastActivityDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        [Display(Name = "Roles")]
        public virtual ICollection<RoleModel> Roles
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the old password.
        /// </summary>
        /// <value>
        /// The old password.
        /// </value>
        [Display(Name = "Old Password")]
        public string OldPassword
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        /// <value>
        /// The new password.
        /// </value>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        [ValidChangePasswordComplexity]
        public string NewPassword
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        /// <value>
        /// The confirm password.
        /// </value>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword
        {
            get;
            set;
        }
    }
}