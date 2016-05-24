using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vserv.Accounting.Web.Code;

namespace Vserv.Accounting.Web.Models
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "User Name is Required.")]
        [Display(Name = "User Name")]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        [IsRegisteredUser]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email Address is Required.")]
        [Display(Name = "Email Address")]
        [ValidEmailAddress(ErrorMessage = "Incorrect Email Address.")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        public string EmailAddress { get; set; }

        [Display(Name = "Security Question")]
        public string SecurityQuestion { get; set; }

        [Display(Name = "Answer")]
        [Required(ErrorMessage = "Answer is required.")]
        [MaxLength(100)]
        public string SecurityQuestionAnswer { get; set; }

        [Display(Name = "Mobile Number")]
        [MaxLength(10)]
        public string MobileNumber { get; set; }

        public int SecurityQuestionId { get; set; }
    }
}