//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.Owin.Security;
//using Vserv.Accounting.Web.Models;

//namespace Vserv.Accounting.Web.Controllers
//{
//    [Authorize]
//    public class AccountController : Controller
//    {
//        public AccountController()
//            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
//        {
//        }

//        public AccountController(UserManager<ApplicationUser> userManager)
//        {
//            UserManager = userManager;
//        }

//        public UserManager<ApplicationUser> UserManager { get; private set; }

//        //
//        // GET: /Account/Login
//        [AllowAnonymous]
//        public ActionResult Login(string returnUrl)
//        {
//            ViewBag.ReturnUrl = returnUrl;
//            return View();
//        }

//        //
//        // POST: /Account/Login
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = await UserManager.FindAsync(model.UserName, model.Password);
//                if (user != null)
//                {
//                    await SignInAsync(user, model.RememberMe);
//                    return RedirectToLocal(returnUrl);
//                }
//                else
//                {
//                    ModelState.AddModelError("", "Invalid username or password.");
//                }
//            }

//            // If we got this far, something failed, redisplay form
//            return View(model);
//        }

//        //
//        // GET: /Account/Register
//        [AllowAnonymous]
//        public ActionResult Register()
//        {
//            return View();
//        }

//        //
//        // POST: /Account/Register
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Register(RegisterViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = new ApplicationUser() { UserName = model.UserName };
//                var result = await UserManager.CreateAsync(user, model.Password);
//                if (result.Succeeded)
//                {
//                    await SignInAsync(user, isPersistent: false);
//                    return RedirectToAction("Index", "Home");
//                }
//                else
//                {
//                    AddErrors(result);
//                }
//            }

//            // If we got this far, something failed, redisplay form
//            return View(model);
//        }

//        //
//        // POST: /Account/Disassociate
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
//        {
//            ManageMessageId? message = null;
//            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
//            if (result.Succeeded)
//            {
//                message = ManageMessageId.RemoveLoginSuccess;
//            }
//            else
//            {
//                message = ManageMessageId.Error;
//            }
//            return RedirectToAction("Manage", new { Message = message });
//        }

//        //
//        // GET: /Account/Manage
//        public ActionResult Manage(ManageMessageId? message)
//        {
//            ViewBag.StatusMessage =
//                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
//                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
//                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
//                : message == ManageMessageId.Error ? "An error has occurred."
//                : "";
//            ViewBag.HasLocalPassword = HasPassword();
//            ViewBag.ReturnUrl = Url.Action("Manage");
//            return View();
//        }

//        //
//        // POST: /Account/Manage
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Manage(ManageUserViewModel model)
//        {
//            bool hasPassword = HasPassword();
//            ViewBag.HasLocalPassword = hasPassword;
//            ViewBag.ReturnUrl = Url.Action("Manage");
//            if (hasPassword)
//            {
//                if (ModelState.IsValid)
//                {
//                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
//                    if (result.Succeeded)
//                    {
//                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
//                    }
//                    else
//                    {
//                        AddErrors(result);
//                    }
//                }
//            }
//            else
//            {
//                // User does not have a password so remove any validation errors caused by a missing OldPassword field
//                ModelState state = ModelState["OldPassword"];
//                if (state != null)
//                {
//                    state.Errors.Clear();
//                }

//                if (ModelState.IsValid)
//                {
//                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
//                    if (result.Succeeded)
//                    {
//                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
//                    }
//                    else
//                    {
//                        AddErrors(result);
//                    }
//                }
//            }

//            // If we got this far, something failed, redisplay form
//            return View(model);
//        }

//        //
//        // POST: /Account/ExternalLogin
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public ActionResult ExternalLogin(string provider, string returnUrl)
//        {
//            // Request a redirect to the external login provider
//            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
//        }

//        //
//        // GET: /Account/ExternalLoginCallback
//        [AllowAnonymous]
//        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
//        {
//            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
//            if (loginInfo == null)
//            {
//                return RedirectToAction("Login");
//            }

//            // Sign in the user with this external login provider if the user already has a login
//            var user = await UserManager.FindAsync(loginInfo.Login);
//            if (user != null)
//            {
//                await SignInAsync(user, isPersistent: false);
//                return RedirectToLocal(returnUrl);
//            }
//            else
//            {
//                // If the user does not have an account, then prompt the user to create an account
//                ViewBag.ReturnUrl = returnUrl;
//                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
//                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
//            }
//        }

//        //
//        // POST: /Account/LinkLogin
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult LinkLogin(string provider)
//        {
//            // Request a redirect to the external login provider to link a login for the current user
//            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
//        }

//        //
//        // GET: /Account/LinkLoginCallback
//        public async Task<ActionResult> LinkLoginCallback()
//        {
//            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
//            if (loginInfo == null)
//            {
//                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
//            }
//            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
//            if (result.Succeeded)
//            {
//                return RedirectToAction("Manage");
//            }
//            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
//        }

//        //
//        // POST: /Account/ExternalLoginConfirmation
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
//        {
//            if (User.Identity.IsAuthenticated)
//            {
//                return RedirectToAction("Manage");
//            }

//            if (ModelState.IsValid)
//            {
//                // Get the information about the user from the external login provider
//                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
//                if (info == null)
//                {
//                    return View("ExternalLoginFailure");
//                }
//                var user = new ApplicationUser() { UserName = model.UserName };
//                var result = await UserManager.CreateAsync(user);
//                if (result.Succeeded)
//                {
//                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
//                    if (result.Succeeded)
//                    {
//                        await SignInAsync(user, isPersistent: false);
//                        return RedirectToLocal(returnUrl);
//                    }
//                }
//                AddErrors(result);
//            }

//            ViewBag.ReturnUrl = returnUrl;
//            return View(model);
//        }

//        //
//        // POST: /Account/LogOff
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult LogOff()
//        {
//            AuthenticationManager.SignOut();
//            return RedirectToAction("Index", "Home");
//        }

//        //
//        // GET: /Account/ExternalLoginFailure
//        [AllowAnonymous]
//        public ActionResult ExternalLoginFailure()
//        {
//            return View();
//        }

//        [ChildActionOnly]
//        public ActionResult RemoveAccountList()
//        {
//            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
//            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
//            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && UserManager != null)
//            {
//                UserManager.Dispose();
//                UserManager = null;
//            }
//            base.Dispose(disposing);
//        }

//        #region Helpers
//        // Used for XSRF protection when adding external logins
//        private const string XsrfKey = "XsrfId";

//        private IAuthenticationManager AuthenticationManager
//        {
//            get
//            {
//                return HttpContext.GetOwinContext().Authentication;
//            }
//        }

//        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
//        {
//            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
//            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
//            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
//        }

//        private void AddErrors(IdentityResult result)
//        {
//            foreach (var error in result.Errors)
//            {
//                ModelState.AddModelError("", error);
//            }
//        }

//        private bool HasPassword()
//        {
//            var user = UserManager.FindById(User.Identity.GetUserId());
//            if (user != null)
//            {
//                return user.PasswordHash != null;
//            }
//            return false;
//        }

//        public enum ManageMessageId
//        {
//            ChangePasswordSuccess,
//            SetPasswordSuccess,
//            RemoveLoginSuccess,
//            Error
//        }

//        private ActionResult RedirectToLocal(string returnUrl)
//        {
//            if (Url.IsLocalUrl(returnUrl))
//            {
//                return Redirect(returnUrl);
//            }
//            else
//            {
//                return RedirectToAction("Index", "Home");
//            }
//        }

//        private class ChallengeResult : HttpUnauthorizedResult
//        {
//            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
//            {
//            }

//            public ChallengeResult(string provider, string redirectUri, string userId)
//            {
//                LoginProvider = provider;
//                RedirectUri = redirectUri;
//                UserId = userId;
//            }

//            public string LoginProvider { get; set; }
//            public string RedirectUri { get; set; }
//            public string UserId { get; set; }

//            public override void ExecuteResult(ControllerContext context)
//            {
//                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
//                if (UserId != null)
//                {
//                    properties.Dictionary[XsrfKey] = UserId;
//                }
//                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
//            }
//        }
//        #endregion
//    }
//}

#region Namespaces
using System;
using System.Web.Mvc;
using System.Web.Security;
using Vserv.Accounting.Business.Managers;
using Vserv.Accounting.Core.Services;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Web.Models;
using WebMatrix.WebData;
#endregion

namespace Vserv.Accounting.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Variables
        private readonly IUsersService usersService;
        private readonly IEmailService emailService;
        #endregion

        #region Constructor
        public AccountController(IUsersService usersService, IEmailService emailService)
        {
            this.usersService = usersService;
            this.emailService = emailService;
        }
        #endregion

        #region Login

        //
        // GET: /Account/Login/
        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Account/Login
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

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
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

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #endregion Register

        #region Change Password

        //ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

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

        #region User Profile


        public ActionResult Profile()
        {
            AccountManager _accountManager = new AccountManager();
            UserProfile userProfile = _accountManager.GetUserProfile(User.Identity.Name);
            UserProfileModel userProfileModel = ConvertTo(userProfile);
            return View(userProfileModel);
        }


        #endregion

        #region Logout

        // [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Account");
        }

        public ActionResult BadLink()
        {
            return View();
        }

        //
        // GET: /Account/ChangePassword/Success

        public ActionResult SuccessChangePassword()
        {
            return View();
        }

        #endregion Logout

        #region Helpers

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
