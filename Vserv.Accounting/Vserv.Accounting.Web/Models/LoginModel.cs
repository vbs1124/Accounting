using System.ComponentModel.DataAnnotations;

namespace Vserv.Accounting.Web.Models
{
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
         [Required(ErrorMessage = "User Name is Required.")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

         /// <summary>
         /// Gets or sets the password.
         /// </summary>
         /// <value>
         /// The password.
         /// </value>
        [Required(ErrorMessage = "Password is Required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remember me].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [remember me]; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}