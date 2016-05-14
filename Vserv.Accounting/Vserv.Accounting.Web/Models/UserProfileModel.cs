using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "New Password")]
        public string NewPassword
        {
            get;
            set;
        }

        [Display(Name = "Confirm Password")]
        public string ConfirmPassword
        {
            get;
            set;
        }
    }
}