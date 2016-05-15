using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vserv.Accounting.Web.Code;

namespace Vserv.Accounting.Web.Models
{
    public class UserProfileModel
    {
        [Display(Name = "User Id")]
        public int UserId
        {
            get;
            set;
        }

        [Display(Name = "User Name")]
        public string UserName
        {
            get;
            set;
        }

        [Display(Name = "Lowered User Name")]
        public string LoweredUserName
        {
            get;
            set;
        }

        [Display(Name = "Full Name")]
        public string DisplayName
        {
            get;
            set;
        }

        [Display(Name = "Mobile")]
        public string MobileAlias
        {
            get;
            set;
        }

        [Display(Name = "Is Anonymous User")]
        public Nullable<bool> IsAnonymous
        {
            get;
            set;
        }

        [Display(Name = "Last Activity Date")]
        public Nullable<DateTime> LastActivityDate
        {
            get;
            set;
        }

        [Display(Name = "Roles")]
        public virtual ICollection<RoleModel> Roles
        {
            get;
            set;
        }

        [Display(Name = "Old Password")]
        public string OldPassword
        {
            get;
            set;
        }

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