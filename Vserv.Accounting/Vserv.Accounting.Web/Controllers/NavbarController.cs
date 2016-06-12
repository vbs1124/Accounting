#region Namespaces

using System.Web.Mvc;
using Vserv.Accounting.Business.Managers;

#endregion

namespace Vserv.Accounting.Web.Controllers
{
    public class NavbarController : ViewControllerBase
    {
        #region Namespaces
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            HomeManager homeManager = new HomeManager();
            return PartialView("_Navbar", homeManager.GetFeatures(true));
        }

        public JsonResult GetFeatures()
        {
            HomeManager homeManager = new HomeManager();
            var features = homeManager.GetFeatures(true);
            return CustomJson(features.ToArray());

        }
        ///// <summary>
        ///// Navbars the items.
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerable<Navbar> navbarItems()
        //{
        //    var menu = new List<Navbar>();
        //    menu.Add(new Navbar
        //    {
        //        Id = 1,
        //        nameOption = "Dashboard",
        //        controller = "dashboard",
        //        action = "index",
        //        imageClass = "fa fa-dashboard fa-fw",
        //        status = true,
        //        isParent = false,
        //        parentId = 0
        //    });

        //    menu.Add(new Navbar
        //    {
        //        Id = 2,
        //        nameOption = "Manage Employees",
        //        imageClass = "fa fa-users fa-fw",
        //        status = true,
        //        isParent = true,
        //        parentId = 0
        //    });
        //    menu.Add(new Navbar
        //    {
        //        Id = 3,
        //        nameOption = "View Employees",
        //        controller = "employee",
        //        action = "list",
        //        status = true,
        //        isParent = false,
        //        parentId = 2
        //    });
        //    menu.Add(new Navbar
        //    {
        //        Id = 4,
        //        nameOption = "Add Employee",
        //        controller = "employee",
        //        action = "add",
        //        status = true,
        //        isParent = false,
        //        parentId = 2
        //    });

        //    //menu.Add(new Navbar
        //    //{
        //    //    Id = 5,
        //    //    nameOption = "Manage Designations",
        //    //    imageClass = "fa fa-users fa-fw",
        //    //    status = true,
        //    //    isParent = true,
        //    //    parentId = 0
        //    //});
        //    //menu.Add(new Navbar
        //    //{
        //    //    Id = 6,
        //    //    nameOption = "View Designations",
        //    //    controller = "designation",
        //    //    action = "index",
        //    //    status = true,
        //    //    isParent = false,
        //    //    parentId = 5
        //    //});
        //    //menu.Add(new Navbar
        //    //{
        //    //    Id = 7,
        //    //    nameOption = "Add Designation",
        //    //    controller = "designation",
        //    //    action = "add",
        //    //    status = true,
        //    //    isParent = false,
        //    //    parentId = 5
        //    //});

        //    menu.Add(new Navbar
        //    {
        //        Id = 8,
        //        nameOption = "Settings",
        //        imageClass = "fa fa-gear fa-fw",
        //        status = true,
        //        isParent = true,
        //        parentId = 0
        //    });

        //    menu.Add(new Navbar
        //    {
        //        Id = 9,
        //        nameOption = "My Profile",
        //        controller = "Account",
        //        action = "userprofile",
        //        status = true,
        //        isParent = false,
        //        parentId = 8
        //    });

        //    menu.Add(new Navbar
        //    {
        //        Id = 10,
        //        nameOption = "Change Password",
        //        controller = "Account",
        //        action = "changepassword",
        //        status = true,
        //        isParent = false,
        //        parentId = 8
        //    });

        //    return menu.ToList();
        //}

        #endregion
    }
}