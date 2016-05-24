#region Namespaces
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Vserv.Accounting.Business.Managers;
using Vserv.Accounting.Core.Services;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Web.Models;
using WebMatrix.WebData;
using System.Linq;
using Vserv.Accounting.Common;
using Vserv.Common.Extensions;

#endregion

namespace Vserv.Accounting.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class AccountController : Controller
    {
        #region Variables
        /// <summary>
        /// The users service
        /// </summary>
        private readonly IUsersService usersService;
        /// <summary>
        /// The email service
        /// </summary>
        private readonly IEmailService emailService;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="usersService">The users service.</param>
        /// <param name="emailService">The email service.</param>
        public AccountController(IUsersService usersService, IEmailService emailService)
        {
            this.usersService = usersService;
            this.emailService = emailService;
        }
        #endregion

        #region Login

        //
        // GET: /Account/Login/
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Account/Login
        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return Url.IsLocalUrl(returnUrl) ? (ActionResult)Redirect(returnUrl) : RedirectToAction("Index", "Dashboard", new { area = "" });
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "User Name or password provided is incorrect.");
            return View(model);
        }

        #endregion Login

        #region Register

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            SetSecurityQuestions();
            return View();
        }

        //
        // POST: /Account/Register
        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    var token = WebSecurity.CreateUserAndAccount(model.UserName, model.Password, model, requireConfirmationToken: false);
                    return RedirectToAction("Index", "Account");

                    // this.usersService.SendAccountActivationMail(model.Email);
                    //return RedirectToAction("success", "register", new { email = model.Email, area = "account" });
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            SetSecurityQuestions();
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void SetSecurityQuestions()
        {
            AccountManager _manager = new AccountManager();
            List<SecurityQuestion> securityQuestions = _manager.GetSecurityQuestions();
            ViewBag.SecurityQuestionCollection1 = securityQuestions.Where(question => question.CollectionId == CommonConstants.INT_ONE);
            ViewBag.SecurityQuestionCollection2 = securityQuestions.Where(question => question.CollectionId == CommonConstants.INT_TWO);
            ViewBag.SecurityQuestionCollection3 = securityQuestions.Where(question => question.CollectionId == CommonConstants.INT_THREE);
        }
        #endregion Register

        #region Change Password

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userProfileModel">The user profile model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangePassword(UserProfileModel userProfileModel)
        {
            AccountManager _accountManager = new Business.Managers.AccountManager();
            UserProfile userProfile = _accountManager.GetUserProfile(User.Identity.Name);
            var membership = this.usersService.GetMembership(userProfile.UserId);
            if (membership == null)
            {
                return RedirectToAction("BadLink");
            }

            // passwords
            if (String.IsNullOrEmpty(userProfileModel.NewPassword))
            {
                ModelState.AddModelError("newPassword", "New Password is required.");
                return View();
            }
            if (userProfileModel.NewPassword.Length < 4)
            {
                ModelState.AddModelError("newPassword", "New Password is too short.");
                return View();
            }
            if (String.IsNullOrEmpty(userProfileModel.ConfirmPassword))
            {
                ModelState.AddModelError("confirmPassword", "Confirm password is required.");
                return View();
            }
            if (userProfileModel.NewPassword != userProfileModel.ConfirmPassword)
            {
                ModelState.AddModelError("confirmPassword", "Passwords are mismatched.");
                return View();
            }

            try
            {
                this.usersService.ChangePassword(membership, userProfileModel.NewPassword);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("_FORM", "Error is occurred. " + ex.Message);
            }

            return RedirectToAction("Success", "Home", new { successMessage = "Your password has been changed successfully." });
        }

        #endregion

        #region Forgot Password

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            ForgotPasswordModel forgotPasswordModel = new ForgotPasswordModel();
            return View(forgotPasswordModel);
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            // Fetch Security Quesiton once user enters a valid username.
            if (ModelState.IsValidField("UserName") && String.IsNullOrWhiteSpace(forgotPasswordModel.SecurityQuestion))
            {
                AccountManager _manager = new AccountManager();
                UserSecurityQuestion randomSecurityQuestion = _manager.GetRandomSecurityQuestion(forgotPasswordModel.UserName);

                if (randomSecurityQuestion.IsNotNull() && randomSecurityQuestion.SecurityQuestion.IsNotNull())
                {
                    forgotPasswordModel.SecurityQuestion = randomSecurityQuestion.SecurityQuestion.Question;
                    forgotPasswordModel.SecurityQuestionId = randomSecurityQuestion.SecurityQuestionId;
                }
            }

            if (ModelState.IsValid)
            {
                AccountManager _manager = new AccountManager();
                forgotPasswordModel.UserName = forgotPasswordModel.UserName.Trim().ToLower();
                forgotPasswordModel.EmailAddress = forgotPasswordModel.EmailAddress.Trim().ToLower();

                bool isRegisteredUser = _manager.IsRegisteredUser(ConvertTo(forgotPasswordModel));
                if (isRegisteredUser)
                {
                    UserProfileModel userProfileModel = new UserProfileModel
                    {
                        UserName = forgotPasswordModel.UserName
                    };

                    return RedirectToAction("ResetPassword", userProfileModel);
                }
                else
                {
                    ModelState.AddModelError("UserRecoveryFailure", "No account found satisfying provided details.");
                }
            }

            return View(forgotPasswordModel);
        }

        public ActionResult ResetPassword(UserProfileModel userProfileModel)
        {
            AccountManager _accountManager = new Business.Managers.AccountManager();
            UserProfile userProfile = _accountManager.GetUserProfile(userProfileModel.UserName);
            var membership = this.usersService.GetMembership(userProfile.UserId);
            if (membership == null)
            {
                return RedirectToAction("BadLink");
            }

            // passwords
            if (String.IsNullOrEmpty(userProfileModel.NewPassword))
            {
                ModelState.AddModelError("newPassword", "New Password is required.");
                return View();
            }
            if (userProfileModel.NewPassword.Length < 4)
            {
                ModelState.AddModelError("newPassword", "New Password is too short.");
                return View();
            }
            if (String.IsNullOrEmpty(userProfileModel.ConfirmPassword))
            {
                ModelState.AddModelError("confirmPassword", "Confirm password is required.");
                return View();
            }
            if (userProfileModel.NewPassword != userProfileModel.ConfirmPassword)
            {
                ModelState.AddModelError("confirmPassword", "Passwords are mismatched.");
                return View();
            }

            try
            {
                this.usersService.ChangePassword(membership, userProfileModel.NewPassword);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("_FORM", "Error is occurred. " + ex.Message);
            }

            return RedirectToAction("Success", "Home", new { successMessage = "Your password has been changed successfully." });
        }

        [NonAction]
        private ForgotPassword ConvertTo(ForgotPasswordModel forgotPasswordModel)
        {
            return new ForgotPassword
            {
                UserName = forgotPasswordModel.UserName,
                EmailAddress = forgotPasswordModel.EmailAddress,
                SecurityQuestion = forgotPasswordModel.SecurityQuestion,
                SecurityQuestionAnswer = forgotPasswordModel.SecurityQuestionAnswer,
                MobileNumber = forgotPasswordModel.MobileNumber,
                SecurityQuestionId = forgotPasswordModel.SecurityQuestionId,
            };
        }

        #endregion Forgot Password

        #region User Profile

        /// <summary>
        /// Users the profile.
        /// </summary>
        /// <returns></returns>
        public ActionResult UserProfile()
        {
            AccountManager _accountManager = new AccountManager();
            UserProfile userProfile = _accountManager.GetUserProfile(User.Identity.Name);
            UserProfileModel userProfileModel = ConvertTo(userProfile);
            return View("profile", userProfileModel);
        }

        public ActionResult SecurityQuestions()
        {
            AccountManager _accountManager = new AccountManager();
            var securityQuestions = _accountManager.GetSecurityQuestions();
            return View(securityQuestions);
        }

        #endregion

        #region Logout

        // [HttpPost, ValidateAntiForgeryToken]
        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Account");
        }

        /// <summary>
        /// Bads the link.
        /// </summary>
        /// <returns></returns>
        public ActionResult BadLink()
        {
            return View();
        }

        //
        // GET: /Account/ChangePassword/Success

        /// <summary>
        /// Successes the change password.
        /// </summary>
        /// <returns></returns>
        public ActionResult SuccessChangePassword()
        {
            return View();
        }

        #endregion Logout

        #region Helpers

        /// <summary>
        /// Errors the code to string.
        /// </summary>
        /// <param name="createStatus">The create status.</param>
        /// <returns></returns>
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        /// <summary>
        /// Converts to.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        /// <returns></returns>
        private UserProfileModel ConvertTo(UserProfile userProfile)
        {
            return new UserProfileModel
            {
                UserId = userProfile.UserId,
                UserName = userProfile.UserName,
                DisplayName = userProfile.DisplayName,
                MobileAlias = userProfile.MobileAlias,
                LastActivityDate = userProfile.LastActivityDate,
            };
        }



        #endregion
    }
}
