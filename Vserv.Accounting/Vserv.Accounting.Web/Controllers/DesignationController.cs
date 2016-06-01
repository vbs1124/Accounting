#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vserv.Accounting.Business.Managers;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Web.Models;
using Vserv.Accounting.Common;

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
        public EmployeeManager _employeeManager;

        public DesignationController()
        {
            _employeeManager = new EmployeeManager();
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
            
            var designations = _employeeManager.GetDesignations();
            var result = ConvertToSelectListItem(designations.Where(condition => condition.IsActive).ToList());
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Add(string designationName)
        {
            
            Designation designation = new Data.Entity.Designation
            {
                //Code = "",
                Name = designationName,
                Description = designationName,
                DisplayOrder = 0,
                IsActive = true,
                CreatedBy = User.Identity.Name,
                CreatedDate = DateTime.Now,
            };

            _employeeManager.AddDesignation(designation, User.Identity.Name);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(string designationName, int designationId)
        {
            

            var existinDesignation = _employeeManager.GetDesignation(designationId);
            if (existinDesignation.IsNotNull() && !String.IsNullOrWhiteSpace(designationName.Trim()))
            {
                existinDesignation.Name = designationName;
                _employeeManager.UpdateDesignation(existinDesignation, User.Identity.Name);
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DesignationWindow()
        {
            
            var designations = _employeeManager.GetDesignations();

            if (designations.IsNotNull())
            {
                ViewBag.Designations = ConvertTo(designations.Where(condition => condition.IsActive).ToList());
            }

            return PartialView("_DesignationWindow");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Converts to.
        /// </summary>
        /// <param name="designation">The designation.</param>
        /// <returns></returns>
        private Designation ConvertTo(DesignationModel designation)
        {
            return new Designation
            {
                DesignationId = designation.DesignationId,
                Code = designation.Code,
                Name = designation.Name,
                Description = designation.Description,
                DisplayOrder = designation.DisplayOrder,
                IsActive = designation.IsActive,
                CreatedBy = designation.CreatedBy,
                UpdatedBy = designation.UpdatedBy,
                CreatedDate = designation.CreatedDate,
                UpdatedDate = designation.UpdatedDate
            };
        }

        /// <summary>
        /// Converts to.
        /// </summary>
        /// <param name="designation">The designation.</param>
        /// <returns></returns>
        private DesignationModel ConvertTo(Designation designation)
        {
            return new DesignationModel
            {
                DesignationId = designation.DesignationId,
                Code = designation.Code,
                Name = designation.Name,
                Description = designation.Description,
                DisplayOrder = designation.DisplayOrder,
                IsActive = designation.IsActive,
                CreatedBy = designation.CreatedBy,
                UpdatedBy = designation.UpdatedBy,
                CreatedDate = designation.CreatedDate,
                UpdatedDate = designation.UpdatedDate
            };
        }

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