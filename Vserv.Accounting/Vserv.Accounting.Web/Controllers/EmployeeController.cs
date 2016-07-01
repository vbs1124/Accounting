#region Namespaces

using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Vserv.Accounting.Business.Managers;
using Vserv.Accounting.Common;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Data.Entity.Models;
using Vserv.Accounting.Web.Models;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
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

        /// <summary>
        /// Gets the filtered employees.
        /// </summary>
        /// <param name="filterChoice">The filter choice.</param>
        /// <returns></returns>
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
                TempData["Message"] = String.Format("Employee <vbs{0}> added successfully.", employeeModel.VBS_Id);
                return RedirectToAction("list", "employee");
                //Dictionary<string, string> message = new Dictionary<string, string>
                //{
                //    {CommonConstants.MESSAGE, "Employee added successfully."}
                //};
                //return RedirectToAction("Success", "Home", new { successMessage = message.ToEncryptedString() });
            }

            ModelState.AddModelError("emp_errors", @"Please fill the required fields...");
            SetDropdownValues();
            return View(employeeModel);
        }

        /// <summary>
        /// Edits the specified employee identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Route("employee/{id}/edit")]
        public ActionResult Edit(string id)
        {
            var employee = EmployeeManager.GetEmployee(id.ToDecryptedInt());
            EmployeeModel employeeModel = ConvertTo(employee);
            SetDropdownValues();
            return View(employeeModel);
        }

        public JsonResult GetEmployeeDetail(string id)
        {
            var employee = EmployeeManager.GetEmployee(id.ToDecryptedInt());
            EmployeeModel employeeModel = ConvertTo(employee);
            return Json(employeeModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Edits the specified employee model.
        /// </summary>
        /// <param name="employeeModel">The employee model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("employee/{id}/edit")]
        public ActionResult Edit(EmployeeModel employeeModel)
        {
            if (employeeModel.IsNotNull())
            {
                employeeModel.UpdatedBy = User.Identity.Name;
                employeeModel.UpdatedDate = DateTime.Now;
                employeeModel.IsActive = !employeeModel.RelievingDate.IsNotNull() && employeeModel.IsActive;

                // Perform Save for Employee
                if (ModelState.IsValid)
                {
                    Employee employee = ConvertTo(employeeModel);

                    // Archive Existing Employee information before going for an update.
                    EmployeeManager.ArchiveEmployee(employeeModel.EmployeeId, User.Identity.Name);

                    EmployeeManager.EditEmployee(employee);
                    TempData["Message"] = String.Format("Employee <vbs{0}> updated successfully.", employeeModel.VBS_Id);
                    return RedirectToAction("list", "employee");
                }
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
        /// Banking this instance.
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

        /// <summary>
        /// Employees the history.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public ActionResult EmployeeHistory(int employeeId)
        {
            List<EmployeeArchive> employees = EmployeeManager.GetEmployeeHistory(employeeId);
            return PartialView("_employeehistory", employees);
        }

        /// <summary>
        /// Employees the compare result.
        /// </summary>
        /// <param name="employeeArchiveId">The employee archive identifier.</param>
        /// <returns></returns>
        [Route("employee/{employeeArchiveId}/compare")]
        public ActionResult EmployeeCompareResult(int employeeArchiveId)
        {
            SetDropdownValues();
            CompareEmployeeModel compareEmployeeModel = EmployeeManager.GetMatchingEmployeeInformation(employeeArchiveId);
            return View("_employeecompare", compareEmployeeModel);
        }

        public ActionResult LoadEmpChangeComparisonResult(int employeeArchiveId)
        {
            CompareEmployeeModel compareEmployeeModel = EmployeeManager.GetMatchingEmployeeInformation(employeeArchiveId);
            
            return Json(compareEmployeeModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the name of the state by city.
        /// </summary>
        /// <param name="cityName">Name of the city.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the yearly pay sheet.
        /// </summary>
        /// <param name="paySheetParameter">The pay sheet parameter.</param>
        /// <returns></returns>
        public JsonResult GetYearlyPaySheet(PaySheetParameter paySheetParameter)
        {
            EmployeeManager manager = new EmployeeManager();
            var result = manager.GetYearlyPaySheet(paySheetParameter.EmployeeId, paySheetParameter.FinancialYearFrom, paySheetParameter.FinancialYearTo);

            var paySheet = ConvertToEmployeePaySheet(result);

            //var paySheet = result.AsQueryable().ProjectTo<EmployeePaySheet>();
            return Json(paySheet, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Converts to employee pay sheet.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Converts to lookup amount.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Loads the employee change history.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public JsonResult LoadEmployeeChangeHistory(int employeeId)
        {
            List<EmployeeArchive> employees = EmployeeManager.GetEmployeeHistory(employeeId);
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        #region Salary Management

        /// <summary>
        /// Saves the employee salary detail.
        /// </summary>
        /// <param name="empSalaryStructureModel">The emp salary structure model.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
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
                PerformanceIncentivePayable = String.IsNullOrWhiteSpace(empSalaryStructureModel.PerformanceIncentivePayable) ? 1 : Convert.ToInt32(empSalaryStructureModel.PerformanceIncentivePayable),
                EmployeeId = employeeId,
                CreatedBy = User.Identity.Name,
            };

            return Json(manager.SaveEmployeeSalaryDetail(empSalaryStructure, User.Identity.Name), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the emp salary structure model.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetEmpSalaryStructureModel()
        {
            EmpSalaryStructureModel empSalaryStructureModel = new EmpSalaryStructureModel();

            return Json(empSalaryStructureModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the employee appraisal history.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetEmployeeAppraisalHistory(int employeeId)
        {
            EmployeeManager manager = new EmployeeManager();
            return Json(manager.GetEmployeeAppraisalHistory(employeeId), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Updates the yearly pay sheet.
        /// </summary>
        /// <param name="paySheets">The pay sheets.</param>
        /// <returns></returns>
        public JsonResult UpdateYearlyPaySheet(List<EmployeePaySheet> paySheets)
        {
            EmployeeManager manager = new EmployeeManager();
            List<EmpSalaryDetail> empSalaryDetails = ConvertToEmployeeSalaryDetails(paySheets);
            manager.UpdateYearlyPaySheet(empSalaryDetails, User.Identity.Name);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [Route("employee/salary/{empSalaryStructureId}/changeHistory")]
        public JsonResult GetSalaryStructureChangeHistory(int empSalaryStructureId)
        {
            return Json(EmployeeManager.GetSalaryStructureChangeHistory(empSalaryStructureId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmpSalaryStructureComparisonList(SalaryComparisonParameter salaryComparisonParameter)
        {
            List<EmpSalaryCompareResult> result = EmployeeManager.GetEmpSalaryStructureComparisonList(salaryComparisonParameter.EmployeeId, salaryComparisonParameter.FinancialYearFrom, salaryComparisonParameter.UniqueChangeId);

            List<EmpSalaryCompareModel> empSalaryCompareModel = result.Select(ConvertToEmpSalaryCompareModel).ToList();

            return Json(empSalaryCompareModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployeePaySlip(int employeeId, int monthId, int year)
        {
            GenearteSalarySlip(EmployeeManager.GetEmployeePaySlip(employeeId, monthId, year));
            return Json("true", JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Investments

        public JsonResult GetInvestmentByEmployeeId(int employeeId)
        {
            InvestmentDeclarationModel investmentDeclarationModel = new InvestmentDeclarationModel
            {
                April = 20000,
            };
            return Json(investmentDeclarationModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInvestmentCatogories(int financialYear, int employeeId)
        {

            EmpInvestmentDeclarationModel investmentCatogories = EmployeeManager.GetInvestmentCatogories(financialYear, employeeId);
            //List<InvestmentCategory> investmentCatogories = EmployeeManager.GetInvestmentCatogories(financialYear);// This will have the values frm DB
            //foreach (var item in investmentCatogories)
            //{
            //    item.InvestmentSubCategories.ForEach(subCat => subCat.InvestmentCategory = null);
            //}

            return Json(investmentCatogories, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveEmployeeInvestments(int employeeId, int finYear, List<InvestmentCategoryModel> empInvestmentDeclarationModel)
        {
            // Call for Save through Manager.
            EmpInvestmentDeclarationModel obj = new EmpInvestmentDeclarationModel
            {
                EmployeeId = employeeId,
                InvestmentCategories = empInvestmentDeclarationModel
            };
            EmployeeManager.SaveEmployeeInvestments(obj.EmployeeId, finYear, obj);
            return Json("true", JsonRequestBehavior.AllowGet);
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

        /// <summary>
        /// Converts to designation model.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the office branches.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the salutations.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Converts to.
        /// </summary>
        /// <param name="banks">The banks.</param>
        /// <returns></returns>
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
        /// <param name="employeeModel">The employee model.</param>
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

        /// <summary>
        /// Converts to.
        /// </summary>
        /// <param name="designations">The designations.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the employee filters.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <param name="filterChoice">The filter choice.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Converts to employee salary details.
        /// </summary>
        /// <param name="paySheets">The pay sheets.</param>
        /// <returns></returns>
        private List<EmpSalaryDetail> ConvertToEmployeeSalaryDetails(List<EmployeePaySheet> paySheets)
        {
            List<EmpSalaryDetail> employeeSalaryDetails = new List<EmpSalaryDetail>();

            foreach (var item in paySheets)
            {
                if (item.April.IsNotNull())
                    employeeSalaryDetails.Add(new EmpSalaryDetail { EmpSalaryStructureId = item.EmpSalaryStructureId.Value, Amount = item.April.Amount, EmpSalaryDetailId = item.April.EmployeeSalaryDetailId });
                if (item.May.IsNotNull())
                    employeeSalaryDetails.Add(new EmpSalaryDetail { EmpSalaryStructureId = item.EmpSalaryStructureId.Value, Amount = item.May.Amount, EmpSalaryDetailId = item.May.EmployeeSalaryDetailId });
                if (item.June.IsNotNull())
                    employeeSalaryDetails.Add(new EmpSalaryDetail { EmpSalaryStructureId = item.EmpSalaryStructureId.Value, Amount = item.June.Amount, EmpSalaryDetailId = item.June.EmployeeSalaryDetailId });
                if (item.July.IsNotNull())
                    employeeSalaryDetails.Add(new EmpSalaryDetail { EmpSalaryStructureId = item.EmpSalaryStructureId.Value, Amount = item.July.Amount, EmpSalaryDetailId = item.July.EmployeeSalaryDetailId });
                if (item.August.IsNotNull())
                    employeeSalaryDetails.Add(new EmpSalaryDetail { EmpSalaryStructureId = item.EmpSalaryStructureId.Value, Amount = item.August.Amount, EmpSalaryDetailId = item.August.EmployeeSalaryDetailId });
                if (item.September.IsNotNull())
                    employeeSalaryDetails.Add(new EmpSalaryDetail { EmpSalaryStructureId = item.EmpSalaryStructureId.Value, Amount = item.September.Amount, EmpSalaryDetailId = item.September.EmployeeSalaryDetailId });
                if (item.October.IsNotNull())
                    employeeSalaryDetails.Add(new EmpSalaryDetail { EmpSalaryStructureId = item.EmpSalaryStructureId.Value, Amount = item.October.Amount, EmpSalaryDetailId = item.October.EmployeeSalaryDetailId });
                if (item.November.IsNotNull())
                    employeeSalaryDetails.Add(new EmpSalaryDetail { EmpSalaryStructureId = item.EmpSalaryStructureId.Value, Amount = item.November.Amount, EmpSalaryDetailId = item.November.EmployeeSalaryDetailId });
                if (item.December.IsNotNull())
                    employeeSalaryDetails.Add(new EmpSalaryDetail { EmpSalaryStructureId = item.EmpSalaryStructureId.Value, Amount = item.December.Amount, EmpSalaryDetailId = item.December.EmployeeSalaryDetailId });
                if (item.January.IsNotNull())
                    employeeSalaryDetails.Add(new EmpSalaryDetail { EmpSalaryStructureId = item.EmpSalaryStructureId.Value, Amount = item.January.Amount, EmpSalaryDetailId = item.January.EmployeeSalaryDetailId });
                if (item.February.IsNotNull())
                    employeeSalaryDetails.Add(new EmpSalaryDetail { EmpSalaryStructureId = item.EmpSalaryStructureId.Value, Amount = item.February.Amount, EmpSalaryDetailId = item.February.EmployeeSalaryDetailId });
                if (item.March.IsNotNull())
                    employeeSalaryDetails.Add(new EmpSalaryDetail { EmpSalaryStructureId = item.EmpSalaryStructureId.Value, Amount = item.March.Amount, EmpSalaryDetailId = item.March.EmployeeSalaryDetailId });
            }

            return employeeSalaryDetails;
        }

        private EmpSalaryCompareModel ConvertToEmpSalaryCompareModel(EmpSalaryCompareResult empSalaryCompareResult)
        {
            return new EmpSalaryCompareModel
            {
                EmployeeId = empSalaryCompareResult.EmployeeId,
                EmpSalaryStructureId = empSalaryCompareResult.EmpSalaryStructureId,
                SCName = empSalaryCompareResult.SCName,
                SCCode = empSalaryCompareResult.SCCode,
                SCDescription = empSalaryCompareResult.SCDescription,
                DisplayOrder = empSalaryCompareResult.DisplayOrder,
                April = ConvertToSalaryComponentDetail(empSalaryCompareResult.April),
                May = ConvertToSalaryComponentDetail(empSalaryCompareResult.May),
                June = ConvertToSalaryComponentDetail(empSalaryCompareResult.June),
                July = ConvertToSalaryComponentDetail(empSalaryCompareResult.July),
                August = ConvertToSalaryComponentDetail(empSalaryCompareResult.August),
                September = ConvertToSalaryComponentDetail(empSalaryCompareResult.September),
                October = ConvertToSalaryComponentDetail(empSalaryCompareResult.October),
                November = ConvertToSalaryComponentDetail(empSalaryCompareResult.November),
                December = ConvertToSalaryComponentDetail(empSalaryCompareResult.December),
                January = ConvertToSalaryComponentDetail(empSalaryCompareResult.January),
                February = ConvertToSalaryComponentDetail(empSalaryCompareResult.February),
                March = ConvertToSalaryComponentDetail(empSalaryCompareResult.March),
            };
        }

        private SalaryComponentDetail ConvertToSalaryComponentDetail(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                return new SalaryComponentDetail();
            }

            string[] amounts = value.Split(':');
            string[] amount = amounts[0].Split('#');
            string[] currentAmount = amounts[1].Split('#');

            SalaryComponentDetail salaryComponentDetail = new SalaryComponentDetail
            {
                CurrentAmount = Convert.ToInt32(currentAmount[1]),
                CurrentEmpSalaryDetailId = Convert.ToInt32(currentAmount[0]),
                Amount = Convert.ToInt32(amount[1]),
                EmpSalaryDetailId = Convert.ToInt32(amount[0]),
            };

            salaryComponentDetail.IsDirty = !salaryComponentDetail.CurrentAmount.Equals(salaryComponentDetail.Amount);
            return salaryComponentDetail;
        }

        private void ShowPdf(string filename)
        {
            //Clears all content output from Buffer Stream
            Response.ClearContent();
            //Clears all headers from Buffer Stream
            Response.ClearHeaders();
            //Adds an HTTP header to the output stream
            Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
            //Gets or Sets the HTTP MIME type of the output stream
            Response.ContentType = "application/pdf";
            //Writes the content of the specified file directory to an HTTP response output stream as a file block
            Response.WriteFile(filename);
            //sends all currently buffered output to the client
            Response.Flush();
            //Clears all content output from Buffer Stream
            Response.Clear();
        }

        public void GenearteSalarySlip(EmployeePaySlip salarySlip)
        {
            Document document = new Document(new Rectangle(841.68f, 599.72f), 20f, 20f, 20f, 20f);
            string path = Server.MapPath("PDFs");
            string filename = path + "/SalarySlip.pdf";
            string imagename = path + "/logo.jpg";
            //Create new PDF document

            try
            {
                PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));

                BaseFont baseFont1 = BaseFont.CreateFont(
                            BaseFont.TIMES_BOLD,
                            BaseFont.CP1252,
                            BaseFont.EMBEDDED);
                Font font1 = new Font(baseFont1, 10);
                BaseFont baseFont2 = BaseFont.CreateFont(
                            BaseFont.TIMES_ROMAN,
                            BaseFont.CP1252,
                            BaseFont.EMBEDDED);
                Font font2 = new Font(baseFont2, 10);
                BaseFont baseFont3 = BaseFont.CreateFont(
                            BaseFont.TIMES_BOLD,
                            BaseFont.CP1252,
                            BaseFont.EMBEDDED);
                Font font3 = new Font(baseFont3, 8);
                BaseFont baseFont4 = BaseFont.CreateFont(
                            BaseFont.TIMES_ROMAN,
                            BaseFont.CP1252,
                            BaseFont.EMBEDDED);
                Font font4 = new Font(baseFont4, 8);

                PdfPTable table = new PdfPTable(11) { TotalWidth = 800f, LockedWidth = true };
                //fix the absolute width of the table

                //relative col widths in proportions - 1/3 and 2/3
                float[] widths = { 4f, 1f, 2f, 2f, 2f, .5f, 2f, 1f, 2f, 1.5f, 2f };
                table.SetWidths(widths);
                table.HorizontalAlignment = 0;
                //leave a gap before and after the table
                table.SpacingBefore = 20f;
                table.SpacingAfter = 30f;
                //PdfPCell cell1 = new PdfPCell();
                //PdfPTable table2 = new PdfPTable(1);
                PdfPCell line1A = new PdfPCell(new Phrase(" ", font1));
                AddtextCell(line1A, 3, 1);
                line1A.BorderWidthBottom = 0;
                line1A.BorderWidthRight = 0;
                table.AddCell(line1A);
                PdfPCell line1B = new PdfPCell(new Phrase("Salary Payslip for the Month of May-2015", font1));
                AddtextCell(line1B, 5, 1);
                line1B.BorderWidthBottom = 0;
                line1B.BorderWidthLeft = 0;
                line1B.BorderWidthRight = 0;
                table.AddCell(line1B);
                PdfPCell line1C = new PdfPCell(new Phrase(" ", font1));
                AddtextCell(line1C, 3, 1);
                line1C.BorderWidthBottom = 0;
                line1C.BorderWidthLeft = 0;
                table.AddCell(line1C);

                Image image = Image.GetInstance(imagename);
                image.ScaleToFit(100, 75);
                image.SetAbsolutePosition(720, 545);

                PdfPCell line2A = new PdfPCell(new Phrase(" ", font1));
                AddtextCell(line2A, 3, 1);
                line2A.BorderWidthTop = 0;
                line2A.BorderWidthBottom = 0;
                line2A.BorderWidthRight = 0;
                table.AddCell(line2A);
                PdfPCell line2 = new PdfPCell(new Phrase("Pay Period 01.05.2015 to 31.05.2015", font2));
                AddtextCell(line2, 5, 1);
                line2.BorderWidthTop = 0;
                line2.BorderWidthBottom = 0;
                line2.BorderWidthLeft = 0;
                line2.BorderWidthRight = 0;
                table.AddCell(line2);
                PdfPCell line2C = new PdfPCell(new Phrase(" ", font1));
                AddtextCell(line2C, 3, 1);
                line2C.BorderWidthTop = 0;
                line2C.BorderWidthBottom = 0;
                line2C.BorderWidthLeft = 0;
                table.AddCell(line2C);

                PdfPCell line3A = new PdfPCell(new Phrase(" ", font1));
                AddtextCell(line3A, 3, 1);
                line3A.BorderWidthTop = 0;
                line3A.BorderWidthRight = 0;
                table.AddCell(line3A);
                PdfPCell line3 = new PdfPCell(new Phrase("Deepak Kumar", font1));
                AddtextCell(line3, 5, 1);
                line3.BorderWidthTop = 0;
                line3.BorderWidthLeft = 0;
                line3.BorderWidthRight = 0;
                line3.PaddingBottom = 5f;
                table.AddCell(line3);
                PdfPCell line3C = new PdfPCell(new Phrase(" ", font1));
                AddtextCell(line3C, 3, 1);
                line3C.BorderWidthTop = 0;
                line3C.BorderWidthLeft = 0;
                table.AddCell(line3C);

                PdfPCell line41 = new PdfPCell(new Phrase("Employee ID", font3));
                AddtextCell(line41, 1, 0);
                line41.BorderWidthRight = 0;
                line41.BorderWidthBottom = 0;
                table.AddCell(line41);
                PdfPCell line42 = new PdfPCell(new Phrase(":    " + salarySlip.VBS_Id, font4));
                AddtextCell(line42, 5, 0);
                line42.BorderWidthRight = 0;
                line42.BorderWidthLeft = 0;
                line42.BorderWidthBottom = 0;
                table.AddCell(line42);
                PdfPCell line43 = new PdfPCell(new Phrase("Bank Name & Account No", font3));
                AddtextCell(line43, 3, 0);
                line43.BorderWidthRight = 0;
                line43.BorderWidthLeft = 0;
                line43.BorderWidthBottom = 0;
                table.AddCell(line43);
                PdfPCell line44 = new PdfPCell(new Phrase(":    " + salarySlip.BankAccountNumber, font4));
                AddtextCell(line44, 2, 0);
                line44.BorderWidthLeft = 0;
                line44.BorderWidthBottom = 0;
                table.AddCell(line44);


                PdfPCell line51 = new PdfPCell(new Phrase("Person ID", font3));
                AddtextCell(line51, 1, 0);
                line51.BorderWidthRight = 0;
                line51.BorderWidthTop = 0;
                line51.BorderWidthBottom = 0;
                table.AddCell(line51);
                PdfPCell line52 = new PdfPCell(new Phrase(":    " + salarySlip.PersonID, font4));
                AddtextCell(line52, 5, 0);
                line52.BorderWidthRight = 0;
                line52.BorderWidthLeft = 0;
                line52.BorderWidthTop = 0;
                line52.BorderWidthBottom = 0;
                table.AddCell(line52);
                PdfPCell line53 = new PdfPCell(new Phrase("Location (CWL)", font3));
                AddtextCell(line53, 3, 0);
                line53.BorderWidthRight = 0;
                line53.BorderWidthLeft = 0;
                line53.BorderWidthTop = 0;
                line53.BorderWidthBottom = 0;
                table.AddCell(line53);
                PdfPCell line54 = new PdfPCell(new Phrase(":    " + salarySlip.Location, font4));
                AddtextCell(line54, 2, 0);
                line54.BorderWidthLeft = 0;
                line54.BorderWidthTop = 0;
                line54.BorderWidthBottom = 0;
                table.AddCell(line54);

                PdfPCell line61 = new PdfPCell(new Phrase("Designation", font3));
                AddtextCell(line61, 1, 0);
                line61.BorderWidthRight = 0;
                line61.BorderWidthTop = 0;
                line61.BorderWidthBottom = 0;
                table.AddCell(line61);
                PdfPCell line62 = new PdfPCell(new Phrase(":    " + salarySlip.Designation, font4));
                AddtextCell(line62, 5, 0);
                line62.BorderWidthRight = 0;
                line62.BorderWidthLeft = 0;
                line62.BorderWidthTop = 0;
                line62.BorderWidthBottom = 0;
                table.AddCell(line62);
                PdfPCell line63 = new PdfPCell(new Phrase("Department", font3));
                AddtextCell(line63, 3, 0);
                line63.BorderWidthRight = 0;
                line63.BorderWidthLeft = 0;
                line63.BorderWidthTop = 0;
                line63.BorderWidthBottom = 0;
                table.AddCell(line63);
                PdfPCell line64 = new PdfPCell(new Phrase(":    " + salarySlip.Department, font4));
                AddtextCell(line64, 2, 0);
                line64.BorderWidthLeft = 0;
                line64.BorderWidthTop = 0;
                line64.BorderWidthBottom = 0;
                table.AddCell(line64);

                PdfPCell line71 = new PdfPCell(new Phrase("DOJ / Gender", font3));
                AddtextCell(line71, 1, 0);
                line71.BorderWidthRight = 0;
                line71.BorderWidthTop = 0;
                line71.BorderWidthBottom = 0;
                table.AddCell(line71);
                PdfPCell line72 = new PdfPCell(new Phrase(":    " + String.Format("{0}/{1}", salarySlip.JoiningDate, salarySlip.Gender), font4));
                AddtextCell(line72, 5, 0);
                line72.BorderWidthRight = 0;
                line72.BorderWidthLeft = 0;
                line72.BorderWidthTop = 0;
                line72.BorderWidthBottom = 0;
                table.AddCell(line72);
                PdfPCell line73 = new PdfPCell(new Phrase("Band", font3));
                AddtextCell(line73, 3, 0);
                line73.BorderWidthRight = 0;
                line73.BorderWidthLeft = 0;
                line73.BorderWidthTop = 0;
                line73.BorderWidthBottom = 0;
                table.AddCell(line73);
                PdfPCell line74 = new PdfPCell(new Phrase(":    " + salarySlip.Band, font4));
                AddtextCell(line74, 2, 0);
                line74.BorderWidthLeft = 0;
                line74.BorderWidthTop = 0;
                line74.BorderWidthBottom = 0;
                table.AddCell(line74);

                PdfPCell line81 = new PdfPCell(new Phrase("PAN No", font3));
                AddtextCell(line81, 1, 0);
                line81.BorderWidthRight = 0;
                line81.BorderWidthTop = 0;
                line81.BorderWidthBottom = 0;
                table.AddCell(line81);
                PdfPCell line82 = new PdfPCell(new Phrase(":    " + salarySlip.PermanentAccountNumber, font4));
                AddtextCell(line82, 5, 0);
                line82.BorderWidthRight = 0;
                line82.BorderWidthLeft = 0;
                line82.BorderWidthTop = 0;
                line82.BorderWidthBottom = 0;
                table.AddCell(line82);
                PdfPCell line83 = new PdfPCell(new Phrase("Days worked in month", font3));
                AddtextCell(line83, 3, 0);
                line83.BorderWidthRight = 0;
                line83.BorderWidthLeft = 0;
                line83.BorderWidthTop = 0;
                line83.BorderWidthBottom = 0;
                table.AddCell(line83);
                PdfPCell line84 = new PdfPCell(new Phrase(":    " + salarySlip.DaysWorkedInMonth, font4));
                AddtextCell(line84, 2, 0);
                line84.BorderWidthLeft = 0;
                line84.BorderWidthTop = 0;
                line84.BorderWidthBottom = 0;
                table.AddCell(line84);

                PdfPCell line91 = new PdfPCell(new Phrase("PF / Pension No", font3));
                AddtextCell(line91, 1, 0);
                line91.BorderWidthRight = 0;
                line91.BorderWidthTop = 0;
                line91.BorderWidthBottom = 0;
                table.AddCell(line91);
                PdfPCell line92 = new PdfPCell(new Phrase(":    " + salarySlip.EPFNumber, font4));
                AddtextCell(line92, 5, 0);
                line92.BorderWidthRight = 0;
                line92.BorderWidthLeft = 0;
                line92.BorderWidthTop = 0;
                line92.BorderWidthBottom = 0;
                table.AddCell(line92);
                PdfPCell line93 = new PdfPCell(new Phrase("LWP Current/Previous Month", font3));
                AddtextCell(line93, 3, 0);
                line93.BorderWidthRight = 0;
                line93.BorderWidthLeft = 0;
                line93.BorderWidthTop = 0;
                line93.BorderWidthBottom = 0;
                table.AddCell(line93);
                PdfPCell line94 = new PdfPCell(new Phrase(":    " + salarySlip.LWPCurrentOrPreviousMonth, font4));
                AddtextCell(line94, 2, 0);
                line94.BorderWidthLeft = 0;
                line94.BorderWidthTop = 0;
                line94.BorderWidthBottom = 0;
                table.AddCell(line94);

                PdfPCell line101 = new PdfPCell(new Phrase("UAN No", font3));
                AddtextCell(line101, 1, 0);
                line101.BorderWidthRight = 0;
                line101.BorderWidthTop = 0;
                table.AddCell(line101);
                PdfPCell line102 = new PdfPCell(new Phrase(":    " + salarySlip.UniversalAccountNumber, font4));
                AddtextCell(line102, 5, 0);
                line102.BorderWidthRight = 0;
                line102.BorderWidthLeft = 0;
                line102.BorderWidthTop = 0;
                table.AddCell(line102);
                PdfPCell line103 = new PdfPCell(new Phrase("Sabbatical Leave Current/Previous Mon", font3));
                AddtextCell(line103, 3, 0);
                line103.BorderWidthRight = 0;
                line103.BorderWidthLeft = 0;
                line103.BorderWidthTop = 0;
                table.AddCell(line103);
                PdfPCell line104 = new PdfPCell(new Phrase(":    " + salarySlip.SabbaticalLeaveCurrentOrPreviousMon, font4));
                AddtextCell(line104, 2, 0);
                line104.BorderWidthLeft = 0;
                line104.BorderWidthTop = 0;
                table.AddCell(line104);

                PdfPCell line111 = new PdfPCell(new Phrase("Standard Monthly Salary", font3));
                AddtextCell(line111, 1, 1);
                line111.BorderWidthRight = 0;
                table.AddCell(line111);
                PdfPCell line112 = new PdfPCell(new Phrase("INR", font3));
                AddtextCell(line112, 2, 1);
                line112.BorderWidthLeft = 0;
                table.AddCell(line112);
                PdfPCell line113 = new PdfPCell(new Phrase("Earnings", font3));
                AddtextCell(line113, 3, 1);
                line113.BorderWidthRight = 0;
                table.AddCell(line113);
                PdfPCell line114 = new PdfPCell(new Phrase("INR", font3));
                AddtextCell(line114, 1, 1);
                line114.BorderWidthLeft = 0;
                table.AddCell(line114);
                PdfPCell line115 = new PdfPCell(new Phrase("Deductions", font3));
                AddtextCell(line115, 3, 1);
                line115.BorderWidthRight = 0;
                table.AddCell(line115);
                PdfPCell line116 = new PdfPCell(new Phrase("INR", font3));
                AddtextCell(line116, 1, 1);
                line116.BorderWidthLeft = 0;
                table.AddCell(line116);


                PdfPCell line121 = new PdfPCell(new Phrase("Basic Salary", font4));
                AddtextCell(line121, 1, 0);
                line121.BorderWidthBottom = 0;
                line121.BorderWidthRight = 0;
                table.AddCell(line121);
                PdfPCell line122 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line122, 2, 2);
                line122.BorderWidthBottom = 0;
                line122.BorderWidthRight = 0;
                table.AddCell(line122);
                PdfPCell line123 = new PdfPCell(new Phrase("Basic Salary", font4));
                AddtextCell(line123, 3, 0);
                line123.BorderWidthBottom = 0;
                line123.BorderWidthRight = 0;
                table.AddCell(line123);
                PdfPCell line124 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line124, 1, 2);
                line124.BorderWidthBottom = 0;
                table.AddCell(line124);
                PdfPCell line125 = new PdfPCell(new Phrase("Power of 1 Deduction", font4));
                AddtextCell(line125, 3, 0);
                line125.BorderWidthBottom = 0;
                table.AddCell(line125);
                PdfPCell line126 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line126, 1, 2);
                line126.BorderWidthBottom = 0;
                table.AddCell(line126);

                PdfPCell line131 = new PdfPCell(new Phrase("HRA", font4));
                AddtextCell(line131, 1, 0);
                line131.BorderWidthTop = 0;
                line131.BorderWidthBottom = 0;
                table.AddCell(line131);
                PdfPCell line132 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line132, 2, 2);
                line132.BorderWidthTop = 0;
                line132.BorderWidthBottom = 0;
                table.AddCell(line132);
                PdfPCell line133 = new PdfPCell(new Phrase("HRA", font4));
                AddtextCell(line133, 3, 0);
                line133.BorderWidthTop = 0;
                line133.BorderWidthBottom = 0;
                table.AddCell(line133);
                PdfPCell line134 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line134, 1, 2);
                line134.BorderWidthTop = 0;
                line134.BorderWidthBottom = 0;
                table.AddCell(line134);
                PdfPCell line135 = new PdfPCell(new Phrase("Ee PF contribution", font4));
                AddtextCell(line135, 3, 0);
                line135.BorderWidthTop = 0;
                line135.BorderWidthBottom = 0;
                table.AddCell(line135);
                PdfPCell line136 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line136, 1, 2);
                line136.BorderWidthTop = 0;
                line136.BorderWidthBottom = 0;
                table.AddCell(line136);

                PdfPCell line141 = new PdfPCell(new Phrase("Conveyance Allowance", font4));
                AddtextCell(line141, 1, 0);
                line141.BorderWidthTop = 0;
                line141.BorderWidthBottom = 0;
                table.AddCell(line141);
                PdfPCell line142 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line142, 2, 2);
                line142.BorderWidthTop = 0;
                line142.BorderWidthBottom = 0;
                table.AddCell(line142);
                PdfPCell line143 = new PdfPCell(new Phrase("Conveyance Allowance", font4));
                AddtextCell(line143, 3, 0);
                line143.BorderWidthTop = 0;
                line143.BorderWidthBottom = 0;
                table.AddCell(line143);
                PdfPCell line144 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line144, 1, 2);
                line144.BorderWidthBottom = 0;
                line144.BorderWidthTop = 0;
                table.AddCell(line144);
                PdfPCell line145 = new PdfPCell(new Phrase("Professional Tax", font4));
                AddtextCell(line145, 3, 0);
                line145.BorderWidthBottom = 0;
                line145.BorderWidthTop = 0;
                table.AddCell(line145);
                PdfPCell line146 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line146, 1, 2);
                line146.BorderWidthBottom = 0;
                line146.BorderWidthTop = 0;
                table.AddCell(line146);

                PdfPCell line151 = new PdfPCell(new Phrase("City Compensatory Allowance", font4));
                AddtextCell(line151, 1, 0);
                line151.BorderWidthBottom = 0;
                line151.BorderWidthTop = 0;
                table.AddCell(line151);
                PdfPCell line152 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line152, 2, 2);
                line152.BorderWidthBottom = 0;
                line152.BorderWidthTop = 0;
                table.AddCell(line152);
                PdfPCell line153 = new PdfPCell(new Phrase("City Compensatory Allowance", font4));
                AddtextCell(line153, 3, 0);
                line153.BorderWidthBottom = 0;
                line153.BorderWidthTop = 0;
                table.AddCell(line153);
                PdfPCell line154 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line154, 1, 2);
                line154.BorderWidthBottom = 0;
                line154.BorderWidthTop = 0;
                table.AddCell(line154);
                PdfPCell line155 = new PdfPCell(new Phrase("Income Tax", font4));
                AddtextCell(line155, 3, 0);
                line155.BorderWidthBottom = 0;
                line155.BorderWidthTop = 0;
                table.AddCell(line155);
                PdfPCell line156 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line156, 1, 2);
                line156.BorderWidthBottom = 0;
                line156.BorderWidthTop = 0;
                table.AddCell(line156);

                PdfPCell line161 = new PdfPCell(new Phrase("Holiday Allowance", font4));
                AddtextCell(line161, 1, 0);
                line161.BorderWidthBottom = 0;
                line161.BorderWidthTop = 0;
                table.AddCell(line161);
                PdfPCell line162 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line162, 2, 2);
                line162.BorderWidthBottom = 0;
                line162.BorderWidthTop = 0;
                table.AddCell(line162);
                PdfPCell line163 = new PdfPCell(new Phrase("Holiday Allowance", font4));
                AddtextCell(line163, 3, 0);
                line163.BorderWidthBottom = 0;
                line163.BorderWidthTop = 0;
                table.AddCell(line163);
                PdfPCell line164 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line164, 1, 2);
                line164.BorderWidthBottom = 0;
                line164.BorderWidthTop = 0;
                table.AddCell(line164);
                PdfPCell line165 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line165, 3, 0);
                line165.BorderWidthBottom = 0;
                line165.BorderWidthTop = 0;
                table.AddCell(line165);
                PdfPCell line166 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line166, 1, 2);
                line166.BorderWidthBottom = 0;
                line166.BorderWidthTop = 0;
                table.AddCell(line166);

                PdfPCell line171 = new PdfPCell(new Phrase("Medical Allowance", font4));
                AddtextCell(line171, 1, 0);
                line171.BorderWidthBottom = 0;
                line171.BorderWidthTop = 0;
                table.AddCell(line171);
                PdfPCell line172 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line172, 2, 2);
                line172.BorderWidthTop = 0;
                line172.BorderWidthBottom = 0;
                table.AddCell(line172);
                PdfPCell line173 = new PdfPCell(new Phrase("Medical Allowance", font4));
                AddtextCell(line173, 3, 0);
                line173.BorderWidthTop = 0;
                line173.BorderWidthBottom = 0;
                table.AddCell(line173);
                PdfPCell line174 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line174, 1, 2);
                line174.BorderWidthTop = 0;
                line174.BorderWidthBottom = 0;
                table.AddCell(line174);
                PdfPCell line175 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line175, 3, 0);
                line175.BorderWidthTop = 0;
                line175.BorderWidthBottom = 0;
                table.AddCell(line175);
                PdfPCell line176 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line176, 1, 2);
                line176.BorderWidthTop = 0;
                line176.BorderWidthBottom = 0;
                table.AddCell(line176);

                PdfPCell line191 = new PdfPCell(new Phrase("Dep Allow. / Perfom Inc", font4));
                AddtextCell(line191, 1, 0);
                line191.BorderWidthBottom = 0;
                line191.BorderWidthTop = 0;
                table.AddCell(line191);
                PdfPCell line192 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line192, 2, 2);
                line192.BorderWidthTop = 0;
                line192.BorderWidthBottom = 0;
                table.AddCell(line192);
                PdfPCell line193 = new PdfPCell(new Phrase("Dep Allow. / Perfom Inc", font4));
                AddtextCell(line193, 3, 0);
                line193.BorderWidthBottom = 0;
                line193.BorderWidthTop = 0;
                table.AddCell(line193);
                PdfPCell line194 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line194, 1, 2);
                line194.BorderWidthTop = 0;
                line194.BorderWidthBottom = 0;
                table.AddCell(line194);
                PdfPCell line195 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line195, 3, 0);
                line195.BorderWidthTop = 0;
                line195.BorderWidthBottom = 0;
                table.AddCell(line195);
                PdfPCell line196 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line196, 1, 2);
                line196.BorderWidthTop = 0;
                line196.BorderWidthBottom = 0;
                table.AddCell(line196);

                PdfPCell line201 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line201, 1, 0);
                line201.BorderWidthBottom = 0;
                line201.BorderWidthTop = 0;
                table.AddCell(line201);
                PdfPCell line202 = new PdfPCell(new Phrase(" ", font4)) { BorderWidthTop = 0 };
                AddtextCell(line202, 2, 2);
                line202.BorderWidthBottom = 0;
                line202.BorderWidthTop = 0;
                table.AddCell(line202);
                PdfPCell line203 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line203, 3, 0);
                line203.BorderWidthBottom = 0;
                line203.BorderWidthTop = 0;
                table.AddCell(line203);
                PdfPCell line204 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line204, 1, 2);
                line204.BorderWidthBottom = 0;
                line204.BorderWidthTop = 0;
                table.AddCell(line204);
                PdfPCell line205 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line205, 3, 0);
                line205.BorderWidthBottom = 0;
                line205.BorderWidthTop = 0;
                table.AddCell(line205);
                PdfPCell line206 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line206, 1, 2);
                line206.BorderWidthBottom = 0;
                line206.BorderWidthTop = 0;
                table.AddCell(line206);

                PdfPCell line211 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line211, 1, 0);
                line211.BorderWidthBottom = 0;
                line211.BorderWidthTop = 0;
                table.AddCell(line211);
                PdfPCell line212 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line212, 2, 2);
                line212.BorderWidthBottom = 0;
                line212.BorderWidthTop = 0;
                table.AddCell(line212);
                PdfPCell line213 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line213, 3, 0);
                line213.BorderWidthBottom = 0;
                line213.BorderWidthTop = 0;
                table.AddCell(line213);
                PdfPCell line214 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line214, 1, 2);
                line214.BorderWidthBottom = 0;
                line214.BorderWidthTop = 0;
                table.AddCell(line214);
                PdfPCell line215 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line215, 3, 0);
                line215.BorderWidthBottom = 0;
                line215.BorderWidthTop = 0;
                table.AddCell(line215);
                PdfPCell line216 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line216, 1, 2);
                line216.BorderWidthTop = 0;
                line216.BorderWidthBottom = 0;
                table.AddCell(line216);

                PdfPCell line221 = new PdfPCell(new Phrase("Total Standard Salary", font3));
                AddtextCell(line221, 1, 0);
                line221.BorderWidthBottom = 0;
                table.AddCell(line221);
                PdfPCell line222 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line222, 2, 2);
                line222.BorderWidthBottom = 0;
                table.AddCell(line222);
                PdfPCell line223 = new PdfPCell(new Phrase("Gross Earnings", font3));
                AddtextCell(line223, 3, 0);
                line223.BorderWidthBottom = 0;
                table.AddCell(line223);
                PdfPCell line224 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line224, 1, 2);
                line224.BorderWidthBottom = 0;
                table.AddCell(line224);
                PdfPCell line225 = new PdfPCell(new Phrase("Gross Deductions", font3));
                AddtextCell(line225, 3, 0);
                line225.BorderWidthBottom = 0;
                table.AddCell(line225);
                PdfPCell line226 = new PdfPCell(new Phrase("", font4));
                AddtextCell(line226, 1, 2);
                line226.BorderWidthBottom = 0;
                table.AddCell(line226);

                PdfPCell line231 = new PdfPCell(new Phrase(" ", font3));
                AddtextCell(line231, 7, 0);
                line231.BorderWidthLeft = 0;
                table.AddCell(line231);
                PdfPCell line232 = new PdfPCell(new Phrase("Net Pay", font3));
                AddtextCell(line232, 3, 0);
                line232.BorderWidthRight = 0;
                table.AddCell(line232);
                PdfPCell line233 = new PdfPCell(new Phrase("", font3));
                AddtextCell(line233, 1, 2);
                line233.BorderWidthLeft = 0;
                table.AddCell(line233);

                PdfPCell line241 = new PdfPCell(new Phrase("Income Tax Computation", font1));
                AddtextCell(line241, 12, 1);
                line241.BorderWidthBottom = 0;
                table.AddCell(line241);

                PdfPCell line300 = new PdfPCell(new Phrase("Exemption U/S 10", font3));
                AddtextCell(line300, 2, 1);
                line300.BorderWidthBottom = 0;
                table.AddCell(line300);
                PdfPCell line301 = new PdfPCell(new Phrase("Projected / Actual Taxable Salary", font3));
                AddtextCell(line301, 3, 1);
                line301.BorderWidthBottom = 0;
                table.AddCell(line301);
                PdfPCell line302 = new PdfPCell(new Phrase("Contribution under Chapter VI A", font3));
                AddtextCell(line302, 4, 1);
                line302.BorderWidthBottom = 0;
                table.AddCell(line302);
                PdfPCell line303 = new PdfPCell(new Phrase("Monthly Tax Deduction", font3));
                AddtextCell(line303, 2, 1);
                line303.BorderWidthBottom = 0;
                table.AddCell(line303);

                PdfPCell line311 = new PdfPCell(new Phrase("HRA Annual Exemption", font4));
                AddtextCell(line311, 1, 0);
                line311.BorderWidthBottom = 0;
                line311.BorderWidthRight = 0;
                table.AddCell(line311);
                PdfPCell line312 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line312, 1, 2);
                line312.BorderWidthBottom = 0;
                line312.BorderWidthLeft = 0;
                table.AddCell(line312);
                PdfPCell line313 = new PdfPCell(new Phrase("Months remaining till March", font4));
                AddtextCell(line313, 2, 0);
                line313.BorderWidthRight = 0;
                line313.BorderWidthBottom = 0;
                table.AddCell(line313);
                PdfPCell line314 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line314, 1, 2);
                line314.BorderWidthLeft = 0;
                line314.BorderWidthBottom = 0;
                table.AddCell(line314);
                PdfPCell line315 = new PdfPCell(new Phrase("Provident Fund", font4));
                AddtextCell(line315, 3, 0);
                line315.BorderWidthBottom = 0;
                table.AddCell(line315);
                PdfPCell line316 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line316, 1, 2);
                line316.BorderWidthBottom = 0;
                table.AddCell(line316);
                PdfPCell line317 = new PdfPCell(new Phrase("April'15", font4));
                AddtextCell(line317, 1, 0);
                line317.BorderWidthBottom = 0;
                line317.BorderWidthRight = 0;
                table.AddCell(line317);
                PdfPCell line318 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line318, 1, 1);
                line318.BorderWidthBottom = 0;
                line318.BorderWidthLeft = 0;
                table.AddCell(line318);

                PdfPCell line321 = new PdfPCell(new Phrase("Conveyance Annual Exempt", font4));
                AddtextCell(line321, 1, 0);
                line321.BorderWidthTop = 0;
                line321.BorderWidthBottom = 0;
                line321.BorderWidthRight = 0;
                table.AddCell(line321);
                PdfPCell line322 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line322, 1, 2);
                line322.BorderWidthBottom = 0;
                line322.BorderWidthTop = 0;
                line322.BorderWidthLeft = 0;
                table.AddCell(line322);
                PdfPCell line323 = new PdfPCell(new Phrase("Taxable Income till Pr. Month", font4));
                AddtextCell(line323, 2, 0);
                line323.BorderWidthBottom = 0;
                table.AddCell(line323);
                PdfPCell line324 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line324, 1, 2);
                line324.BorderWidthBottom = 0;
                table.AddCell(line324);
                PdfPCell line325 = new PdfPCell(new Phrase("Voluntary PF", font4));
                AddtextCell(line325, 3, 0);
                line325.BorderWidthBottom = 0;
                line325.BorderWidthTop = 0;
                table.AddCell(line325);
                PdfPCell line326 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line326, 1, 2);
                line326.BorderWidthBottom = 0;
                line326.BorderWidthTop = 0;
                table.AddCell(line326);
                PdfPCell line327 = new PdfPCell(new Phrase("May'15", font4));
                AddtextCell(line327, 1, 0);
                line327.BorderWidthBottom = 0;
                line327.BorderWidthTop = 0;
                line327.BorderWidthRight = 0;
                table.AddCell(line327);
                PdfPCell line328 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line328, 1, 1);
                line328.BorderWidthBottom = 0;
                line328.BorderWidthTop = 0;
                line328.BorderWidthLeft = 0;
                table.AddCell(line328);

                PdfPCell line331 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line331, 1, 0);
                line331.BorderWidthTop = 0;
                line331.BorderWidthRight = 0;
                line331.BorderWidthBottom = 0;
                table.AddCell(line331);
                PdfPCell line332 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line332, 1, 2);
                line332.BorderWidthTop = 0;
                line332.BorderWidthLeft = 0;
                line332.BorderWidthBottom = 0;
                table.AddCell(line332);
                PdfPCell line333 = new PdfPCell(new Phrase("Current Mth Taxable income", font4));
                AddtextCell(line333, 2, 0);
                line333.BorderWidthTop = 0;
                line333.BorderWidthBottom = 0;
                table.AddCell(line333);
                PdfPCell line334 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line334, 1, 2);
                line334.BorderWidthTop = 0;
                line334.BorderWidthBottom = 0;
                table.AddCell(line334);
                PdfPCell line335 = new PdfPCell(new Phrase("Payment towards Life Insurance", font4));
                AddtextCell(line335, 3, 0);
                line335.BorderWidthBottom = 0;
                line335.BorderWidthTop = 0;
                table.AddCell(line335);
                PdfPCell line336 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line336, 1, 2);
                line336.BorderWidthBottom = 0;
                line336.BorderWidthTop = 0;
                table.AddCell(line336);
                PdfPCell line337 = new PdfPCell(new Phrase("June'15", font4));
                AddtextCell(line337, 1, 0);
                line337.BorderWidthBottom = 0;
                line337.BorderWidthTop = 0;
                line337.BorderWidthRight = 0;
                table.AddCell(line337);
                PdfPCell line338 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line338, 1, 1);
                line338.BorderWidthBottom = 0;
                line338.BorderWidthTop = 0;
                line338.BorderWidthLeft = 0;
                table.AddCell(line338);

                PdfPCell line341 = new PdfPCell(new Phrase("Total ", font4));
                AddtextCell(line341, 1, 0);
                line341.BorderWidthRight = 0;
                line341.BorderWidthBottom = 0;
                table.AddCell(line341);
                PdfPCell line342 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line342, 1, 2);
                line342.BorderWidthLeft = 0;
                line342.BorderWidthBottom = 0;
                table.AddCell(line342);
                PdfPCell line343 = new PdfPCell(new Phrase("Projected Standard Salary", font4));
                AddtextCell(line343, 2, 0);
                line343.BorderWidthTop = 0;
                line343.BorderWidthBottom = 0;
                table.AddCell(line343);
                PdfPCell line344 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line344, 1, 2);
                line344.BorderWidthTop = 0;
                line344.BorderWidthBottom = 0;
                table.AddCell(line344);
                PdfPCell line345 = new PdfPCell(new Phrase("Policy", font4));
                AddtextCell(line345, 3, 0);
                line345.BorderWidthBottom = 0;
                line345.BorderWidthTop = 0;
                table.AddCell(line345);
                PdfPCell line346 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line346, 1, 2);
                line346.BorderWidthBottom = 0;
                line346.BorderWidthTop = 0;
                table.AddCell(line346);
                PdfPCell line347 = new PdfPCell(new Phrase("July'15", font4));
                AddtextCell(line347, 1, 0);
                line347.BorderWidthBottom = 0;
                line347.BorderWidthTop = 0;
                line347.BorderWidthRight = 0;
                table.AddCell(line347);
                PdfPCell line348 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line348, 1, 1);
                line348.BorderWidthBottom = 0;
                line348.BorderWidthTop = 0;
                line348.BorderWidthLeft = 0;
                table.AddCell(line348);

                PdfPCell line351 = new PdfPCell(new Phrase("HRA Exemption ", font3));
                AddtextCell(line351, 1, 1);
                line351.BorderWidthRight = 0;
                line351.BorderWidthBottom = 0;
                table.AddCell(line351);
                PdfPCell line352 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line352, 1, 2);
                line352.BorderWidthLeft = 0;
                line352.BorderWidthBottom = 0;
                table.AddCell(line352);
                PdfPCell line353 = new PdfPCell(new Phrase("Taxable Ann Perks", font4));
                AddtextCell(line353, 2, 0);
                line353.BorderWidthTop = 0;
                line353.BorderWidthBottom = 0;
                table.AddCell(line353);
                PdfPCell line354 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line354, 1, 2);
                line354.BorderWidthTop = 0;
                line354.BorderWidthBottom = 0;
                table.AddCell(line354);
                PdfPCell line355 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line355, 3, 0);
                line355.BorderWidthBottom = 0;
                line355.BorderWidthTop = 0;
                table.AddCell(line355);
                PdfPCell line356 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line356, 1, 2);
                line356.BorderWidthBottom = 0;
                line356.BorderWidthTop = 0;
                table.AddCell(line356);
                PdfPCell line357 = new PdfPCell(new Phrase("August'15", font4));
                AddtextCell(line357, 1, 0);
                line357.BorderWidthBottom = 0;
                line357.BorderWidthTop = 0;
                line357.BorderWidthRight = 0;
                table.AddCell(line357);
                PdfPCell line358 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line358, 1, 1);
                line358.BorderWidthBottom = 0;
                line358.BorderWidthTop = 0;
                line358.BorderWidthLeft = 0;
                table.AddCell(line358);

                PdfPCell line361 = new PdfPCell(new Phrase("NMetro -HRA Rcvd/ Proj ", font4));
                AddtextCell(line361, 1, 0);
                line361.BorderWidthRight = 0;
                line361.BorderWidthBottom = 0;
                table.AddCell(line361);
                PdfPCell line362 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line362, 1, 2);
                line362.BorderWidthLeft = 0;
                line362.BorderWidthBottom = 0;
                table.AddCell(line362);
                PdfPCell line363 = new PdfPCell(new Phrase("Annual Medical Exemption", font4));
                AddtextCell(line363, 2, 0);
                line363.BorderWidthTop = 0;
                line363.BorderWidthBottom = 0;
                table.AddCell(line363);
                PdfPCell line364 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line364, 1, 2);
                line364.BorderWidthTop = 0;
                line364.BorderWidthBottom = 0;
                table.AddCell(line364);
                PdfPCell line365 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line365, 3, 0);
                line365.BorderWidthBottom = 0;
                line365.BorderWidthTop = 0;
                table.AddCell(line365);
                PdfPCell line366 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line366, 1, 2);
                line366.BorderWidthBottom = 0;
                line366.BorderWidthTop = 0;
                table.AddCell(line366);
                PdfPCell line367 = new PdfPCell(new Phrase("September'15", font4));
                AddtextCell(line367, 1, 0);
                line367.BorderWidthBottom = 0;
                line367.BorderWidthTop = 0;
                line367.BorderWidthRight = 0;
                table.AddCell(line367);
                PdfPCell line368 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line368, 1, 1);
                line368.BorderWidthBottom = 0;
                line368.BorderWidthTop = 0;
                line368.BorderWidthLeft = 0;
                table.AddCell(line368);

                PdfPCell line371 = new PdfPCell(new Phrase("* NMetro - 40% of Basic", font4));
                AddtextCell(line371, 1, 0);
                line371.BorderWidthRight = 0;
                line371.BorderWidthBottom = 0;
                line371.BorderWidthTop = 0;
                table.AddCell(line371);
                PdfPCell line372 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line372, 1, 2);
                line372.BorderWidthLeft = 0;
                line372.BorderWidthBottom = 0;
                line372.BorderWidthTop = 0;
                table.AddCell(line372);
                PdfPCell line373 = new PdfPCell(new Phrase("Gross Salary", font4));
                AddtextCell(line373, 2, 0);
                line373.BorderWidthTop = 0;
                line373.BorderWidthBottom = 0;
                table.AddCell(line373);
                PdfPCell line374 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line374, 1, 2);
                line374.BorderWidthTop = 0;
                line374.BorderWidthBottom = 0;
                table.AddCell(line374);
                PdfPCell line375 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line375, 3, 0);
                line375.BorderWidthBottom = 0;
                line375.BorderWidthTop = 0;
                table.AddCell(line375);
                PdfPCell line376 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line376, 1, 2);
                line376.BorderWidthBottom = 0;
                line376.BorderWidthTop = 0;
                table.AddCell(line376);
                PdfPCell line377 = new PdfPCell(new Phrase("October'15", font4));
                AddtextCell(line377, 1, 0);
                line377.BorderWidthBottom = 0;
                line377.BorderWidthTop = 0;
                line377.BorderWidthRight = 0;
                table.AddCell(line377);
                PdfPCell line378 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line378, 1, 1);
                line378.BorderWidthBottom = 0;
                line378.BorderWidthTop = 0;
                line378.BorderWidthLeft = 0;
                table.AddCell(line378);

                PdfPCell line381 = new PdfPCell(new Phrase("NMetro Rent Paid - 10 % B", font4));
                AddtextCell(line381, 1, 0);
                line381.BorderWidthRight = 0;
                line381.BorderWidthBottom = 0;
                line381.BorderWidthTop = 0;
                table.AddCell(line381);
                PdfPCell line382 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line382, 1, 2);
                line382.BorderWidthLeft = 0;
                line382.BorderWidthBottom = 0;
                line382.BorderWidthTop = 0;
                table.AddCell(line382);
                PdfPCell line383 = new PdfPCell(new Phrase("Exemption U/S 10", font4));
                AddtextCell(line383, 2, 0);
                line383.BorderWidthTop = 0;
                line383.BorderWidthBottom = 0;
                table.AddCell(line383);
                PdfPCell line384 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line384, 1, 2);
                line384.BorderWidthTop = 0;
                line384.BorderWidthBottom = 0;
                table.AddCell(line384);
                PdfPCell line385 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line385, 3, 0);
                line385.BorderWidthBottom = 0;
                line385.BorderWidthTop = 0;
                table.AddCell(line385);
                PdfPCell line386 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line386, 1, 2);
                line386.BorderWidthBottom = 0;
                line386.BorderWidthTop = 0;
                table.AddCell(line386);
                PdfPCell line387 = new PdfPCell(new Phrase("November'15", font4));
                AddtextCell(line387, 1, 0);
                line387.BorderWidthBottom = 0;
                line387.BorderWidthTop = 0;
                line387.BorderWidthRight = 0;
                table.AddCell(line387);
                PdfPCell line388 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line388, 1, 1);
                line388.BorderWidthBottom = 0;
                line388.BorderWidthTop = 0;
                line388.BorderWidthLeft = 0;
                table.AddCell(line388);

                PdfPCell line391 = new PdfPCell(new Phrase("Least of Metro/NMetro is exempt", font4));
                AddtextCell(line391, 1, 0);
                line391.BorderWidthRight = 0;
                line391.BorderWidthBottom = 0;
                line391.BorderWidthTop = 0;
                table.AddCell(line391);
                PdfPCell line392 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line392, 1, 2);
                line392.BorderWidthLeft = 0;
                line392.BorderWidthBottom = 0;
                line392.BorderWidthTop = 0;
                table.AddCell(line392);
                PdfPCell line393 = new PdfPCell(new Phrase("Tax on Employment (Prof. Tax)", font4));
                AddtextCell(line393, 2, 0);
                line393.BorderWidthTop = 0;
                line393.BorderWidthBottom = 0;
                table.AddCell(line393);
                PdfPCell line394 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line394, 1, 2);
                line394.BorderWidthTop = 0;
                line394.BorderWidthBottom = 0;
                table.AddCell(line394);
                PdfPCell line395 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line395, 3, 0);
                line395.BorderWidthBottom = 0;
                line395.BorderWidthTop = 0;
                table.AddCell(line395);
                PdfPCell line396 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line396, 1, 2);
                line396.BorderWidthBottom = 0;
                line396.BorderWidthTop = 0;
                table.AddCell(line396);
                PdfPCell line397 = new PdfPCell(new Phrase("December'15", font4));
                AddtextCell(line397, 1, 0);
                line397.BorderWidthBottom = 0;
                line397.BorderWidthTop = 0;
                line397.BorderWidthRight = 0;
                table.AddCell(line397);
                PdfPCell line398 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line398, 1, 1);
                line398.BorderWidthBottom = 0;
                line398.BorderWidthTop = 0;
                line398.BorderWidthLeft = 0;
                table.AddCell(line398);

                PdfPCell line401 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line401, 1, 0);
                line401.BorderWidthRight = 0;
                line401.BorderWidthBottom = 0;
                line401.BorderWidthTop = 0;
                table.AddCell(line401);
                PdfPCell line402 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line402, 1, 2);
                line402.BorderWidthLeft = 0;
                line402.BorderWidthBottom = 0;
                line402.BorderWidthTop = 0;
                table.AddCell(line402);
                PdfPCell line403 = new PdfPCell(new Phrase("Income under Head Salary", font4));
                AddtextCell(line403, 2, 0);
                line403.BorderWidthTop = 0;
                line403.BorderWidthBottom = 0;
                table.AddCell(line403);
                PdfPCell line404 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line404, 1, 2);
                line404.BorderWidthTop = 0;
                line404.BorderWidthBottom = 0;
                table.AddCell(line404);
                PdfPCell line405 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line405, 3, 0);
                line405.BorderWidthBottom = 0;
                line405.BorderWidthTop = 0;
                table.AddCell(line405);
                PdfPCell line406 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line406, 1, 2);
                line406.BorderWidthBottom = 0;
                line406.BorderWidthTop = 0;
                table.AddCell(line406);
                PdfPCell line407 = new PdfPCell(new Phrase("January'16", font4));
                AddtextCell(line407, 1, 0);
                line407.BorderWidthBottom = 0;
                line407.BorderWidthTop = 0;
                line407.BorderWidthRight = 0;
                table.AddCell(line407);
                PdfPCell line408 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line408, 1, 1);
                line408.BorderWidthBottom = 0;
                line408.BorderWidthTop = 0;
                line408.BorderWidthLeft = 0;
                table.AddCell(line408);

                PdfPCell line411 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line411, 1, 0);
                line411.BorderWidthRight = 0;
                line411.BorderWidthBottom = 0;
                line411.BorderWidthTop = 0;
                table.AddCell(line411);
                PdfPCell line412 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line412, 1, 2);
                line412.BorderWidthLeft = 0;
                line412.BorderWidthBottom = 0;
                line412.BorderWidthTop = 0;
                table.AddCell(line412);
                PdfPCell line413 = new PdfPCell(new Phrase("Interest on House Property", font4));
                AddtextCell(line413, 2, 0);
                line413.BorderWidthTop = 0;
                line413.BorderWidthBottom = 0;
                table.AddCell(line413);
                PdfPCell line414 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line414, 1, 2);
                line414.BorderWidthTop = 0;
                line414.BorderWidthBottom = 0;
                table.AddCell(line414);
                PdfPCell line415 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line415, 3, 0);
                line415.BorderWidthBottom = 0;
                line415.BorderWidthTop = 0;
                table.AddCell(line415);
                PdfPCell line416 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line416, 1, 2);
                line416.BorderWidthBottom = 0;
                line416.BorderWidthTop = 0;
                table.AddCell(line416);
                PdfPCell line417 = new PdfPCell(new Phrase("February'16", font4));
                AddtextCell(line417, 1, 0);
                line417.BorderWidthBottom = 0;
                line417.BorderWidthTop = 0;
                line417.BorderWidthRight = 0;
                table.AddCell(line417);
                PdfPCell line418 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line418, 1, 1);
                line418.BorderWidthBottom = 0;
                line418.BorderWidthTop = 0;
                line418.BorderWidthLeft = 0;
                table.AddCell(line418);

                PdfPCell line421 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line421, 1, 0);
                line421.BorderWidthRight = 0;
                line421.BorderWidthBottom = 0;
                line421.BorderWidthTop = 0;
                table.AddCell(line421);
                PdfPCell line422 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line422, 1, 2);
                line422.BorderWidthLeft = 0;
                line422.BorderWidthBottom = 0;
                line422.BorderWidthTop = 0;
                table.AddCell(line422);
                PdfPCell line423 = new PdfPCell(new Phrase("Gross Total Income", font4));
                AddtextCell(line423, 2, 0);
                line423.BorderWidthTop = 0;
                line423.BorderWidthBottom = 0;
                table.AddCell(line423);
                PdfPCell line424 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line424, 1, 2);
                line424.BorderWidthTop = 0;
                line424.BorderWidthBottom = 0;
                table.AddCell(line424);
                PdfPCell line425 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line425, 3, 0);
                line425.BorderWidthBottom = 0;
                line425.BorderWidthTop = 0;
                table.AddCell(line425);
                PdfPCell line426 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line426, 1, 2);
                line426.BorderWidthBottom = 0;
                line426.BorderWidthTop = 0;
                table.AddCell(line426);
                PdfPCell line427 = new PdfPCell(new Phrase("March'16", font4));
                AddtextCell(line427, 1, 0);
                line427.BorderWidthBottom = 0;
                line427.BorderWidthTop = 0;
                line427.BorderWidthRight = 0;
                table.AddCell(line427);
                PdfPCell line428 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line428, 1, 1);
                line428.BorderWidthBottom = 0;
                line428.BorderWidthTop = 0;
                line428.BorderWidthLeft = 0;
                table.AddCell(line428);

                PdfPCell line501 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line501, 1, 0);
                line501.BorderWidthRight = 0;
                line501.BorderWidthBottom = 0;
                line501.BorderWidthTop = 0;
                table.AddCell(line501);
                PdfPCell line502 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line502, 1, 2);
                line502.BorderWidthLeft = 0;
                line502.BorderWidthBottom = 0;
                line502.BorderWidthTop = 0;
                table.AddCell(line502);
                PdfPCell line503 = new PdfPCell(new Phrase("Agg of Chapter VI", font4));
                AddtextCell(line503, 2, 0);
                line503.BorderWidthTop = 0;
                line503.BorderWidthBottom = 0;
                table.AddCell(line503);
                PdfPCell line504 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line504, 1, 2);
                line504.BorderWidthTop = 0;
                line504.BorderWidthBottom = 0;
                table.AddCell(line504);
                PdfPCell line505 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line505, 3, 0);
                line505.BorderWidthBottom = 0;
                line505.BorderWidthTop = 0;
                table.AddCell(line505);
                PdfPCell line506 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line506, 1, 2);
                line506.BorderWidthBottom = 0;
                line506.BorderWidthTop = 0;
                table.AddCell(line506);
                PdfPCell line507 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line507, 1, 0);
                line507.BorderWidthBottom = 0;
                line507.BorderWidthTop = 0;
                line507.BorderWidthRight = 0;
                table.AddCell(line507);
                PdfPCell line508 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line508, 1, 1);
                line508.BorderWidthBottom = 0;
                line508.BorderWidthTop = 0;
                line508.BorderWidthLeft = 0;
                table.AddCell(line508);

                PdfPCell line511 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line511, 1, 0);
                line511.BorderWidthRight = 0;
                line511.BorderWidthBottom = 0;
                line511.BorderWidthTop = 0;
                table.AddCell(line511);
                PdfPCell line512 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line512, 1, 2);
                line512.BorderWidthLeft = 0;
                line512.BorderWidthBottom = 0;
                line512.BorderWidthTop = 0;
                table.AddCell(line512);
                PdfPCell line513 = new PdfPCell(new Phrase("Total Income", font4));
                AddtextCell(line513, 2, 0);
                line513.BorderWidthTop = 0;
                line513.BorderWidthBottom = 0;
                table.AddCell(line513);
                PdfPCell line514 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line514, 1, 2);
                line514.BorderWidthTop = 0;
                line514.BorderWidthBottom = 0;
                table.AddCell(line514);
                PdfPCell line515 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line515, 3, 0);
                line515.BorderWidthBottom = 0;
                line515.BorderWidthTop = 0;
                table.AddCell(line515);
                PdfPCell line516 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line516, 1, 2);
                line516.BorderWidthBottom = 0;
                line516.BorderWidthTop = 0;
                table.AddCell(line516);
                PdfPCell line517 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line517, 1, 0);
                line517.BorderWidthBottom = 0;
                line517.BorderWidthTop = 0;
                line517.BorderWidthRight = 0;
                table.AddCell(line517);
                PdfPCell line518 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line518, 1, 1);
                line518.BorderWidthBottom = 0;
                line518.BorderWidthTop = 0;
                line518.BorderWidthLeft = 0;
                table.AddCell(line518);

                PdfPCell line521 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line521, 1, 0);
                line521.BorderWidthRight = 0;
                line521.BorderWidthBottom = 0;
                line521.BorderWidthTop = 0;
                table.AddCell(line521);
                PdfPCell line522 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line522, 1, 2);
                line522.BorderWidthLeft = 0;
                line522.BorderWidthBottom = 0;
                line522.BorderWidthTop = 0;
                table.AddCell(line522);
                PdfPCell line523 = new PdfPCell(new Phrase("Tax on Total Income", font4));
                AddtextCell(line523, 2, 0);
                line523.BorderWidthTop = 0;
                line523.BorderWidthBottom = 0;
                table.AddCell(line523);
                PdfPCell line524 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line524, 1, 2);
                line524.BorderWidthTop = 0;
                line524.BorderWidthBottom = 0;
                table.AddCell(line524);
                PdfPCell line525 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line525, 3, 0);
                line525.BorderWidthBottom = 0;
                line525.BorderWidthTop = 0;
                table.AddCell(line525);
                PdfPCell line526 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line526, 1, 2);
                line526.BorderWidthBottom = 0;
                line526.BorderWidthTop = 0;
                table.AddCell(line526);
                PdfPCell line527 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line527, 1, 0);
                line527.BorderWidthBottom = 0;
                line527.BorderWidthTop = 0;
                line527.BorderWidthRight = 0;
                table.AddCell(line527);
                PdfPCell line528 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line528, 1, 1);
                line528.BorderWidthBottom = 0;
                line528.BorderWidthTop = 0;
                line528.BorderWidthLeft = 0;
                table.AddCell(line528);

                PdfPCell line531 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line531, 1, 0);
                line531.BorderWidthRight = 0;
                line531.BorderWidthBottom = 0;
                line531.BorderWidthTop = 0;
                table.AddCell(line531);
                PdfPCell line532 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line532, 1, 2);
                line532.BorderWidthLeft = 0;
                line532.BorderWidthBottom = 0;
                line532.BorderWidthTop = 0;
                table.AddCell(line532);
                PdfPCell line533 = new PdfPCell(new Phrase("Tax Credit", font4));
                AddtextCell(line533, 2, 0);
                line533.BorderWidthTop = 0;
                line533.BorderWidthBottom = 0;
                table.AddCell(line533);
                PdfPCell line534 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line534, 1, 2);
                line534.BorderWidthTop = 0;
                line534.BorderWidthBottom = 0;
                table.AddCell(line534);
                PdfPCell line535 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line535, 3, 0);
                line535.BorderWidthBottom = 0;
                line535.BorderWidthTop = 0;
                table.AddCell(line535);
                PdfPCell line536 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line536, 1, 2);
                line536.BorderWidthBottom = 0;
                line536.BorderWidthTop = 0;
                table.AddCell(line536);
                PdfPCell line537 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line537, 1, 0);
                line537.BorderWidthBottom = 0;
                line537.BorderWidthTop = 0;
                line537.BorderWidthRight = 0;
                table.AddCell(line537);
                PdfPCell line538 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line538, 1, 1);
                line538.BorderWidthBottom = 0;
                line538.BorderWidthTop = 0;
                line538.BorderWidthLeft = 0;
                table.AddCell(line538);

                PdfPCell line541 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line541, 1, 0);
                line541.BorderWidthRight = 0;
                line541.BorderWidthBottom = 0;
                line541.BorderWidthTop = 0;
                table.AddCell(line541);
                PdfPCell line542 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line542, 1, 2);
                line542.BorderWidthLeft = 0;
                line542.BorderWidthBottom = 0;
                line542.BorderWidthTop = 0;
                table.AddCell(line542);
                PdfPCell line543 = new PdfPCell(new Phrase("Education Cess (On Net Tax)", font4));
                AddtextCell(line543, 2, 0);
                line543.BorderWidthTop = 0;
                line543.BorderWidthBottom = 0;
                table.AddCell(line543);
                PdfPCell line544 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line544, 1, 2);
                line544.BorderWidthTop = 0;
                line544.BorderWidthBottom = 0;
                table.AddCell(line544);
                PdfPCell line545 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line545, 3, 0);
                line545.BorderWidthBottom = 0;
                line545.BorderWidthTop = 0;
                table.AddCell(line545);
                PdfPCell line546 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line546, 1, 2);
                line546.BorderWidthBottom = 0;
                line546.BorderWidthTop = 0;
                table.AddCell(line546);
                PdfPCell line547 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line547, 1, 0);
                line547.BorderWidthBottom = 0;
                line547.BorderWidthTop = 0;
                line547.BorderWidthRight = 0;
                table.AddCell(line547);
                PdfPCell line548 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line548, 1, 1);
                line548.BorderWidthBottom = 0;
                line548.BorderWidthTop = 0;
                line548.BorderWidthLeft = 0;
                table.AddCell(line548);

                PdfPCell line551 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line551, 1, 0);
                line551.BorderWidthRight = 0;
                line551.BorderWidthBottom = 0;
                line551.BorderWidthTop = 0;
                table.AddCell(line551);
                PdfPCell line552 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line552, 1, 2);
                line552.BorderWidthLeft = 0;
                line552.BorderWidthBottom = 0;
                line552.BorderWidthTop = 0;
                table.AddCell(line552);
                PdfPCell line553 = new PdfPCell(new Phrase("Tax payable", font4));
                AddtextCell(line553, 2, 0);
                line553.BorderWidthTop = 0;
                line553.BorderWidthBottom = 0;
                table.AddCell(line553);
                PdfPCell line554 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line554, 1, 2);
                line554.BorderWidthTop = 0;
                line554.BorderWidthBottom = 0;
                table.AddCell(line554);
                PdfPCell line555 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line555, 3, 0);
                line555.BorderWidthBottom = 0;
                line555.BorderWidthTop = 0;
                table.AddCell(line555);
                PdfPCell line556 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line556, 1, 2);
                line556.BorderWidthBottom = 0;
                line556.BorderWidthTop = 0;
                table.AddCell(line556);
                PdfPCell line557 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line557, 1, 0);
                line557.BorderWidthBottom = 0;
                line557.BorderWidthTop = 0;
                line557.BorderWidthRight = 0;
                table.AddCell(line557);
                PdfPCell line558 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line558, 1, 1);
                line558.BorderWidthBottom = 0;
                line558.BorderWidthTop = 0;
                line558.BorderWidthLeft = 0;
                table.AddCell(line558);

                PdfPCell line561 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line561, 1, 0);
                line561.BorderWidthRight = 0;
                line561.BorderWidthBottom = 0;
                line561.BorderWidthTop = 0;
                table.AddCell(line561);
                PdfPCell line562 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line562, 1, 2);
                line562.BorderWidthLeft = 0;
                line562.BorderWidthBottom = 0;
                line562.BorderWidthTop = 0;
                table.AddCell(line562);
                PdfPCell line563 = new PdfPCell(new Phrase("Tax deducted so far", font4));
                AddtextCell(line563, 2, 0);
                line563.BorderWidthTop = 0;
                line563.BorderWidthBottom = 0;
                table.AddCell(line563);
                PdfPCell line564 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line564, 1, 2);
                line564.BorderWidthTop = 0;
                line564.BorderWidthBottom = 0;
                table.AddCell(line564);
                PdfPCell line565 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line565, 3, 0);
                line565.BorderWidthBottom = 0;
                line565.BorderWidthTop = 0;
                table.AddCell(line565);
                PdfPCell line566 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line566, 1, 2);
                line566.BorderWidthBottom = 0;
                line566.BorderWidthTop = 0;
                table.AddCell(line566);
                PdfPCell line567 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line567, 1, 0);
                line567.BorderWidthBottom = 0;
                line567.BorderWidthTop = 0;
                line567.BorderWidthRight = 0;
                table.AddCell(line567);
                PdfPCell line568 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line568, 1, 1);
                line568.BorderWidthBottom = 0;
                line568.BorderWidthTop = 0;
                line568.BorderWidthLeft = 0;
                table.AddCell(line568);

                PdfPCell line571 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line571, 1, 0);
                line571.BorderWidthRight = 0;
                line571.BorderWidthTop = 0;
                table.AddCell(line571);
                PdfPCell line572 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line572, 1, 2);
                line572.BorderWidthLeft = 0;
                line572.BorderWidthTop = 0;
                table.AddCell(line572);
                PdfPCell line573 = new PdfPCell(new Phrase("Balance Tax", font4));
                AddtextCell(line573, 2, 0);
                line573.BorderWidthTop = 0;
                table.AddCell(line573);
                PdfPCell line574 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line574, 1, 2);
                line574.BorderWidthTop = 0;
                table.AddCell(line574);
                PdfPCell line575 = new PdfPCell(new Phrase("Total ", font3));
                AddtextCell(line575, 3, 0);
                table.AddCell(line575);
                PdfPCell line576 = new PdfPCell(new Phrase(" ", font3));
                AddtextCell(line576, 1, 2);
                table.AddCell(line576);
                PdfPCell line577 = new PdfPCell(new Phrase("Total ", font3));
                AddtextCell(line577, 1, 0);
                line577.BorderWidthRight = 0;
                table.AddCell(line577);
                PdfPCell line578 = new PdfPCell(new Phrase(" ", font4));
                AddtextCell(line578, 1, 1);
                line578.BorderWidthLeft = 0;
                table.AddCell(line578);

                PdfPCell line588 = new PdfPCell(new Phrase(" *This is a computer generated payslip and doesn't require signature or any company seal. All one time payments like PB,taxable LTA,variable pay etc will be subject to one time tax deduction at your applicable tax slab \n *The current month pay slip has got generated after consideration of payroll input i.e. compensation letter, flexi declaration, one-timer payment input provided and approved transfers till 24th of this month", font4));
                AddtextCell(line588, 11, 0);
                line588.BorderWidthLeft = 0;
                line588.BorderWidthRight = 0;
                line588.BorderWidthBottom = 0;
                table.AddCell(line588);

                document.Open();
                document.Add(table);
                document.Add(image);


            }
            finally
            {
                document.Close();
                ShowPdf(filename);
            }
        }
        private void AddtextCell(PdfPCell cell, int colspan, int allignment)
        {
            cell.Colspan = colspan;
            cell.HorizontalAlignment = allignment; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BorderColorLeft = BaseColor.BLACK;
            cell.BorderColorRight = BaseColor.BLACK;
            cell.BorderColorTop = BaseColor.BLACK;
            cell.BorderColorBottom = BaseColor.BLACK;
            cell.BorderWidthLeft = .1f;
            cell.BorderWidthRight = .1f;
            cell.BorderWidthTop = .2f;
            cell.BorderWidthBottom = .2f;
            //cell.PaddingBottom = 3f;

        }

        #endregion Private Methods
    }
}