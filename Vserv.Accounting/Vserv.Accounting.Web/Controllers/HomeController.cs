#region Namespaces
using System.Web.Mvc;
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
        public ActionResult Success(string successMessage)
        {
            ViewBag.SuccessMessage = successMessage;
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
