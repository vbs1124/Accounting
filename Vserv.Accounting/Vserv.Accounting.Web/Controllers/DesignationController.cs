#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vserv.Accounting.Business.Managers;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Web.Models;
using Vserv.Common.Extensions;
#endregion

namespace Vserv.Accounting.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [Authorize]
    public class DesignationController : Controller
    {
        #region Action Methods

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            EmployeeManager _employeeManager = new EmployeeManager();
            List<DesignationModel> designations = ConvertTo(_employeeManager.GetDesignations());
            return View(designations);
        }

        /// <summary>
        /// Adds this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Adds the specified designation.
        /// </summary>
        /// <param name="designation">The designation.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(DesignationModel designation)
        {
            if (ModelState.IsValid)
            {
                EmployeeManager _employeeManager = new EmployeeManager();
                _employeeManager.AddDesignation(ConvertTo(designation), User.Identity.Name);
                return RedirectToAction("Success", "Home", new { successMessage = "Designation added successfully." });
            }

            return View(designation);
        }

        /// <summary>
        /// Edits the specified designation identifier.
        /// </summary>
        /// <param name="designationId">The designation identifier.</param>
        /// <returns></returns>
        public ActionResult Edit(int designationId)
        {
            EmployeeManager _employeeManager = new EmployeeManager();
            Designation designation = _employeeManager.GetDesignation(designationId);
            return View(ConvertTo(designation));
        }

        /// <summary>
        /// Edits the specified designation.
        /// </summary>
        /// <param name="designation">The designation.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(DesignationModel designation)
        {
            if (ModelState.IsValid)
            {
                EmployeeManager _employeeManager = new EmployeeManager();
                _employeeManager.UpdateDesignation(ConvertTo(designation), User.Identity.Name);
                return RedirectToAction("Success", "Home", new { successMessage = "Designation updated successfully." });
            }

            return View(designation);
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

        /// <summary>
        /// Converts to.
        /// </summary>
        /// <param name="designations">The designations.</param>
        /// <returns></returns>
        private List<DesignationModel> ConvertTo(List<Designation> designations)
        {
            List<DesignationModel> designationModels = new List<DesignationModel>();

            if (designations.IsNotNull())
            {
                designations.ForEach(designation => designationModels.Add(new DesignationModel
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
                }));
            }
            return designationModels;
        }

        #endregion
    }
}