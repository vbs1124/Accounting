#region Namespaces
using System.Web.Mvc;
using Vserv.Accounting.Business.Managers;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Web.Models;
#endregion

namespace Vserv.Accounting.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        #region Action Methods
        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            return View();
        }
        #endregion
    }
}
