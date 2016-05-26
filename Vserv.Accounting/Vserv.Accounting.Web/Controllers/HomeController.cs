#region Namespaces

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Vserv.Accounting.Common;

#endregion

namespace Vserv.Accounting.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class HomeController : Controller
    {
        #region Action Methods

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("index", "dashboard");
            }

            return RedirectToAction("index", "account");
        }

        /// <summary>
        /// Successes the specified success message.
        /// </summary>
        /// <param name="successMessage">The success message.</param>
        /// <returns></returns>
        [Route("success/{successMessage}")]
        public ActionResult Success(string successMessage)
        {
            Dictionary<String, String> encryptString = new Dictionary<String, String>();
            encryptString.ToDecryptedString(successMessage);
            ViewBag.SuccessMessage = encryptString[CommonConstants.MESSAGE];
            return View();
        }

        /// <summary>
        /// Headers this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Header()
        {
            return PartialView("_Header");
        }

        #endregion
    }
}
