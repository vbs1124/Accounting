#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vserv.Accounting.Business.Managers;
using Vserv.Accounting.Common;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Web.Models;
using System.Collections;

#endregion

namespace Vserv.Accounting.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [Authorize]
    public class EmployeeController : ViewControllerBase
    {
        public EmployeeManager _employeeManager;

        public EmployeeController()
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
            return View();
        }

        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult List(int? filterChoice)
        {
            ViewBag.EmployeFilters = GetEmployeeFilters();
            return View(GetEmployees(filterChoice));
        }

        public ActionResult GetFilteredEmployees(int? filterChoice)
        {
            return PartialView("_ViewEmployeesPartial", GetEmployees(filterChoice));
        }

        /// <summary>
        /// Adds this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
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

            // Perform Save for Employee
            if (ModelState.IsValid)
            {
                Employee employee = ConvertTo(employeeModel);
                _employeeManager.AddEmployee(employee);

                Dictionary<string, string> message = new Dictionary<string, string>();
                message.Add(CommonConstants.MESSAGE, "Employee added successfully.");
                return RedirectToAction("Success", "Home", new { successMessage = message.ToEncryptedString() });
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
        //[Route("employee/edit/{employeeId}")]
        public ActionResult Edit(int employeeId)
        {

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
        //[Route("employee/edit/")]
        public ActionResult Edit(EmployeeModel employeeModel)
        {

            employeeModel.UpdatedBy = User.Identity.Name;
            employeeModel.UpdatedDate = DateTime.Now;

            // Perform Save for Employee
            if (ModelState.IsValid)
            {
                Employee employee = ConvertTo(employeeModel);

                // Archive Existing Employee information before going for an update.
                _employeeManager.ArchiveEmployee(employeeModel.EmployeeId, User.Identity.Name);

                _employeeManager.EditEmployee(employee);
                Dictionary<string, string> message = new Dictionary<string, string>();
                message.Add(CommonConstants.MESSAGE, "Employee updated successfully.");
                string encryptedMessage = message.ToEncryptedString();
                return RedirectToAction("Success", "Home", new { successMessage = encryptedMessage });
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

        public ActionResult EmployeeHistory(int employeeId)
        {
            List<EmployeeArchive> employees = _employeeManager.GetEmployeeHistory(employeeId);
            return PartialView("_employeehistory", employees);
        }

        [Route("employee/{employeeArchiveId}/compare")]
        public ActionResult EmployeeCompareResult(int employeeArchiveId)
        {
            SetDropdownValues();
            CompareEmployeeModel compareEmployeeModel = _employeeManager.GetMatchingEmployeeInformation(employeeArchiveId);
            return View("_employeecompare", compareEmployeeModel);
        }

        [HttpGet]
        public JsonResult GetStateByCityName(string cityName)
        {
            EmployeeManager _manager = new EmployeeManager();
            State state = _manager.GetStateByCityName(cityName);
            string stateId = String.Empty;
            if (state.IsNotNull())
            {
                stateId = Convert.ToString(state.StateId);
            }

            return Json(stateId, JsonRequestBehavior.AllowGet);
        }

        #region dropdownlist

        /// <summary>
        /// Get a list of designations.
        /// </summary>
        /// <returns></returns>
        public List<DesignationModel> GetDesignations()
        {
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
            genders.Add(new SelectListItem { Text = "Unknown", Value = "1" });
            genders.Add(new SelectListItem { Text = "Male", Value = "2" });
            genders.Add(new SelectListItem { Text = "Female", Value = "3" });

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
                BirthDay = employeeModel.BirthDay.IsNotNull() && employeeModel.BirthDay.HasValue ? employeeModel.BirthDay.Value : DateTime.Now,
                JoiningDate = employeeModel.JoiningDate.IsNotNull() && employeeModel.JoiningDate.HasValue ? employeeModel.JoiningDate.Value : DateTime.Now,
                ResignationDate = employeeModel.ResignationDate,
                RelievingDate = employeeModel.RelievingDate,
                VBS_Id = String.Format("vbs{0}", employeeModel.VBS_Id.Trim()),
                DesignationId = employeeModel.DesignationId.Value,
                SalutationId = employeeModel.SalutationId.Value,
                GenderId = employeeModel.GenderId.Value,
                OfficeBranchId = employeeModel.OfficeBranchId.Value,
                IsActive = employeeModel.IsActive,
                CreatedBy = User.Identity.Name,
                UpdatedBy = employeeModel.UpdatedBy,
                CreatedDate = DateTime.Now,
                UpdatedDate = employeeModel.UpdatedDate,
                EPFNumber = employeeModel.EPFNumber,
                ESINumber = employeeModel.ESINumber,
                OfficialEmailAddress = employeeModel.OfficialEmailAddress,
                PersonalEmailAddress = employeeModel.PersonalEmailAddress,
                PermanentAddress1 = employeeModel.PermanentAddress1,
                PermanentAddress2 = employeeModel.PermanentAddress2,
                PermanentCity = employeeModel.PermanentCity,
                PermanentZipCode = employeeModel.PermanentZipCode,
                PermanentStateId = employeeModel.PermanentStateId,
                PermanentCountryId = employeeModel.PermanentCountryId,
                MailingAddress1 = employeeModel.MailingAddress1,
                MailingAddress2 = employeeModel.MailingAddress2,
                MailingCity = employeeModel.MailingCity,
                MailingZipCode = employeeModel.MailingZipCode,
                MailingStateId = employeeModel.MailingStateId,
                MailingCountryId = employeeModel.MailingCountryId,
                IsMetro = employeeModel.IsMetro,
                BankAccountNumber = employeeModel.BankAccountNumber,
                BankId = employeeModel.BankId,
                BankIFSCCode = employeeModel.BankIFSCCode,
                BankMICRCode = employeeModel.BankMICRCode,
            };
        }

        /// <summary>
        /// Sets the dropdown values.
        /// </summary>
        /// <param name="employeeModel">The employee model.</param>
        private void SetDropdownValues()
        {
            ViewBag.Genders = GetGenders();

            var designations = _employeeManager.GetDesignations();

            if (designations.IsNotNull())
            {
                ViewBag.Designations = ConvertTo(designations);
            }

            var officeBranches = _employeeManager.GetOfficeBranches();

            if (officeBranches.IsNotNull())
            {
                ViewBag.OfficeBranches = ConvertTo(officeBranches);
            }
            var salutations = _employeeManager.GetSalutations();

            if (salutations.IsNotNull())
            {
                ViewBag.Salutations = ConvertTo(salutations);
            }

            var states = _employeeManager.GetStates();

            if (states.IsNotNull())
            {
                ViewBag.States = ConvertTo(states);
            }

            var banks = _employeeManager.GetBanks();

            if (states.IsNotNull())
            {
                ViewBag.Banks = ConvertTo(banks.Where(condition => condition.IsActive).ToList());
            }
        }

        private List<SelectListItem> ConvertTo(List<Bank> banks)
        {
            var result = new List<SelectListItem>();

            if (banks.IsNotNull())
            {
                banks.ForEach(item => result.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.BankId.ToString()
                }));
            }

            return result;
        }

        /// <summary>
        /// Converts to.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        private EmployeeModel ConvertTo(Employee employeeModel)
        {
            if (employeeModel.IsNull())
            {
                return new EmployeeModel();
            }
            return new EmployeeModel
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
                BirthDay = employeeModel.BirthDay,
                JoiningDate = employeeModel.JoiningDate,
                ResignationDate = employeeModel.ResignationDate,
                RelievingDate = employeeModel.RelievingDate,
                VBS_Id = employeeModel.VBS_Id.Remove(0, 3),
                DesignationId = employeeModel.DesignationId,
                SalutationId = employeeModel.SalutationId,
                GenderId = employeeModel.GenderId,
                OfficeBranchId = employeeModel.OfficeBranchId,
                IsActive = employeeModel.IsActive,
                CreatedBy = employeeModel.CreatedBy,
                UpdatedBy = employeeModel.UpdatedBy,
                CreatedDate = employeeModel.CreatedDate,
                UpdatedDate = employeeModel.UpdatedDate,
                EPFNumber = employeeModel.EPFNumber,
                ESINumber = employeeModel.ESINumber,
                OfficialEmailAddress = employeeModel.OfficialEmailAddress,
                PersonalEmailAddress = employeeModel.PersonalEmailAddress,
                PermanentAddress1 = employeeModel.PermanentAddress1,
                PermanentAddress2 = employeeModel.PermanentAddress2,
                PermanentCity = employeeModel.PermanentCity,
                PermanentZipCode = employeeModel.PermanentZipCode,
                PermanentStateId = employeeModel.PermanentStateId,
                PermanentCountryId = employeeModel.PermanentCountryId,
                MailingAddress1 = employeeModel.MailingAddress1,
                MailingAddress2 = employeeModel.MailingAddress2,
                MailingCity = employeeModel.MailingCity,
                MailingZipCode = employeeModel.MailingZipCode,
                MailingStateId = employeeModel.MailingStateId,
                MailingCountryId = employeeModel.MailingCountryId,
                IsMetro = employeeModel.IsMetro,
                BankAccountNumber = employeeModel.BankAccountNumber,
                BankId = employeeModel.BankId,
                BankIFSCCode = employeeModel.BankIFSCCode,
                BankMICRCode = employeeModel.BankMICRCode,
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

        private List<SelectListItem> GetEmployeeFilters()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem
            {
                Text = "All Employees",
                Value = "1"
            });
            listItems.Add(new SelectListItem
            {
                Text = "Active Employees",
                Value = "2",
                Selected = true
            });
            listItems.Add(new SelectListItem
            {
                Text = "In Active Employees",
                Value = "3"
            });

            return listItems;
        }

        private List<Employee> GetEmployees(int? filterChoice)
        {
            if (filterChoice.IsNull())
            {
                filterChoice = 2; // Set the default to All.
            }

            EmployeeFilter employeeFilter = (EmployeeFilter)Enum.ToObject(typeof(EmployeeFilter), filterChoice.Value);

            return _employeeManager.GetEmployees(employeeFilter);
        }

        #endregion Private Methods
    }
}