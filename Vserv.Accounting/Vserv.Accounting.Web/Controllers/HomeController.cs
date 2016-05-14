//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace Vserv.Accounting.Web.Controllers
//{
//    public class HomeController : Controller
//    {
//        public ActionResult Index()
//        {
//            return View();
//        }

//        public ActionResult About()
//        {
//            ViewBag.Message = "Your application description page.";

//            return View();
//        }

//        public ActionResult Contact()
//        {
//            ViewBag.Message = "Your contact page.";

//            return View();
//        }
//    }
//}

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
                return RedirectToAction("Index", "Dashboard");
            }

            return RedirectToAction("Index", "Account");
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
