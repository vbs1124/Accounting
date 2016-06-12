#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vserv.Accounting.Business.Managers;
using Vserv.Accounting.Common;
using Vserv.Accounting.Data.Entity;

#endregion

namespace Vserv.Accounting.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [Authorize]
    public class DesignationController : ViewControllerBase
    {
        public EmployeeManager EmployeeManager;

        public DesignationController()
        {
            EmployeeManager = new EmployeeManager();
        }

        #region Action Methods

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //
            //List<DesignationModel> designations = ConvertTo(_employeeManager.GetDesignations());
            return View();
        }

        [HttpGet]
        public ActionResult GetDesignations()
        {
            
            var designations = EmployeeManager.GetDesignations();
            var result = ConvertToSelectListItem(designations.Where(condition => condition.IsActive).ToList());
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Add(string designationName)
        {
            
            Designation designation = new Designation
            {
                //Code = "",
                Name = designationName,
                Description = designationName,
                DisplayOrder = 0,
                IsActive = true,
                CreatedBy = User.Identity.Name,
                CreatedDate = DateTime.Now,
            };

            EmployeeManager.AddDesignation(designation, User.Identity.Name);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(string designationName, int designationId)
        {
            

            var existinDesignation = EmployeeManager.GetDesignation(designationId);
            if (existinDesignation.IsNotNull() && !String.IsNullOrWhiteSpace(designationName.Trim()))
            {
                existinDesignation.Name = designationName;
                EmployeeManager.UpdateDesignation(existinDesignation, User.Identity.Name);
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DesignationWindow()
        {
            
            var designations = EmployeeManager.GetDesignations();

            if (designations.IsNotNull())
            {
                ViewBag.Designations = ConvertTo(designations.Where(condition => condition.IsActive).ToList());
            }

            return PartialView("_DesignationWindow");
        }

        #endregion

        #region Private Methods

        private List<SelectListItem> ConvertTo(List<Designation> designations)
        {
            var result = new List<SelectListItem>();

            if (designations.IsNotNull())
            {
                designations.ForEach(item => result.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.DesignationId.ToString()
                }));
            }

            return result;
        }

        private List<SelectListItem> ConvertToSelectListItem(List<Designation> designations)
        {
            var result = new List<SelectListItem>();

            if (designations.IsNotNull())
            {
                designations.ForEach(item => result.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.DesignationId.ToString()
                }));
            }

            return result;
        }

        #endregion
    }
}