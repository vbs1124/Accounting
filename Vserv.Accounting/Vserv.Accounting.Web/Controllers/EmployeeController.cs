#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Vserv.Accounting.Business.Managers;
using Vserv.Accounting.Common;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Web.Models;
using AutoMapper;

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
        public EmployeeManager EmployeeManager;

        public EmployeeController()
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
                EmployeeManager.AddEmployee(employee);

                Dictionary<string, string> message = new Dictionary<string, string>
                {
                    {CommonConstants.MESSAGE, "Employee added successfully."}
                };
                return RedirectToAction("Success", "Home", new { successMessage = message.ToEncryptedString() });
            }
            ModelState.AddModelError("emp_errors", @"Please fill the required fields...");
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

            var employee = EmployeeManager.GetEmployee(employeeId);
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
                EmployeeManager.ArchiveEmployee(employeeModel.EmployeeId, User.Identity.Name);

                EmployeeManager.EditEmployee(employee);
                Dictionary<string, string> message = new Dictionary<string, string>
                {
                    {CommonConstants.MESSAGE, "Employee updated successfully."}
                };
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

            EmployeeManager.DeleteEmployee(employeeId);
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
            List<EmployeeArchive> employees = EmployeeManager.GetEmployeeHistory(employeeId);
            return PartialView("_employeehistory", employees);
        }

        [Route("employee/{employeeArchiveId}/compare")]
        public ActionResult EmployeeCompareResult(int employeeArchiveId)
        {
            SetDropdownValues();
            CompareEmployeeModel compareEmployeeModel = EmployeeManager.GetMatchingEmployeeInformation(employeeArchiveId);
            return View("_employeecompare", compareEmployeeModel);
        }

        [HttpGet]
        public JsonResult GetStateByCityName(string cityName)
        {
            EmployeeManager manager = new EmployeeManager();
            State state = manager.GetStateByCityName(cityName);
            string stateId = String.Empty;
            if (state.IsNotNull())
            {
                stateId = Convert.ToString(state.StateId);
            }

            return Json(stateId, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetYearlyPaySheet(PaySheetParameter paySheetParameter)
        {
            EmployeeManager manager = new EmployeeManager();
            var result = manager.GetYearlyPaySheet(paySheetParameter.EmployeeId, paySheetParameter.FinancialYearFrom, paySheetParameter.FinancialYearTo);

            var paySheet = ConvertToEmployeePaySheet(result);

            //var paySheet = result.AsQueryable().ProjectTo<EmployeePaySheet>();
            return Json(paySheet, JsonRequestBehavior.AllowGet);
        }

        private List<EmployeePaySheet> ConvertToEmployeePaySheet(List<GetEmployeeSalaryDetail_Result> result)
        {
            return result.Select(item => new EmployeePaySheet
            {
                EmployeeId = item.EmployeeId,
                DisplayOrder = item.DisplayOrder,
                April = ConvertToLookupAmount(item.April),
                May = ConvertToLookupAmount(item.May),
                June = ConvertToLookupAmount(item.June),
                July = ConvertToLookupAmount(item.July),
                August = ConvertToLookupAmount(item.August),
                September = ConvertToLookupAmount(item.September),
                October = ConvertToLookupAmount(item.October),
                November = ConvertToLookupAmount(item.November),
                December = ConvertToLookupAmount(item.December),
                January = ConvertToLookupAmount(item.January),
                February = ConvertToLookupAmount(item.February),
                March = ConvertToLookupAmount(item.March),
                EmpSalaryStructureId = item.EmpSalaryStructureId,
                SCName = item.SCName,
                SCCode = item.SCCode,
                SCDescription = item.SCDescription,
            }).ToList();
        }

        private LookupAmount ConvertToLookupAmount(string month)
        {
            if (month.IsNull())
            {
                return null;
            }

            var result = month.Split('#');
            return new LookupAmount { EmployeeSalaryDetailId = Convert.ToInt32(result[0]), Amount = Convert.ToDecimal(result[1]) };
        }

        public JsonResult GetEmployee(int employeeId)
        {
            var employee = EmployeeManager.GetEmployee(employeeId);
            EmployeeModel employeeModel = ConvertTo(employee);
            return CustomJson(employeeModel);
        }

        #region Salary Management

        public JsonResult SaveEmployeeSalaryDetail(EmpSalaryStructureModel empSalaryStructureModel, int employeeId)
        {
            EmployeeManager manager = new EmployeeManager();
            EmpSalaryStructure empSalaryStructure = new EmpSalaryStructure
            {
                CTC = String.IsNullOrWhiteSpace(empSalaryStructureModel.CTC) ? 0 : Convert.ToDecimal(empSalaryStructureModel.CTC),
                MonthlyCabDeductions = String.IsNullOrWhiteSpace(empSalaryStructureModel.CabDeductions) ? 0 : Convert.ToDecimal(empSalaryStructureModel.CabDeductions),
                MonthlyProjectIncentive = String.IsNullOrWhiteSpace(empSalaryStructureModel.ProjectIncentive) ? 0 : Convert.ToDecimal(empSalaryStructureModel.ProjectIncentive),
                MonthlyCarLease = String.IsNullOrWhiteSpace(empSalaryStructureModel.CarLease) ? 0 : Convert.ToDecimal(empSalaryStructureModel.CarLease),
                MonthlyFoodCoupons = String.IsNullOrWhiteSpace(empSalaryStructureModel.FoodCoupons) ? 0 : Convert.ToDecimal(empSalaryStructureModel.FoodCoupons),
                EffectiveFrom = String.IsNullOrWhiteSpace(empSalaryStructureModel.EffectiveFrom) ? DateTime.Now : Convert.ToDateTime(empSalaryStructureModel.EffectiveFrom),
                EmployeeId = employeeId,
                CreatedBy = User.Identity.Name,
            };

            manager.SaveEmployeeSalaryDetail(empSalaryStructure, User.Identity.Name);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetEmpSalaryStructureModel()
        {
            EmpSalaryStructureModel empSalaryStructureModel = new EmpSalaryStructureModel();

            return Json(empSalaryStructureModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetEmployeeAppraisalHistory(int employeeId)
        {
            EmployeeManager manager = new EmployeeManager();
            return Json(manager.GetEmployeeAppraisalHistory(employeeId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateYearlyPaySheet(List<EmployeePaySheet> paySheets)
        {
            EmployeeManager _manager = new EmployeeManager();
            List<GetEmployeeSalaryDetail_Result> updatedPaySheet = new List<GetEmployeeSalaryDetail_Result>();
            
            List<EmployeeSalaryDetail> empSalaryDetails = ConvertToEmployeeSalaryDetails(paySheets);
            var result = _manager.UpdateYearlyPaySheet(empSalaryDetails, User.Identity.Name);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region dropdownlist

        /// <summary>
        /// Get a list of designations.
        /// </summary>
        /// <returns></returns>
        public List<DesignationModel> GetDesignations()
        {
            var designations = EmployeeManager.GetDesignations();
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
            var officeBranches = EmployeeManager.GetOfficeBranches();
            IEnumerable<SelectListItem> officeBranchesSelectListItems = null;

            if (officeBranches.IsNotNull())
            {
                officeBranchesSelectListItems = ConvertTo(officeBranches.Where(condition => condition.IsActive).ToList());
            }

            return Json(new SelectList(officeBranchesSelectListItems, "Value", "Text", "1"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSalutations()
        {
            IEnumerable<SelectListItem> officeBranchesSelectListItems = null;

            var salutations = EmployeeManager.GetSalutations();

            if (salutations.IsNotNull())
            {
                officeBranchesSelectListItems = ConvertTo(salutations.Where(condition => condition.IsActive).ToList());
            }

            return Json(new SelectList(officeBranchesSelectListItems, "Value", "Text", "1"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStates()
        {
            IEnumerable<SelectListItem> statesSelectListItems = null;

            var states = EmployeeManager.GetStates();

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
            var genders = new List<SelectListItem>
            {
                new SelectListItem {Text = @"Unknown", Value = "1"},
                new SelectListItem {Text = @"Male", Value = "2"},
                new SelectListItem {Text = @"Female", Value = "3"}
            };

            return genders;
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
        /// <param name="employeeModel">The employee model.</param>
        /// <returns></returns>
        private Employee ConvertTo(EmployeeModel employeeModel)
        {
            if (employeeModel.IsNull())
            {
                return new Employee();
            }

            if (employeeModel.DesignationId != null)
                if (employeeModel.SalutationId != null)
                    if (employeeModel.GenderId != null)
                        if (employeeModel.OfficeBranchId != null)
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
            return null;
        }

        /// <summary>
        /// Sets the dropdown values.
        /// </summary>
        private void SetDropdownValues()
        {
            ViewBag.Genders = GetGenders();

            var designations = EmployeeManager.GetDesignations();

            if (designations.IsNotNull())
            {
                ViewBag.Designations = ConvertTo(designations);
            }

            var officeBranches = EmployeeManager.GetOfficeBranches();

            if (officeBranches.IsNotNull())
            {
                ViewBag.OfficeBranches = ConvertTo(officeBranches);
            }
            var salutations = EmployeeManager.GetSalutations();

            if (salutations.IsNotNull())
            {
                ViewBag.Salutations = ConvertTo(salutations);
            }

            var states = EmployeeManager.GetStates();

            if (states.IsNotNull())
            {
                ViewBag.States = ConvertTo(states);
            }

            var banks = EmployeeManager.GetBanks();

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
        /// <param name="employeeModel"></param>
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
            List<SelectListItem> listItems = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"All Employees",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = @"Active Employees",
                    Value = "2",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"In Active Employees",
                    Value = "3"
                }
            };

            return listItems;
        }

        private List<Employee> GetEmployees(int? filterChoice)
        {
            if (filterChoice.IsNull())
            {
                filterChoice = 2; // Set the default to All.
            }

            if (filterChoice != null)
            {
                EmployeeFilter employeeFilter = (EmployeeFilter)Enum.ToObject(typeof(EmployeeFilter), filterChoice.Value);

                return EmployeeManager.GetEmployees(employeeFilter);
            }
            return null;
        }

        private List<EmployeeSalaryDetail> ConvertToEmployeeSalaryDetails(List<EmployeePaySheet> paySheets)
        {
            List<EmployeeSalaryDetail> employeeSalaryDetails = new List<EmployeeSalaryDetail>();

            foreach (var item in paySheets)
            {
                employeeSalaryDetails.Add(new EmployeeSalaryDetail { Amount = item.April.Amount, EmployeeSalaryDetailId = item.April.EmployeeSalaryDetailId });
                employeeSalaryDetails.Add(new EmployeeSalaryDetail { Amount = item.May.Amount, EmployeeSalaryDetailId = item.May.EmployeeSalaryDetailId });
                employeeSalaryDetails.Add(new EmployeeSalaryDetail { Amount = item.June.Amount, EmployeeSalaryDetailId = item.June.EmployeeSalaryDetailId });
                employeeSalaryDetails.Add(new EmployeeSalaryDetail { Amount = item.July.Amount, EmployeeSalaryDetailId = item.July.EmployeeSalaryDetailId });
                employeeSalaryDetails.Add(new EmployeeSalaryDetail { Amount = item.August.Amount, EmployeeSalaryDetailId = item.August.EmployeeSalaryDetailId });
                employeeSalaryDetails.Add(new EmployeeSalaryDetail { Amount = item.September.Amount, EmployeeSalaryDetailId = item.September.EmployeeSalaryDetailId });
                employeeSalaryDetails.Add(new EmployeeSalaryDetail { Amount = item.October.Amount, EmployeeSalaryDetailId = item.October.EmployeeSalaryDetailId });
                employeeSalaryDetails.Add(new EmployeeSalaryDetail { Amount = item.November.Amount, EmployeeSalaryDetailId = item.November.EmployeeSalaryDetailId });
                employeeSalaryDetails.Add(new EmployeeSalaryDetail { Amount = item.December.Amount, EmployeeSalaryDetailId = item.December.EmployeeSalaryDetailId });
                employeeSalaryDetails.Add(new EmployeeSalaryDetail { Amount = item.January.Amount, EmployeeSalaryDetailId = item.January.EmployeeSalaryDetailId });
                employeeSalaryDetails.Add(new EmployeeSalaryDetail { Amount = item.February.Amount, EmployeeSalaryDetailId = item.February.EmployeeSalaryDetailId });
                employeeSalaryDetails.Add(new EmployeeSalaryDetail { Amount = item.March.Amount, EmployeeSalaryDetailId = item.March.EmployeeSalaryDetailId });
            }

            return employeeSalaryDetails;
        }

        #endregion Private Methods
    }
}