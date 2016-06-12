#region Namespaces

using System.Web.Mvc;
using Vserv.Accounting.Business.Managers;
using Vserv.Accounting.Web.Models;

#endregion

namespace Vserv.Accounting.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [Authorize]
    public class DashboardController : ViewControllerBase
    {
        public HomeManager HomeManager;

        public DashboardController()
        {
            HomeManager = new HomeManager();
        }

        #region Action Methods
        //
        // GET: /Dashboard/
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            DashboardModel dashboardModel = new DashboardModel {EmployeeCount = HomeManager.GetEmployeeCount()};
            return View(dashboardModel);
        }
        #endregion
    }
}
