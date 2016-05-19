#region Namespaces
using System.Web.Mvc;
using Vserv.Accounting.Business.Managers;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Web.Models;
#endregion

namespace Vserv.Accounting.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [Authorize]
    public class DashboardController : Controller
    {
        #region Action Methods
        //
        // GET: /Dashboard/
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            HomeManager _homeManager = new HomeManager();
            DashboardModel dashboardModel = new DashboardModel();
            dashboardModel.EmployeeCount = _homeManager.GetEmployeeCount();
            return View(dashboardModel);
        }
        #endregion
    }
}
