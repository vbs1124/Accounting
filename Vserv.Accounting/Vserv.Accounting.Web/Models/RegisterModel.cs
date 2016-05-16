using System;
using System.ComponentModel.DataAnnotations;
using Vserv.Accounting.Web.Code;

namespace Vserv.Accounting.Web.Models
{
    public class RegisterModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
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

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required(ErrorMessage = "User Name is Required.")]
        [Display(Name = "User Name")]
        [DataType(DataType.Text)]
        [MaxLength(256)]
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required(ErrorMessage = "Password is Required.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [ValidPasswordComplexity]
        public string Password
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
        [Required(ErrorMessage = "Confirm Password is Required.")]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required(ErrorMessage = "First Name is Required.")]
        [Display(Name = "First Name")]
        [MaxLength(256)]
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required(ErrorMessage = "Last Name is Required.")]
        [Display(Name = "Last Name")]
        [MaxLength(256)]
        public string LastName
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
        [Display(Name = "Mobile Number")]
        [MaxLength(16)]
        public string MobileAlias
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the password question.
        /// </summary>
        /// <value>
        /// The password question.
        /// </value>
        [Display(Name = "Password Question")]
        [MaxLength(256)]
        public string PasswordQuestion
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the password answer.
        /// </summary>
        /// <value>
        /// The password answer.
        /// </value>
        [Display(Name = "Password Answer")]
        [MaxLength(128)]
        public string PasswordAnswer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        [Display(Name = "Comment")]
        [MaxLength(256)]
        public string Comment
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName
        {
            get { return String.Format("{0}, {1}", FirstName, LastName); }
        }

        /// <summary>
        /// Gets the name of the lowered user.
        /// </summary>
        /// <value>
        /// The name of the lowered user.
        /// </value>
        public string LoweredUserName
        {
            get { return String.IsNullOrWhiteSpace(UserName) ? String.Empty : UserName.Trim().ToLower(); }
        }

        /// <summary>
        /// Gets the lowered email.
        /// </summary>
        /// <value>
        /// The lowered email.
        /// </value>
        public string LoweredEmail
        {
            get { return String.IsNullOrWhiteSpace(Email) ? String.Empty : Email.Trim().ToLower(); }
        }
    }
}