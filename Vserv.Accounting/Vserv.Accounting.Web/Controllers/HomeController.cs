#region Namespaces
using System.Web.Mvc;
#endregion

namespace Vserv.Accounting.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Action Methods

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("index", "dashboard");
            }

            return RedirectToAction("index", "account");
        }

        public ActionResult Success(string successMessage)
        {
            ViewBag.SuccessMessage = successMessage;
            return View();
        }

        public ActionResult Header()
        {
            return PartialView("_Header");
        }

        #endregion
    }
}
