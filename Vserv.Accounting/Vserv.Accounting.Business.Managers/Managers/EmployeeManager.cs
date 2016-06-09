#region Namespaces

using System;
using System.Collections.Generic;
using Vserv.Accounting.Common;
using Vserv.Accounting.Common.Enums;
using Vserv.Accounting.Data;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Data.Entity.Models;
using Vserv.Common.Contracts;
using System.Linq;

#endregion

namespace Vserv.Accounting.Business.Managers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Vserv.Accounting.Business.Managers.ManagerBase" />
    public class EmployeeManager : ManagerBase
    {
        #region Properties

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeManager"/> class.
        /// </summary>
        public EmployeeManager()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeManager"/> class.
        /// </summary>
        /// <param name="dataRepositoryFactory">The data repository factory.</param>
        public EmployeeManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetEmployees(EmployeeFilter employeeFilter)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetEmployees(employeeFilter);
            });
        }

        /// <summary>
        /// Gets the employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public Employee GetEmployee(int employeeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetEmployee(employeeId);
            });
        }

        /// <summary>
        /// Deletes the employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        public void DeleteEmployee(int employeeId)
        {
            ExecuteFaultHandledOperation(() =>
          {
              var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
              _employeeRepository.DeleteEmployee(employeeId);
          });
        }

        /// <summary>
        /// Adds the employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        public Employee AddEmployee(Employee employee)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.AddEmployee(employee);
            });
        }

        /// <summary>
        /// Edits the employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        public Employee EditEmployee(Employee employee)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.EditEmployee(employee);
            });
        }

        /// <summary>
        /// Gets the address types.
        /// </summary>
        /// <returns></returns>
        public List<AddressType> GetAddressTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IAddressTypeRepository _repository = _dataRepositoryFactory.GetDataRepository<IAddressTypeRepository>();
                return _repository.GetAddressTypes();
            });
        }

        /// <summary>
        /// Gets the office branches.
        /// </summary>
        /// <returns></returns>
        public List<OfficeBranch> GetOfficeBranches()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IOfficeBranchRepository _repository = _dataRepositoryFactory.GetDataRepository<IOfficeBranchRepository>();
                return _repository.GetOfficeBranches();
            });
        }

        /// <summary>
        /// Gets the salutations.
        /// </summary>
        /// <returns></returns>
        public List<Salutation> GetSalutations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _repository = _dataRepositoryFactory.GetDataRepository<ISalutationRepository>();
                return _repository.GetSalutations();
            });
        }

        /// <summary>
        /// Determines whether [is employee identifier already registered] [the specified vb s_ identifier].
        /// </summary>
        /// <param name="VBS_Id">The vb s_ identifier.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public Boolean IsEmployeeIdAlreadyRegistered(string VBS_Id, int employeeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.IsEmployeeIdAlreadyRegistered(VBS_Id, employeeId);
            });
        }

        /// <summary>
        /// Determines whether [is email already registered] [the specified email address].
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public Boolean IsEmailAlreadyRegistered(string emailAddress, int employeeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.IsEmailAlreadyRegistered(emailAddress, employeeId);
            });
        }

        /// <summary>
        /// Determines whether [is mobile number already registered] [the specified mobile number].
        /// </summary>
        /// <param name="mobileNumber">The mobile number.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public Boolean IsMobileNumberAlreadyRegistered(string mobileNumber, int employeeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.IsMobileNumberAlreadyRegistered(mobileNumber, employeeId);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetEmployeeCount()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetEmployeeCount();
            });
        }

        /// <summary>
        /// Retrieves the list of all Active Banks.
        /// </summary>
        /// <returns></returns>
        public List<Bank> GetBanks()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IBankRepository _bankRepository = _dataRepositoryFactory.GetDataRepository<IBankRepository>();
                return _bankRepository.GetBanks();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeId"></param>
        public void ArchiveEmployee(int employeeId, string updatedByUserName)
        {
            ExecuteFaultHandledOperation(() =>
          {
              var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
              _employeeRepository.ArchiveEmployee(employeeId, updatedByUserName);
          });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public List<EmployeeArchive> GetEmployeeHistory(int employeeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetEmployeeHistory(employeeId);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeArchiveId"></param>
        /// <returns></returns>
        public CompareEmployeeModel GetMatchingEmployeeInformation(int employeeArchiveId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetMatchingEmployeeInformation(employeeArchiveId);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="financialYearFrom"></param>
        /// <param name="financialYearTo"></param>
        /// <returns></returns>
        public List<GetEmployeeSalaryDetail_Result> GetYearlyPaySheet(Nullable<int> employeeId, Nullable<int> financialYearFrom, Nullable<int> financialYearTo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetYearlyPaySheet(employeeId, financialYearFrom, financialYearTo);
            });
        }

        #region Designations

        /// <summary>
        /// Gets the designations.
        /// </summary>
        /// <returns></returns>
        public List<Designation> GetDesignations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _repository = _dataRepositoryFactory.GetDataRepository<IDesignationRepository>();
                return _repository.GetDesignations();
            });
        }

        /// <summary>
        /// Gets the designation.
        /// </summary>
        /// <param name="designationId">The designation identifier.</param>
        /// <returns></returns>
        public Designation GetDesignation(int designationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _repository = _dataRepositoryFactory.GetDataRepository<IDesignationRepository>();
                return _repository.GetDesignation(designationId);
            });
        }

        /// <summary>
        /// Adds the designation.
        /// </summary>
        /// <param name="designation">The designation.</param>
        /// <param name="userName">Name of the user.</param>
        public void AddDesignation(Designation designation, string userName)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var _designationRepository = _dataRepositoryFactory.GetDataRepository<IDesignationRepository>();
                designation.CreatedBy = userName;
                designation.CreatedDate = DateTime.Now;
                designation.DisplayOrder = 0;
                designation.IsActive = true; // By default mark the designation as active.
                _designationRepository.Add(designation, userName);
            });
        }

        /// <summary>
        /// Updates the designation.
        /// </summary>
        /// <param name="designation">The designation.</param>
        /// <param name="userName">Name of the user.</param>
        public void UpdateDesignation(Designation designation, string userName)
        {
            ExecuteFaultHandledOperation(() =>
            {
                designation.UpdatedBy = userName;
                designation.UpdatedDate = DateTime.Now;
                IDesignationRepository _designationRepository = _dataRepositoryFactory.GetDataRepository<IDesignationRepository>();
                _designationRepository.Update(designation, userName);
            });
        }

        /// <summary>
        /// Determines whether [is designation exists] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="designationId">The designation identifier.</param>
        /// <returns></returns>
        public Boolean IsDesignationExists(string name, int designationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _repository = _dataRepositoryFactory.GetDataRepository<IDesignationRepository>();
                return _repository.IsDesignationExists(name, designationId);
            });
        }

        #endregion Designations

        #region Address

        /// <summary>
        /// Gets the cities.
        /// </summary>
        /// <returns></returns>
        public List<City> GetCities()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _repository = _dataRepositoryFactory.GetDataRepository<ICityRepository>();
                return _repository.GetCities();
            });
        }

        /// <summary>
        /// Gets the cities.
        /// </summary>
        /// <param name="stateId">The state identifier.</param>
        /// <param name="cityId">The city identifier.</param>
        /// <returns></returns>
        public List<City> GetCities(int stateId, int? cityId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _repository = _dataRepositoryFactory.GetDataRepository<ICityRepository>();
                return _repository.GetCities(stateId, cityId);
            });
        }

        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <returns></returns>
        public List<State> GetStates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _repository = _dataRepositoryFactory.GetDataRepository<IStateRepository>();
                return _repository.GetStates();
            });
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <param name="stateId">The state identifier.</param>
        /// <returns></returns>
        public State GetState(int stateId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _repository = _dataRepositoryFactory.GetDataRepository<IStateRepository>();
                return _repository.GetState(stateId);
            });
        }

        /// <summary>
        /// Gets the zip codes.
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <returns></returns>
        public List<ZipCode> GetZipCodes(int cityId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _repository = _dataRepositoryFactory.GetDataRepository<IZipCodeRepository>();
                return _repository.GetZipCodes(cityId);
            });
        }

        public State GetStateByCityName(string cityName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _repository = _dataRepositoryFactory.GetDataRepository<IStateRepository>();
                return _repository.GetStateByCityName(cityName);
            });
        }

        #endregion Address

        #region Salary Calculation

        public bool SaveEmployeeSalaryDetail(EmpSalaryStructure empSalaryStructure, string userName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                if (empSalaryStructure.CTC.IsNotNull())
                {
                    List<EmployeeSalaryDetail> employeeSalaryDetails = CalculateSalaryComponents(empSalaryStructure, userName);
                    // Push record for input fields from the form.
                    IEmpSalaryStructureRepo _empSalaryStructureRepo = _dataRepositoryFactory.GetDataRepository<IEmpSalaryStructureRepo>();

                    empSalaryStructure.CreatedDate = DateTime.Now;
                    empSalaryStructure.IsActive = true;
                    empSalaryStructure.EmployeeSalaryDetails = employeeSalaryDetails;
                    _empSalaryStructureRepo.Add(empSalaryStructure, userName);
                }

                return true;
            });
        }

        private List<EmployeeSalaryDetail> CalculateSalaryComponents(EmpSalaryStructure empSalaryStructure, string userName)
        {
            List<EmployeeSalaryDetail> employeeSalaryDetails = new List<EmployeeSalaryDetail>();
            Dictionary<int, int> financialYearMonths = GetFinancialYearMonths(empSalaryStructure.EffectiveFrom);

            foreach (var item in financialYearMonths)
            {
                if (empSalaryStructure.EffectiveFrom.Month == item.Key && empSalaryStructure.EffectiveFrom.Day > 1)
                {
                    employeeSalaryDetails.AddRange(CalculateSalaryComponent(empSalaryStructure, item.Key, item.Value)); //TODO: need to calculate on pro data basis.
                }
                else
                {
                    employeeSalaryDetails.AddRange(CalculateSalaryComponent(empSalaryStructure, item.Key, item.Value));
                }
            }

            var result = financialYearMonths.OrderByDescending(order => order.Value).ThenByDescending(then => then.Key).FirstOrDefault();

            if (result.IsNotNull())
            {
                empSalaryStructure.EffectiveTo = new DateTime(result.Value, result.Key, empSalaryStructure.EffectiveFrom.Day);
            }

            return employeeSalaryDetails;
        }

        private List<EmployeeSalaryDetail> CalculateSalaryComponent(EmpSalaryStructure empSalaryStructure, int monthId, int year)
        {
            List<EmployeeSalaryDetail> employeeSalaryDetails = new List<EmployeeSalaryDetail>();
            var salaryComponents = GetSalaryComponents();

            SalaryComponentEnum salaryComponentEnum = Common.Enums.SalaryComponentEnum.Basic;
            Decimal? deductedAmountfromCTC = 0;

            String[] deductedComponentFromCTC = { "Basic", "HRA", "Conveyance", "PerformanceIncentive", "Medical", "FoodCoupons", "ProjectIncentive", "CarLease", "LTC", "PF", "Mediclaim", "Gratuity", "CabDeductions", };
            EmployeeSalaryDetail employeeSalaryDetail = null;

            foreach (SalaryComponent salaryComponent in salaryComponents)
            {
                salaryComponentEnum = (SalaryComponentEnum)Enum.Parse(typeof(SalaryComponentEnum), salaryComponent.Name);

                if (!salaryComponentEnum.Equals(SalaryComponentEnum.SpecialAllowance))
                {
                    employeeSalaryDetail = new EmployeeSalaryDetail();
                    employeeSalaryDetail.EmployeeId = empSalaryStructure.EmployeeId;
                    employeeSalaryDetail.SalaryComponentId = salaryComponent.SalaryComponentId;
                    employeeSalaryDetail.MonthId = monthId;
                    employeeSalaryDetail.Year = year;
                    employeeSalaryDetail.Amount = GetAmountBySalaryComponent(empSalaryStructure, salaryComponent.DefaultAmount, salaryComponentEnum, monthId);
                    employeeSalaryDetail.IsActive = true;
                    employeeSalaryDetail.CreatedBy = empSalaryStructure.CreatedBy;
                    employeeSalaryDetail.CreatedDate = DateTime.Now;

                    if (deductedComponentFromCTC.Contains(salaryComponentEnum.ToStringValue()))
                    {
                        deductedAmountfromCTC += employeeSalaryDetail.Amount.IsNotNull() && employeeSalaryDetail.Amount.HasValue ? employeeSalaryDetail.Amount : 0;
                    }

                    employeeSalaryDetails.Add(employeeSalaryDetail);
                }
            }

            // Update value for SpecialAllowance based on below rules.
            // CTC -(Basic + HRA + Conveyance)- Performance Incentive - 
            // (Medical + Food Coupons + Project Incentive + Car Lease + LTC + PF + Mediclaim + Gratuity) - (Cab Deductions)
            employeeSalaryDetail = new EmployeeSalaryDetail();
            employeeSalaryDetail.EmployeeId = empSalaryStructure.EmployeeId;
            employeeSalaryDetail.SalaryComponentId = Convert.ToInt32(SalaryComponentEnum.SpecialAllowance);
            employeeSalaryDetail.MonthId = monthId;
            employeeSalaryDetail.Year = year;
            employeeSalaryDetail.Amount = (empSalaryStructure.CTC / 12) - deductedAmountfromCTC;
            employeeSalaryDetail.IsActive = true;
            employeeSalaryDetail.CreatedBy = empSalaryStructure.CreatedBy;
            employeeSalaryDetail.CreatedDate = DateTime.Now;
            employeeSalaryDetails.Add(employeeSalaryDetail);

            return employeeSalaryDetails.OrderBy(order => order.MonthId).ThenBy(then => then.SalaryComponentId).ToList();
        }

        private Decimal? GetAmountBySalaryComponent(EmpSalaryStructure empSalaryStructure, Decimal? defaultAmount, SalaryComponentEnum salaryComponentEnum, int monthId)
        {
            Decimal? CTCMonthly = empSalaryStructure.CTC / 12;
            Decimal? basic = 40 * CTCMonthly / 100;

            switch (salaryComponentEnum)
            {
                case SalaryComponentEnum.CTCPerMonth:
                    return CTCMonthly; // Divide yearly CTC in twelve months.
                case SalaryComponentEnum.Basic:
                    return basic;  // 40% of CTC
                case SalaryComponentEnum.HRA:
                    return 50 * basic / 100; // 50 % of Basic
                case SalaryComponentEnum.Conveyance:
                    return defaultAmount;
                case SalaryComponentEnum.SpecialAllowance:
                    // CTC -(Basic + HRA + Conveyance)- Performance Incentive - 
                    // (Medical + Food Coupons + Project Incentive + Car Lease + LTC + PF + Mediclaim + Gratuity) - (Cab Deductions)
                    return 0;
                case SalaryComponentEnum.PerformanceIncentive:
                    return GetPerformanceIncentive(empSalaryStructure); // 5 * CTCMonthly / 100;
                case SalaryComponentEnum.LeaveEncashment:
                    return 0;
                case SalaryComponentEnum.SalaryArrears:
                    return 0;
                case SalaryComponentEnum.CabDeductions:
                    return empSalaryStructure.MonthlyCabDeductions;
                case SalaryComponentEnum.OtherDeduction:
                    return 0;
                case SalaryComponentEnum.Commission:
                    return 0;
                case SalaryComponentEnum.Others:
                    return 0;
                case SalaryComponentEnum.Medical:
                    return defaultAmount;
                case SalaryComponentEnum.FoodCoupons:
                    return empSalaryStructure.MonthlyFoodCoupons;
                case SalaryComponentEnum.ProjectIncentive:
                    return empSalaryStructure.MonthlyProjectIncentive;
                case SalaryComponentEnum.CarLease:
                    return empSalaryStructure.MonthlyCarLease;
                case SalaryComponentEnum.LTC:
                    return 0;
                case SalaryComponentEnum.PF:
                    return 12 * basic / 100;
                case SalaryComponentEnum.Mediclaim:
                    return GetCalculatedMediclaimByMonth(CTCMonthly, monthId);
                case SalaryComponentEnum.Gratuity:
                    return basic * 15 / 26 / 12;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// // 5 % of CTC paid Anually or (25% of increment in June and 25% in December)
        /// </summary>
        /// <param name="empSalaryStructure"></param>
        /// <returns></returns>
        private decimal? GetPerformanceIncentive(EmpSalaryStructure empSalaryStructure)
        {
            // 5 % of CTC paid Anually or (25% of increment in June and 25% in December)
            return 5 * empSalaryStructure.CTC / 12 / 100;
        }

        private decimal? GetCalculatedMediclaimByMonth(decimal? monthlyCTC, int monthId)
        {
            //=IF(H21<41667, 484, IF(AND(H21>41668, H21<83334), 969,1453))
            //    =IF(L21<41667, 476, IF(AND(L21>41668, L21<83334), 952,1428))
            //if (month.Equals(MonthEnum.April)
            //    || month.Equals(MonthEnum.May) || month.Equals(MonthEnum.June)
            //    || month.Equals(MonthEnum.February) || month.Equals(MonthEnum.March))
            if (monthId >= 2 && monthId <= 6)
            {
                //if(CTC < 41667){484}
                //else if (CTC > 41668 AND CTC< 83334){969}
                //else{1453}
                return monthlyCTC < 41667 ? 484 : monthlyCTC > 41668 && monthlyCTC < 83334 ? 969 : 1453;
            }
            else
            {
                //if(CTC < 41667){476}
                //else if (CTC > 41668 AND CTC< 83334){952}
                //else{1428}
                return monthlyCTC < 41667 ? 476 : monthlyCTC > 41668 && monthlyCTC < 83334 ? 952 : 1428;
            }
        }

        public IEnumerable<SalaryComponent> GetSalaryComponents()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _repository = _dataRepositoryFactory.GetDataRepository<ISalaryComponentRepository>();
                return _repository.Get();
            });
        }

        public static Dictionary<int, int> GetFinancialYearMonths(DateTime inputDate)
        {
            Dictionary<int, int> monthsInfo = new Dictionary<int, int>();

            var currentMonthId = inputDate.Month;
            var currentYear = inputDate.Year;

            if (currentMonthId >= 4)
            {
                // Months of Current Year
                for (int i = currentMonthId; i <= 12; i++)
                {
                    monthsInfo.Add(i, currentYear);
                }

                for (int i = 1; i <= 3; i++)
                {
                    monthsInfo.Add(i, currentYear + 1);
                }
            }
            else
            {
                // Months of Current Year
                for (int i = currentMonthId; i <= 3; i++)
                {
                    monthsInfo.Add(i, currentYear);
                }
            }
            return monthsInfo;
        }

        #endregion

        #endregion
    }
}