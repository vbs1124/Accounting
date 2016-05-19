#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vserv.Accounting.Business.Managers;
using Vserv.Accounting.Common.Enums;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Web.Models;
using Vserv.Common.Extensions;
using System.Collections;
#endregion

namespace Vserv.Accounting.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [Authorize]
    public class EmployeeController : Controller
    {
        #region Action Methods

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            EmployeeManager _employeeManager = new EmployeeManager();
            var employees = _employeeManager.GetEmployees();
            return View(employees);
        }

        /// <summary>
        /// Adds this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            EmployeeManager _employeeManager = new EmployeeManager();
            EmployeeModel employeeModel = new EmployeeModel();
            SetDropdownValues();
            return View(employeeModel);
        }

        /// <summary>
        /// Adds the specified employee model.
        /// </summary>
        /// <param name="employeeModel">The employee model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(EmployeeModel employeeModel)
        {
            employeeModel.CreatedBy = User.Identity.Name;
            employeeModel.CreatedDate = DateTime.Now;
            EmployeeManager _employeeManager = new EmployeeManager();

            // Perform Save for Employee
            if (ModelState.IsValid)
            {
                Employee employee = ConvertTo(employeeModel);
                _employeeManager.AddEmployee(employee);
                return RedirectToAction("Success", "Home", new { successMessage = "Employee added successfully." });
            }
            ModelState.AddModelError("emp_errors", "Please fill the required fields...");
            SetDropdownValues();
            return View(employeeModel);
        }

        /// <summary>
        /// Edits the specified employee identifier.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public ActionResult Edit(int employeeId)
        {
            EmployeeManager _employeeManager = new EmployeeManager();
            var employee = _employeeManager.GetEmployee(employeeId);
            EmployeeModel employeeModel = ConvertTo(employee);
            SetDropdownValues();
            return View(employeeModel);
        }

        /// <summary>
        /// Edits the specified employee model.
        /// </summary>
        /// <param name="employeeModel">The employee model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(EmployeeModel employeeModel)
        {
            EmployeeManager _employeeManager = new EmployeeManager();
            employeeModel.UpdatedBy = User.Identity.Name;
            employeeModel.UpdatedDate = DateTime.Now;
            // Perform Save for Employee
            if (ModelState.IsValid)
            {
                Employee employee = ConvertTo(employeeModel);
                _employeeManager.EditEmployee(employee);
                return RedirectToAction("Success", "Home", new { successMessage = "Employee updated successfully." });
            }

            SetDropdownValues();
            return View(employeeModel);
        }

        /// <summary>
        /// Deletes the specified employee identifier.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public ActionResult Delete(int employeeId)
        {
            EmployeeManager _employeeManager = new EmployeeManager();
            _employeeManager.DeleteEmployee(employeeId);
            return RedirectToAction("List");
        }

        /// <summary>
        /// Bankings this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Banking()
        {
            return View();
        }

        /// <summary>
        /// Taxations this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Taxation()
        {
            return View();
        }

        /// <summary>
        /// Salaries this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Salary()
        {
            return View();
        }

        #region dropdownlist

        /// <summary>
        /// Get a list of designations.
        /// </summary>
        /// <returns></returns>
        public List<DesignationModel> GetDesignations()
        {
            EmployeeManager _employeeManager = new EmployeeManager();
            var designations = _employeeManager.GetDesignations();
            List<DesignationModel> designationModels = new List<DesignationModel>();

            if (designations.IsNotNull())
            {
                designationModels = ConvertToDesignationModel(designations.Where(condition => condition.IsActive).ToList());
            }

            return designationModels;
        }

        private List<DesignationModel> ConvertToDesignationModel(List<Designation> list)
        {
            List<DesignationModel> designationModels = new List<DesignationModel>();

            list.ForEach(desg => designationModels.Add(new DesignationModel
            {
                DesignationId = desg.DesignationId,
                Code = desg.Code,
                Name = desg.Name,
                Description = desg.Description,
                DisplayOrder = desg.DisplayOrder,
                IsActive = desg.IsActive,
                CreatedBy = desg.CreatedBy,
                UpdatedBy = desg.UpdatedBy,
                CreatedDate = desg.CreatedDate,
                UpdatedDate = desg.UpdatedDate
            }));

            return designationModels;
        }

        public JsonResult GetOfficeBranches()
        {
            EmployeeManager _employeeManager = new EmployeeManager();
            var officeBranches = _employeeManager.GetOfficeBranches();
            IEnumerable<SelectListItem> officeBranchesSelectListItems = null;

            if (officeBranches.IsNotNull())
            {
                officeBranchesSelectListItems = ConvertTo(officeBranches.Where(condition => condition.IsActive).ToList());
            }

            return Json(new SelectList(officeBranchesSelectListItems, "Value", "Text", "1"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSalutations()
        {
            EmployeeManager _employeeManager = new EmployeeManager();
            var officeBranches = _employeeManager.GetSalutations();
            IEnumerable<SelectListItem> officeBranchesSelectListItems = null;

            var salutations = _employeeManager.GetSalutations();

            if (salutations.IsNotNull())
            {
                officeBranchesSelectListItems = ConvertTo(salutations.Where(condition => condition.IsActive).ToList());
            }

            return Json(new SelectList(officeBranchesSelectListItems, "Value", "Text", "1"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStates()
        {
            EmployeeManager _employeeManager = new EmployeeManager();
            var officeBranches = _employeeManager.GetSalutations();
            IEnumerable<SelectListItem> statesSelectListItems = null;

            var states = _employeeManager.GetStates();

            if (states.IsNotNull())
            {
                statesSelectListItems = ConvertTo(states.Where(condition => condition.IsActive).ToList());
            }

            return Json(new SelectList(statesSelectListItems, "Value", "Text", "1"), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the genders.
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> GetGenders()
        {
            var genders = new List<SelectListItem>();
            genders.Add(new SelectListItem { Text = "Unknown", Value = "0" });
            genders.Add(new SelectListItem { Text = "Male", Value = "1" });
            genders.Add(new SelectListItem { Text = "Female", Value = "2" });

            return genders;
        }

        /// <summary>
        /// Converts to.
        /// </summary>
        /// <param name="cities">The cities.</param>
        /// <returns></returns>
        private List<CityModel> ConvertTo(List<City> cities)
        {
            var result = new List<CityModel>();

            if (cities.IsNotNull())
            {
                cities.ForEach(city => result.Add(new CityModel
                {
                    CityId = city.CityId,
                    StateId = city.StateId,
                    Name = city.Name,
                    Description = city.Description,
                    DisplayOrder = city.DisplayOrder,
                    IsActive = city.IsActive,
                    CreatedBy = city.CreatedBy,
                    UpdatedBy = city.UpdatedBy,
                    CreatedDate = city.CreatedDate,
                    UpdatedDate = city.UpdatedDate,
                }));
            }

            return result;
        }

        /// <summary>
        /// Converts to.
        /// </summary>
        /// <param name="states">The states.</param>
        /// <returns></returns>
        private List<SelectListItem> ConvertTo(List<State> states)
        {
            List<SelectListItem> stateModelList = new List<SelectListItem>();
            if (states.IsNotNull())
            {
                states.ForEach(item => stateModelList.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.StateId.ToString(),
                }));
            }

            return stateModelList;
        }

        /// <summary>
        /// Converts to.
        /// </summary>
        /// <param name="salutations">The salutations.</param>
        /// <returns></returns>
        private List<SelectListItem> ConvertTo(List<Salutation> salutations)
        {
            var result = new List<SelectListItem>();

            if (salutations.IsNotNull())
            {
                salutations.ForEach(item => result.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.SalutationId.ToString(),
                }));
            }

            return result;
        }

        /// <summary>
        /// Converts to.
        /// </summary>
        /// <param name="officeBranches">The office branches.</param>
        /// <returns></returns>
        private List<SelectListItem> ConvertTo(List<OfficeBranch> officeBranches)
        {
            var result = new List<SelectListItem>();

            if (officeBranches.IsNotNull())
            {
                officeBranches.ForEach(item => result.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.OfficeBranchId.ToString(),
                }));
            }

            return result;
        }

        /// <summary>
        /// Converts to.
        /// </summary>
        /// <param name="departments">The departments.</param>
        /// <returns></returns>
        private List<DepartmentModel> ConvertTo(List<Department> departments)
        {
            var result = new List<DepartmentModel>();

            if (departments.IsNotNull())
            {
                departments.ForEach(item => result.Add(new DepartmentModel
                {
                    DepartmentId = item.DepartmentId,
                    Code = item.Code,
                    Name = item.Name,
                    Description = item.Description,
                    DisplayOrder = item.DisplayOrder,
                    IsActive = item.IsActive,
                    CreatedBy = item.CreatedBy,
                    UpdatedBy = item.UpdatedBy,
                    CreatedDate = item.CreatedDate,
                    UpdatedDate = item.UpdatedDate,
                }));
            }

            return result;
        }

        /// <summary>
        /// Converts to.
        /// </summary>
        /// <param name="addressTypes">The address types.</param>
        /// <returns></returns>
        private List<AddressTypeModel> ConvertTo(List<AddressType> addressTypes)
        {
            var result = new List<AddressTypeModel>();

            if (addressTypes.IsNotNull())
            {
                addressTypes.ForEach(item => result.Add(new AddressTypeModel
                {
                    AddressTypeId = item.AddressTypeId,
                    Code = item.Code,
                    Name = item.Name,
                    Description = item.Description,
                    DisplayOrder = item.DisplayOrder,
                    IsActive = item.IsActive,
                    CreatedBy = item.CreatedBy,
                    UpdatedBy = item.UpdatedBy,
                    CreatedDate = item.CreatedDate,
                    UpdatedDate = item.UpdatedDate,
                }));
            }

            return result;
        }

        /// <summary>
        /// Converts to.
        /// </summary>
        /// <param name="employeeModel">The employee model.</param>
        /// <returns></returns>
        private Employee ConvertTo(EmployeeModel employeeModel)
        {
            if (employeeModel.IsNull())
            {
                return new Employee();
            }
            List<EmployeeAddress> addresses = new List<EmployeeAddress>();

            if (employeeModel.PermanentAddress.IsNotNull())
            {
                Address permanentAddress = new Address
                {
                    AddressId = employeeModel.PermanentAddress.AddressId,
                    Address1 = employeeModel.PermanentAddress.Address1,
                    Address2 = employeeModel.PermanentAddress.Address2,
                    CountryId = null,
                    StateId = employeeModel.PermanentAddress.StateId,
                    CityId = null,
                    ZipCodeId = null,
                    City = employeeModel.PermanentAddress.City,
                    ZipCode = employeeModel.PermanentAddress.ZipCode,
                    Latitude = employeeModel.PermanentAddress.Latitude,
                    Longitude = employeeModel.PermanentAddress.Longitude,
                    IsCommunicationAddress = false,
                    AddressTypeId = Convert.ToInt32(AddressTypeEnum.PermanentAddress),
                    IsActive = true,
                    CreatedBy = User.Identity.Name,
                    UpdatedBy = null,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = null
                };
                addresses.Add(new EmployeeAddress { Address = permanentAddress });
            }

            if (employeeModel.MailingAddress.IsNotNull())
            {
                Address mailingAddress = new Address
                {
                    AddressId = employeeModel.MailingAddress.AddressId,
                    Address1 = employeeModel.MailingAddress.Address1,
                    Address2 = employeeModel.MailingAddress.Address2,
                    CountryId = null,
                    StateId = employeeModel.MailingAddress.StateId,
                    CityId = null,
                    ZipCodeId = null,
                    City = employeeModel.MailingAddress.City,
                    ZipCode = employeeModel.MailingAddress.ZipCode,
                    Latitude = employeeModel.MailingAddress.Latitude,
                    Longitude = employeeModel.MailingAddress.Longitude,
                    IsCommunicationAddress = false,
                    AddressTypeId = Convert.ToInt32(AddressTypeEnum.MailingAddress),
                    IsActive = true,
                    CreatedBy = User.Identity.Name,
                    UpdatedBy = null,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = null,
                };

                addresses.Add(new EmployeeAddress { Address = mailingAddress });
            }

            return new Employee
            {
                EmployeeId = employeeModel.EmployeeId,
                FirstName = employeeModel.FirstName,
                MiddleName = employeeModel.MiddleName,
                LastName = employeeModel.LastName,
                FatherName = employeeModel.FatherName,
                MotherName = employeeModel.MotherName,
                UniversalAccountNumber = employeeModel.UniversalAccountNumber,
                PermanentAccountNumber = employeeModel.PermanentAccountNumber,
                AADHAARNumber = employeeModel.AADHAARNumber,
                MobileNumber = employeeModel.MobileNumber,
                EmailAddress = employeeModel.EmailAddress,
                BirthDay = employeeModel.BirthDay.IsNotNull() && employeeModel.BirthDay.HasValue ? employeeModel.BirthDay.Value : DateTime.Now,
                JoiningDate = employeeModel.JoiningDate.IsNotNull() && employeeModel.JoiningDate.HasValue ? employeeModel.JoiningDate.Value : DateTime.Now,
                RelievingDate = employeeModel.RelievingDate,
                VBS_Id = String.Format("vbs{0}", employeeModel.VBS_Id),
                DesignationId = employeeModel.DesignationId.Value,
                SalutationId = employeeModel.SalutationId.Value,
                GenderId = employeeModel.GenderId.Value,
                OfficeBranchId = employeeModel.OfficeBranchId.Value,
                IsActive = employeeModel.IsActive,
                CreatedBy = User.Identity.Name,
                UpdatedBy = employeeModel.UpdatedBy,
                CreatedDate = DateTime.Now,
                UpdatedDate = employeeModel.UpdatedDate,
                EmployeeAddresses = addresses
            };
        }

        /// <summary>
        /// Sets the dropdown values.
        /// </summary>
        /// <param name="employeeModel">The employee model.</param>
        private void SetDropdownValues()
        {
            EmployeeManager _employeeManager = new EmployeeManager();

            ViewBag.Genders = GetGenders();

            var designations = _employeeManager.GetDesignations();

            if (designations.IsNotNull())
            {
                ViewBag.Designations = ConvertTo(designations.Where(condition => condition.IsActive).ToList());
            }

            var officeBranches = _employeeManager.GetOfficeBranches();

            if (officeBranches.IsNotNull())
            {
                ViewBag.OfficeBranches = ConvertTo(officeBranches.Where(condition => condition.IsActive).ToList());
            }
            var salutations = _employeeManager.GetSalutations();

            if (salutations.IsNotNull())
            {
                ViewBag.Salutations = ConvertTo(salutations.Where(condition => condition.IsActive).ToList());
            }

            var states = _employeeManager.GetStates();

            if (states.IsNotNull())
            {
                ViewBag.States = ConvertTo(states.Where(condition => condition.IsActive).ToList());
            }
        }

        /// <summary>
        /// Converts to.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        private EmployeeModel ConvertTo(Employee employee)
        {
            AddressModel permanentAddress = null;
            AddressModel mailingAddress = null;

            foreach (var item in employee.EmployeeAddresses)
            {
                if (item.Address.IsNotNull() && item.Address.AddressTypeId.Equals(Convert.ToInt32(AddressTypeEnum.PermanentAddress)))
                {
                    permanentAddress = new AddressModel
                    {
                        AddressId = item.Address.AddressId,
                        Address1 = item.Address.Address1,
                        Address2 = item.Address.Address2,
                        CountryId = item.Address.CountryId,
                        StateId = item.Address.StateId,
                        City = item.Address.City,
                        ZipCode = item.Address.ZipCode,
                        Latitude = item.Address.Latitude,
                        Longitude = item.Address.Longitude,
                        IsCommunicationAddress = item.Address.IsCommunicationAddress,
                        AddressTypeId = item.Address.AddressTypeId,
                        IsActive = item.Address.IsActive,
                        CreatedBy = item.Address.CreatedBy,
                        UpdatedBy = item.Address.UpdatedBy,
                        CreatedDate = item.Address.CreatedDate,
                        UpdatedDate = item.Address.UpdatedDate,
                    };
                }

                if (item.Address.IsNotNull() && item.Address.AddressTypeId.Equals(Convert.ToInt32(AddressTypeEnum.MailingAddress)))
                {
                    mailingAddress = new AddressModel
                    {
                        AddressId = item.Address.AddressId,
                        Address1 = item.Address.Address1,
                        Address2 = item.Address.Address2,
                        CountryId = item.Address.CountryId,
                        StateId = item.Address.StateId,
                        City = item.Address.City,
                        ZipCode = item.Address.ZipCode,
                        Latitude = item.Address.Latitude,
                        Longitude = item.Address.Longitude,
                        IsCommunicationAddress = item.Address.IsCommunicationAddress,
                        AddressTypeId = item.Address.AddressTypeId,
                        IsActive = item.Address.IsActive,
                        CreatedBy = item.Address.CreatedBy,
                        UpdatedBy = item.Address.UpdatedBy,
                        CreatedDate = item.Address.CreatedDate,
                        UpdatedDate = item.Address.UpdatedDate,
                    };
                }
            }

            return new EmployeeModel
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                FatherName = employee.FatherName,
                MotherName = employee.MotherName,
                UniversalAccountNumber = employee.UniversalAccountNumber,
                PermanentAccountNumber = employee.PermanentAccountNumber,
                AADHAARNumber = employee.AADHAARNumber,
                MobileNumber = employee.MobileNumber,
                EmailAddress = employee.EmailAddress,
                BirthDay = employee.BirthDay,
                JoiningDate = employee.JoiningDate,
                RelievingDate = employee.RelievingDate,
                VBS_Id = employee.VBS_Id.Replace("vbs", ""),
                DesignationId = employee.DesignationId,
                SalutationId = employee.SalutationId,
                GenderId = employee.GenderId,
                OfficeBranchId = employee.OfficeBranchId,
                IsActive = employee.IsActive,
                CreatedBy = employee.CreatedBy,
                UpdatedBy = employee.UpdatedBy,
                CreatedDate = employee.CreatedDate,
                UpdatedDate = employee.UpdatedDate,
                PermanentAddress = permanentAddress,
                MailingAddress = mailingAddress
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

        #endregion Private Methods
    }
}