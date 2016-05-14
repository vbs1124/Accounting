using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vserv.Accounting.Business.Managers;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Web.Models;
using Vserv.Common.Extensions;

namespace Vserv.Accounting.Web.Controllers
{
    [Authorize]
    public class DesignationController : Controller
    {
        #region Action Methods

        public ActionResult Index()
        {
            EmployeeManager _employeeManager = new EmployeeManager();
            List<DesignationModel> designations = ConvertTo(_employeeManager.GetDesignations());
            return View(designations);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(DesignationModel designation)
        {
            designation.CreatedById = 1;
            designation.CreatedDate = DateTime.Now;
            designation.DisplayOrder = 0;

            if (ModelState.IsValid)
            {
                EmployeeManager _employeeManager = new EmployeeManager();
                _employeeManager.AddDesignation(ConvertTo(designation));
                return RedirectToAction("Success", "Home", new { successMessage = "Designation added successfully." });
            }

            return View(designation);
        }

        public ActionResult Edit(int designationId)
        {
            EmployeeManager _employeeManager = new EmployeeManager();
            Designation designation = _employeeManager.GetDesignation(designationId);
            return View(ConvertTo(designation));
        }

        [HttpPost]
        public ActionResult Edit(DesignationModel designation)
        {
            designation.UpdatedById = 1;
            designation.UpdatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                EmployeeManager _employeeManager = new EmployeeManager();
                _employeeManager.UpdateDesignation(ConvertTo(designation));
                return RedirectToAction("Success", "Home", new { successMessage = "Designation updated successfully." });
            }

            return View(designation);
        }

        #endregion

        #region Private Methods

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
                CreatedById = designation.CreatedById,
                UpdatedById = designation.UpdatedById,
                CreatedDate = designation.CreatedDate,
                UpdatedDate = designation.UpdatedDate
            };
        }

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
                CreatedById = designation.CreatedById,
                UpdatedById = designation.UpdatedById,
                CreatedDate = designation.CreatedDate,
                UpdatedDate = designation.UpdatedDate
            };
        }

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
                    CreatedById = designation.CreatedById,
                    UpdatedById = designation.UpdatedById,
                    CreatedDate = designation.CreatedDate,
                    UpdatedDate = designation.UpdatedDate
                }));
            }
            return designationModels;
        }

        #endregion
    }
}