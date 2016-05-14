using System;
using System.ComponentModel.DataAnnotations;
using Vserv.Accounting.Web.Code;

namespace Vserv.Accounting.Web.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email Address is Required.")]
        [Display(Name = "Email Address")]
        [ValidEmailAddress(ErrorMessage = "Incorrect Email Address.")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(256)]
        public string Email
        {
            get;
            set;
        }

        [Required(ErrorMessage = "User Name is Required.")]
        [Display(Name = "User Name")]
        [DataType(DataType.Text)]
        [MaxLength(256)]
        public string UserName
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Password is Required.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password
        {
            get;
            set;
        }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password is Required.")]
        [Display(Name = "Confirm password")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword
        {
            get;
            set;
        }

        [Required(ErrorMessage = "First Name is Required.")]
        [Display(Name = "First Name")]
        [MaxLength(256)]
        public string FirstName
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Last Name is Required.")]
        [Display(Name = "Last Name")]
        [MaxLength(256)]
        public string LastName
        {
            get;
            set;
        }

        [Display(Name = "Mobile Number")]
        [MaxLength(16)]
        public string MobileAlias
        {
            get;
            set;
        }

        [Display(Name = "Password Question")]
        [MaxLength(256)]
        public string PasswordQuestion
        {
            get;
            set;
        }

        [Display(Name = "Password Answer")]
        [MaxLength(128)]
        public string PasswordAnswer
        {
            get;
            set;
        }

        [Display(Name = "Comment")]
        [MaxLength(256)]
        public string Comment
        {
            get;
            set;
        }

        public string DisplayName
        {
            get { return String.Format("{0}, {1}", FirstName, LastName); }
        }

        public string LoweredUserName
        {
            get { return String.IsNullOrWhiteSpace(UserName) ? String.Empty : UserName.Trim().ToLower(); }
        }

        public string LoweredEmail
        {
            get { return String.IsNullOrWhiteSpace(Email) ? String.Empty : Email.Trim().ToLower(); }
        }
    }
}