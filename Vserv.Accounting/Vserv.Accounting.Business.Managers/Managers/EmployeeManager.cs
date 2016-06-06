#region Namespaces

using System;
using System.Collections.Generic;
using Vserv.Accounting.Common;
using Vserv.Accounting.Common.Enums;
using Vserv.Accounting.Data;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

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

        public bool SaveEmployeeSalaryDetail(SalarySummary salarySummary, string userName)
        {
            return ExecuteFaultHandledOperation(() =>
            {

                List<EmployeeSalaryDetail> employeeSalaryDetails = new List<EmployeeSalaryDetail>();
                IEmployeeSalaryDetailRepo _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeSalaryDetailRepo>();

                // Create the object for entire months.
                foreach (SalaryComponentEnum salaryComponentEnum in Enum.GetValues(typeof(SalaryComponentEnum)))
                {
                    var result = CreateEmployeeSalaryDetailObject(salarySummary, userName, salaryComponentEnum);
                    if (result.IsNotNull())
                    {
                        employeeSalaryDetails.AddRange(result);
                    }
                }

                // Go for saving in database.
                foreach (var employeeSalaryDetail in employeeSalaryDetails)
                {
                    _employeeRepository.Add(employeeSalaryDetail, userName);
                }

                return true;
            });
        }

        private List<EmployeeSalaryDetail> CreateEmployeeSalaryDetailObject(SalarySummary salarySummary, string userName, SalaryComponentEnum salaryComponentEnum)
        {
            Decimal? CTCMonthly = salarySummary.CTC / 12;
            Decimal? basic = 40 * CTCMonthly / 100;

            switch (salaryComponentEnum)
            {
                case SalaryComponentEnum.CTCPerMonth:
                    return GetEmployeeSalaryDetailObjectForYear(salarySummary, CTCMonthly, SalaryComponentEnum.CTCPerMonth);
                case SalaryComponentEnum.Basic:
                    //40% of CTC
                    return GetEmployeeSalaryDetailObjectForYear(salarySummary, basic, SalaryComponentEnum.Basic);
                case SalaryComponentEnum.HRA:
                    //50 % of Basic
                    return GetEmployeeSalaryDetailObjectForYear(salarySummary, 50 * basic / 100, SalaryComponentEnum.HRA);
                case SalaryComponentEnum.Conveyance:
                    // Fixed: 1600
                    return GetEmployeeSalaryDetailObjectForYear(salarySummary, 1600, SalaryComponentEnum.Conveyance);
                case SalaryComponentEnum.SpecialAllowance:
                    // CTC -(Basic + HRA + Conveyance)- Performance Incentive - 
                    // (Medical + Food Coupons + Project Incentive + Car Lease + LTC + PF + Mediclaim + Gratuity) - (Cab Deductions)
                    return GetEmployeeSalaryDetailObjectForYear(salarySummary, 0, SalaryComponentEnum.SpecialAllowance);
                case SalaryComponentEnum.PerformanceIncentive:
                    // 5 % of CTC paid Anually or (25% of increment in June and 25% in December)
                    return GetEmployeeSalaryDetailObjectForYear(salarySummary, 5 * CTCMonthly / 100, SalaryComponentEnum.PerformanceIncentive);
                case SalaryComponentEnum.LeaveEncashment:
                    return GetEmployeeSalaryDetailObjectForYear(salarySummary, 0, SalaryComponentEnum.LeaveEncashment);
                case SalaryComponentEnum.SalaryArrears:
                    //Input Field / Calculated Columns and if blank the input field.
                    return GetEmployeeSalaryDetailObjectForYear(salarySummary, 0, SalaryComponentEnum.SalaryArrears);
                case SalaryComponentEnum.CabDeductions:
                    return GetEmployeeSalaryDetailObjectForYear(salarySummary, salarySummary.CabDeductions / 12, SalaryComponentEnum.CabDeductions);
                case SalaryComponentEnum.OtherDeduction:
                    return GetEmployeeSalaryDetailObjectForYear(salarySummary, 0, SalaryComponentEnum.OtherDeduction);
                case SalaryComponentEnum.Commission:
                    return GetEmployeeSalaryDetailObjectForYear(salarySummary, 0, SalaryComponentEnum.Commission);
                case SalaryComponentEnum.Others:
                    return GetEmployeeSalaryDetailObjectForYear(salarySummary, 0, SalaryComponentEnum.Others);
                case SalaryComponentEnum.Medical:
                    // Fixed: 1250
                    return GetEmployeeSalaryDetailObjectForYear(salarySummary, 1250, SalaryComponentEnum.Medical);
                case SalaryComponentEnum.FoodCoupons:
                    return GetEmployeeSalaryDetailObjectForYear(salarySummary, salarySummary.FoodCoupons, SalaryComponentEnum.FoodCoupons);
                case SalaryComponentEnum.ProjectIncentive:
                    return GetEmployeeSalaryDetailObjectForYear(salarySummary, salarySummary.PerformanceIncentive / 12, SalaryComponentEnum.ProjectIncentive);
                case SalaryComponentEnum.CarLease:
                    return GetEmployeeSalaryDetailObjectForYear(salarySummary, salarySummary.CarLease / 12, SalaryComponentEnum.CarLease);
                case SalaryComponentEnum.LTC:
                    return GetEmployeeSalaryDetailObjectForYear(salarySummary, 0, SalaryComponentEnum.LTC);
                case SalaryComponentEnum.PF:
                    //12 % of Basic
                    return GetEmployeeSalaryDetailObjectForYear(salarySummary, 12 * basic / 100, SalaryComponentEnum.PF);
                case SalaryComponentEnum.Mediclaim:
                    //if(CTC < 41667){484}
                    //else if (CTC > 41668 AND CTC< 83334){969}
                    //else{1453}
                    return GetEmployeeSalaryDetailObjectForYear(salarySummary, 0, SalaryComponentEnum.Mediclaim);
                case SalaryComponentEnum.Gratuity:
                    //Basic*15/26/12
                    return GetEmployeeSalaryDetailObjectForYear(salarySummary, basic * 15 / 26 / 12, SalaryComponentEnum.Gratuity);
            }

            return null;
        }

        private List<EmployeeSalaryDetail> GetEmployeeSalaryDetailObjectForYear(SalarySummary salarySummary, decimal? amount, SalaryComponentEnum salaryComponentEnum)
        {
            List<EmployeeSalaryDetail> employeeSalaryDetails = new List<EmployeeSalaryDetail>();
            EmployeeSalaryDetail employeeSalaryDetail = null;
            decimal? monthlyCTC = salarySummary.CTC / 12;

            foreach (MonthEnum month in Enum.GetValues(typeof(MonthEnum)))
            {
                if (salaryComponentEnum.Equals(SalaryComponentEnum.Mediclaim))
                {
                    employeeSalaryDetail = new EmployeeSalaryDetail
                     {
                         EmployeeId = salarySummary.EmployeeId,
                         SalaryComponentId = Convert.ToInt32(salaryComponentEnum),
                         MonthId = Convert.ToInt32(month),
                         Year = salarySummary.Year,
                         IsActive = true,
                         CreatedBy = salarySummary.UserName,
                         CreatedDate = DateTime.Now,
                     };

                    //=IF(H21<41667, 484, IF(AND(H21>41668, H21<83334), 969,1453))
                    //    =IF(L21<41667, 476, IF(AND(L21>41668, L21<83334), 952,1428))
                    if (month.Equals(MonthEnum.April)
                        || month.Equals(MonthEnum.May) || month.Equals(MonthEnum.June)
                        || month.Equals(MonthEnum.February) || month.Equals(MonthEnum.March))
                    {
                        //if(CTC < 41667){484}
                        //else if (CTC > 41668 AND CTC< 83334){969}
                        //else{1453}
                        employeeSalaryDetail.Amount = monthlyCTC < 41667 ? 484 : monthlyCTC > 41668 && monthlyCTC < 83334 ? 969 : 1453;
                    }
                    else
                    {
                        //if(CTC < 41667){476}
                        //else if (CTC > 41668 AND CTC< 83334){952}
                        //else{1428}
                        employeeSalaryDetail.Amount = monthlyCTC < 41667 ? 484 : monthlyCTC > 41668 && monthlyCTC < 83334 ? 969 : 1453;
                    }
                }
                else
                {
                    employeeSalaryDetail = new EmployeeSalaryDetail
                    {
                        EmployeeId = salarySummary.EmployeeId,
                        SalaryComponentId = Convert.ToInt32(salaryComponentEnum),
                        MonthId = Convert.ToInt32(month),
                        Year = salarySummary.Year,
                        Amount = amount,
                        IsActive = true,
                        CreatedBy = salarySummary.UserName,
                        CreatedDate = DateTime.Now,
                    };
                }

                employeeSalaryDetails.Add(employeeSalaryDetail);
            }

            return employeeSalaryDetails;
        }

        #endregion

        #endregion
    }
}
