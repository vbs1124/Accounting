﻿#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Vserv.Accounting.Common;
using Vserv.Accounting.Common.Enums;
using Vserv.Accounting.Data;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Data.Entity.Models;
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
            DataRepositoryFactory = dataRepositoryFactory;
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
                var employeeRepository = DataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return employeeRepository.GetEmployees(employeeFilter);
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
                var employeeRepository = DataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return employeeRepository.GetEmployee(employeeId);
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
              var employeeRepository = DataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
              employeeRepository.DeleteEmployee(employeeId);
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
                var employeeRepository = DataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return employeeRepository.AddEmployee(employee);
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
                var employeeRepository = DataRepositoryFactory.GetDataRepository<IEmployeeRepository>();

                if (!employee.IsActive && employee.RelievingDate.IsNotNull() && employee.RelievingDate.HasValue)
                {
                    //Trigger Full and Final Process for relieved Employee.
                    PerformEmployeeFNF(employee);
                }

                return employeeRepository.EditEmployee(employee);
            });
        }

        private void PerformEmployeeFNF(Employee employee)
        {
            ExecuteFaultHandledOperation(() =>
            {
                IEmpSalaryStructureRepo empSalaryStructureRepo = DataRepositoryFactory.GetDataRepository<IEmpSalaryStructureRepo>();
                IEmployeeSalaryDetailRepo employeeSalaryDetailRepo = DataRepositoryFactory.GetDataRepository<IEmployeeSalaryDetailRepo>();
                EmpSalaryStructure dbEmpSalaryStructure = empSalaryStructureRepo.GetCurrentEmpSalaryStructure(employee.EmployeeId);

                // Archive existing Breakup.
                employeeSalaryDetailRepo.ArchiveEmpSalaryDetail(dbEmpSalaryStructure.EmpSalaryStructureId, employee.UpdatedBy);

                foreach (var empSalaryDetail in dbEmpSalaryStructure.EmpSalaryDetails)
                {
                    if (employee.RelievingDate != null && (empSalaryDetail.Year != null && new DateTime(empSalaryDetail.Year.Value, empSalaryDetail.MonthId, 1) > employee.RelievingDate.Value))
                    {
                        empSalaryDetail.Amount = null; //Reset Amount for all the future Months.
                        employeeSalaryDetailRepo.Update(empSalaryDetail, employee.UpdatedBy);
                    }
                    else if (employee.RelievingDate != null && (empSalaryDetail.MonthId.Equals(employee.RelievingDate.Value.Month) && empSalaryDetail.Year.Equals(employee.RelievingDate.Value.Year)))
                    {
                        // Go For Pro Data based Calculations.
                        empSalaryDetail.Amount = empSalaryDetail.Amount.IsNotNull() && empSalaryDetail.Amount.HasValue ? Math.Round(empSalaryDetail.Amount.Value * employee.RelievingDate.Value.Day / 30, 2) : 0M;
                        employeeSalaryDetailRepo.Update(empSalaryDetail, employee.UpdatedBy);
                    }
                }

                if (employee.RelievingDate != null)
                    dbEmpSalaryStructure.EffectiveTo = employee.RelievingDate.Value; //Update the EffectiveTo to the RelievingDate of the employee
                empSalaryStructureRepo.Update(dbEmpSalaryStructure, employee.UpdatedBy);
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
                IAddressTypeRepository repository = DataRepositoryFactory.GetDataRepository<IAddressTypeRepository>();
                return repository.GetAddressTypes();
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
                IOfficeBranchRepository repository = DataRepositoryFactory.GetDataRepository<IOfficeBranchRepository>();
                return repository.GetOfficeBranches();
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
                var repository = DataRepositoryFactory.GetDataRepository<ISalutationRepository>();
                return repository.GetSalutations();
            });
        }

        /// <summary>
        /// Determines whether [is employee identifier already registered] [the specified vb s_ identifier].
        /// </summary>
        /// <param name="vbsId">The vb s_ identifier.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public Boolean IsEmployeeIdAlreadyRegistered(string vbsId, int employeeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var employeeRepository = DataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return employeeRepository.IsEmployeeIdAlreadyRegistered(vbsId, employeeId);
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
                var employeeRepository = DataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return employeeRepository.IsEmailAlreadyRegistered(emailAddress, employeeId);
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
                var employeeRepository = DataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return employeeRepository.IsMobileNumberAlreadyRegistered(mobileNumber, employeeId);
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
                var employeeRepository = DataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return employeeRepository.GetEmployeeCount();
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
                IBankRepository bankRepository = DataRepositoryFactory.GetDataRepository<IBankRepository>();
                return bankRepository.GetBanks();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="updatedByUserName"></param>
        public void ArchiveEmployee(int employeeId, string updatedByUserName)
        {
            ExecuteFaultHandledOperation(() =>
          {
              var employeeRepository = DataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
              employeeRepository.ArchiveEmployee(employeeId, updatedByUserName);
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
                var employeeRepository = DataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return employeeRepository.GetEmployeeHistory(employeeId);
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
                var employeeRepository = DataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return employeeRepository.GetMatchingEmployeeInformation(employeeArchiveId);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="financialYearFrom"></param>
        /// <param name="financialYearTo"></param>
        /// <returns></returns>
        public List<GetEmployeeSalaryDetail_Result> GetYearlyPaySheet(int? employeeId, int? financialYearFrom, int? financialYearTo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var employeeRepository = DataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return employeeRepository.GetYearlyPaySheet(employeeId, financialYearFrom, financialYearTo);
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
                var repository = DataRepositoryFactory.GetDataRepository<IDesignationRepository>();
                return repository.GetDesignations();
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
                var repository = DataRepositoryFactory.GetDataRepository<IDesignationRepository>();
                return repository.GetDesignation(designationId);
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
                var designationRepository = DataRepositoryFactory.GetDataRepository<IDesignationRepository>();
                designation.CreatedBy = userName;
                designation.CreatedDate = DateTime.Now;
                designation.DisplayOrder = 0;
                designation.IsActive = true; // By default mark the designation as active.
                designationRepository.Add(designation, userName);
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
                IDesignationRepository designationRepository = DataRepositoryFactory.GetDataRepository<IDesignationRepository>();
                designationRepository.Update(designation, userName);
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
                var repository = DataRepositoryFactory.GetDataRepository<IDesignationRepository>();
                return repository.IsDesignationExists(name, designationId);
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
                var repository = DataRepositoryFactory.GetDataRepository<ICityRepository>();
                return repository.GetCities();
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
                var repository = DataRepositoryFactory.GetDataRepository<ICityRepository>();
                return repository.GetCities(stateId, cityId);
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
                var repository = DataRepositoryFactory.GetDataRepository<IStateRepository>();
                return repository.GetStates();
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
                var repository = DataRepositoryFactory.GetDataRepository<IStateRepository>();
                return repository.GetState(stateId);
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
                var repository = DataRepositoryFactory.GetDataRepository<IZipCodeRepository>();
                return repository.GetZipCodes(cityId);
            });
        }

        /// <summary>
        /// Gets the name of the state by city.
        /// </summary>
        /// <param name="cityName">Name of the city.</param>
        /// <returns></returns>
        public State GetStateByCityName(string cityName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var repository = DataRepositoryFactory.GetDataRepository<IStateRepository>();
                return repository.GetStateByCityName(cityName);
            });
        }

        #endregion Address

        #region Salary Calculation

        /// <summary>
        /// Saves the employee salary detail.
        /// </summary>
        /// <param name="empSalaryStructure">The emp salary structure.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public MessageResult SaveEmployeeSalaryDetail(EmpSalaryStructure empSalaryStructure, string userName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                if (empSalaryStructure.CTC.IsNotNull())
                {
                    var options = new TransactionOptions
                    {
                        IsolationLevel = IsolationLevel.ReadCommitted,
                        Timeout = TransactionManager.DefaultTimeout,
                    };

                    using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, options))
                    {
                        List<EmpSalaryDetail> empSalaryDetails = new List<EmpSalaryDetail>();

                        IEmpSalaryStructureRepo empSalaryStructureRepo = DataRepositoryFactory.GetDataRepository<IEmpSalaryStructureRepo>();
                        IEmployeeSalaryDetailRepo employeeSalaryDetailRepo = DataRepositoryFactory.GetDataRepository<IEmployeeSalaryDetailRepo>();

                        // Get the new month collection based on effective from date.
                        IEnumerable<FinancialPeriod> financialMonthsOnEffectiveFrom = empSalaryStructure.EffectiveFrom.GetFinancialYearMonths().Select(months => new FinancialPeriod { Month = months.Key, Year = months.Value }).ToList();
                        List<FinancialPeriod> matchingFinancialMonths = null;

                        // Set Values for EmpSalaryStructure entity.
                        var periods = financialMonthsOnEffectiveFrom.OrderByDescending(order => order.Year).ThenByDescending(then => then.Month).FirstOrDefault();

                        if (periods.IsNotNull())
                        {
                            empSalaryStructure.EffectiveTo = new DateTime(periods.Year, periods.Month, empSalaryStructure.EffectiveFrom.Day);
                        }

                        empSalaryStructure.CreatedDate = DateTime.Now;
                        empSalaryStructure.CreatedBy = userName;
                        empSalaryStructure.IsActive = true;
                        // End

                        // Find if the month exists in the existing Salary Structure.
                        EmpSalaryStructure previousEmpSalaryStructure = empSalaryStructureRepo.GetCurrentEmpSalaryStructure(empSalaryStructure.EmployeeId);

                        if (previousEmpSalaryStructure.IsNotNull())
                        {
                            IEnumerable<FinancialPeriod> financialMonthsOnExistingEffectiveFrom = previousEmpSalaryStructure.EffectiveFrom.GetFinancialYearMonths().Select(months => new FinancialPeriod { Month = months.Key, Year = months.Value }).ToList();

                            matchingFinancialMonths = financialMonthsOnExistingEffectiveFrom.Intersect(financialMonthsOnEffectiveFrom).ToList();
                        }


                        //if matching month(s) found the the existing records will be updated with the new structure 
                        // and those will be mapped to the new Employee Structure. Need to archive the existing Records too.
                        if (matchingFinancialMonths.IsNotNull() && matchingFinancialMonths.Any())
                        {
                            empSalaryStructure.ParentId = previousEmpSalaryStructure.EmpSalaryStructureId;
                            empSalaryStructure.SalaryStructureTypeId = Convert.ToInt32(SalaryStructureTypeEnum.Appraisal);

                            // Archive the existing records from EmpSalaryDetail (move to EmpSalaryDetailArchive table.)
                            // Add a new entry in EmpSalaryStructure table.
                            // Update amount for all the salary components which are from the matching months.
                            // Make sure that the existing and updated entries are mapped to the new entry pushed to EmpSalaryStructure table.
                            // Build the salary structure for the current month based on pro data basis.
                            // Build Salary Structure for rest of the month.

                            // Update Amount and EmpSalaryStructureId for Matching Records.
                            // Get all the existing Salary Details Which has to be updated.
                            // Archive records which are there against existing Structure.
                            employeeSalaryDetailRepo.ArchiveEmpSalaryDetail(previousEmpSalaryStructure.EmpSalaryStructureId, userName);

                            List<EmpSalaryDetail> empSalaryDetailsToUpdate = (from esd in previousEmpSalaryStructure.EmpSalaryDetails
                                                                              let year = esd.Year
                                                                              where year != null
                                                                              join fp in matchingFinancialMonths on new { Month = esd.MonthId, Year = year.Value } equals new { fp.Month, fp.Year }
                                                                              select esd).ToList();

                            List<EmpSalaryDetail> empSalaryDetailByMonth = GetMonthlySalaryComponents(empSalaryStructure, previousEmpSalaryStructure, matchingFinancialMonths.FirstOrDefault());

                            if (empSalaryDetailByMonth.IsNotNull())
                            {
                                foreach (var period in matchingFinancialMonths)
                                {
                                    // Appraisal Month: Build Salary Component's information on Pro data basis.
                                    if (period.Month.Equals(empSalaryStructure.EffectiveFrom.Month) && period.Year.Equals(empSalaryStructure.EffectiveFrom.Year) && empSalaryStructure.EffectiveFrom.Day > 1)
                                    {
                                        empSalaryDetails.AddRange(GetAppraisalMonthSalaryComponents(empSalaryStructure, previousEmpSalaryStructure));
                                    }
                                    else
                                    {
                                        foreach (var item in empSalaryDetailByMonth)
                                        {
                                            item.MonthId = period.Month;
                                            item.Year = period.Year;
                                        }

                                        empSalaryDetails.AddRange(empSalaryDetailByMonth.Clone());
                                    }
                                }
                            }

                            foreach (var empSalaryDetail in empSalaryDetailsToUpdate)
                            {
                                EmpSalaryDetail updatedAmountRecord = empSalaryDetails.FirstOrDefault(condition => condition.EmployeeId == empSalaryDetail.EmployeeId && condition.MonthId == empSalaryDetail.MonthId && condition.Year == empSalaryDetail.Year && condition.SalaryComponentId == empSalaryDetail.SalaryComponentId);

                                if (updatedAmountRecord.IsNotNull())
                                {
                                    empSalaryDetail.Amount = updatedAmountRecord.Amount;
                                }
                            }

                            List<EmpSalaryDetail> calculatedEmpSalaryDetails = new List<EmpSalaryDetail>();
                            calculatedEmpSalaryDetails.AddRange(empSalaryDetailsToUpdate);
                            calculatedEmpSalaryDetails.AddRange(previousEmpSalaryStructure.EmpSalaryDetails.Except(empSalaryDetailsToUpdate));

                            var withNegativeSpeicalAllowance = empSalaryDetailsToUpdate.FirstOrDefault(condition => condition.SalaryComponentId == Convert.ToInt32(SalaryComponentEnum.SpecialAllowance) && condition.Amount < 0);
                            if (withNegativeSpeicalAllowance.IsNotNull())
                            {
                                return new MessageResult { IsErrorOccurred = true, Message = CommonConstants.INVALID_SPECIAL_ALLOWANCE };
                            }

                            empSalaryStructure = empSalaryStructureRepo.Add(empSalaryStructure, userName);

                            foreach (var item in calculatedEmpSalaryDetails)
                            {
                                item.EmpSalaryStructureId = empSalaryStructure.EmpSalaryStructureId;
                                employeeSalaryDetailRepo.AddOrUpdate(item, userName);
                            }
                        }
                        else
                        {
                            // Else Insert new records for all the months falling after the effective from Date.
                            // Insert new record in EmpSalaryStructure table.
                            // Insert new records in  EmpSalaryDetail table against the newly created EmpSalaryStructure.
                            if (financialMonthsOnEffectiveFrom.IsNotNull())
                            {
                                List<EmpSalaryDetail> empSalaryDetailByMonth = GetMonthlySalaryComponents(empSalaryStructure, previousEmpSalaryStructure, financialMonthsOnEffectiveFrom.FirstOrDefault());

                                if (empSalaryDetailByMonth.IsNotNull())
                                {
                                    foreach (var period in financialMonthsOnEffectiveFrom)
                                    {
                                        // Appraisal Month: Build Salary Component's information on Pro data basis.
                                        if (period.Month.Equals(empSalaryStructure.EffectiveFrom.Month) && period.Year.Equals(empSalaryStructure.EffectiveFrom.Year) && empSalaryStructure.EffectiveFrom.Day > 1)
                                        {
                                            empSalaryDetails.AddRange(GetAppraisalMonthSalaryComponents(empSalaryStructure, previousEmpSalaryStructure));
                                        }
                                        else
                                        {
                                            foreach (var item in empSalaryDetailByMonth)
                                            {
                                                item.MonthId = period.Month;
                                                item.Year = period.Year;
                                            }

                                            empSalaryDetails.AddRange(empSalaryDetailByMonth.Clone());
                                        }
                                    }
                                }

                                if (previousEmpSalaryStructure.IsNotNull())
                                {
                                    empSalaryStructure.ParentId = previousEmpSalaryStructure.EmpSalaryStructureId;
                                    empSalaryStructure.SalaryStructureTypeId = Convert.ToInt32(SalaryStructureTypeEnum.Appraisal);
                                }
                                else
                                {
                                    empSalaryStructure.SalaryStructureTypeId = Convert.ToInt32(SalaryStructureTypeEnum.Initial);
                                }

                                var withNegativeSpeicalAllowance = empSalaryDetails.FirstOrDefault(condition => condition.SalaryComponentId == Convert.ToInt32(SalaryComponentEnum.SpecialAllowance) && condition.Amount < 0);
                                if (withNegativeSpeicalAllowance.IsNotNull())
                                {
                                    return new MessageResult { IsErrorOccurred = true, Message = CommonConstants.INVALID_SPECIAL_ALLOWANCE };
                                }

                                empSalaryStructure.EmpSalaryDetails = empSalaryDetails;
                                empSalaryStructureRepo.Add(empSalaryStructure, userName);
                            }
                        }

                        transactionScope.Complete();
                    }
                }

                return new MessageResult { IsErrorOccurred = false, Message = CommonConstants.SALARY_BREAKUP_SUCCESS_MESSAGE };
            });
        }

        /// <summary>
        /// Gets the salary components.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SalaryComponent> GetSalaryComponents()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var repository = DataRepositoryFactory.GetDataRepository<ISalaryComponentRepository>();
                return repository.Get();
            });
        }

        /// <summary>
        /// Gets the employee appraisal history.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public List<GetEmpAppraisalHistory_Result> GetEmployeeAppraisalHistory(int employeeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IEmpSalaryStructureRepo empSalaryStructureRepo = DataRepositoryFactory.GetDataRepository<IEmpSalaryStructureRepo>();
                return empSalaryStructureRepo.GetEmployeeAppraisalHistory(employeeId);
            });
        }

        /// <summary>
        /// Updates the yearly pay sheet.
        /// </summary>
        /// <param name="paySheet">The pay sheet.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public List<EmpSalaryDetail> UpdateYearlyPaySheet(List<EmpSalaryDetail> paySheet, string userName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IEmployeeSalaryDetailRepo employeeSalaryDetailRepo = DataRepositoryFactory.GetDataRepository<IEmployeeSalaryDetailRepo>();
                IEmpSalaryStructureRepo empSalaryStructureRepo = DataRepositoryFactory.GetDataRepository<IEmpSalaryStructureRepo>();

                if (paySheet.IsNotNull() && paySheet.Any())
                {
                    EmpSalaryStructure dbEmpSalaryStructure = empSalaryStructureRepo.Get(paySheet.FirstOrDefault().EmpSalaryStructureId);

                    // Archive existing Breakup.
                    employeeSalaryDetailRepo.ArchiveEmpSalaryDetail(dbEmpSalaryStructure.EmpSalaryStructureId, userName);

                    foreach (var item in paySheet)
                    {
                        var existingEmployeeSalaryDetail = employeeSalaryDetailRepo.Get(item.EmpSalaryDetailId);
                        if (existingEmployeeSalaryDetail.IsNotNull())
                        {
                            existingEmployeeSalaryDetail.Amount = item.Amount;
                            existingEmployeeSalaryDetail.UpdatedBy = userName;
                            existingEmployeeSalaryDetail.UpdatedDate = DateTime.Now;
                            employeeSalaryDetailRepo.Update(existingEmployeeSalaryDetail, userName);
                        }
                    }
                }

                return paySheet;
            });
        }

        /// <summary>
        /// Fetch the change history against any Salary Structure.
        /// </summary>
        /// <param name="empSalaryStructureId"></param>
        /// <returns></returns>
        public List<SalaryStructureChangeHistory_Result> GetSalaryStructureChangeHistory(int empSalaryStructureId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IEmployeeSalaryDetailRepo employeeSalaryDetailRepo = DataRepositoryFactory.GetDataRepository<IEmployeeSalaryDetailRepo>();
                return employeeSalaryDetailRepo.GetSalaryStructureChangeHistory(empSalaryStructureId);
            });
        }

        public List<EmpSalaryCompareResult> GetEmpSalaryStructureComparisonList(int employeeId, int financialYearFrom, Guid uniqueChangeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IEmpSalaryStructureRepo repository = DataRepositoryFactory.GetDataRepository<IEmpSalaryStructureRepo>();
                return repository.GetEmpSalaryStructureComparisonList(employeeId, financialYearFrom, uniqueChangeId);
            });
        }

        public EmployeePaySlip GetEmployeePaySlip(int employeeId, int monthId, int year)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IEmployeePaySlipRepo repo = DataRepositoryFactory.GetDataRepository<IEmployeePaySlipRepo>();
                return repo.GetEmployeePaySlip(employeeId, monthId, year);
            });
        }

        #endregion

        #region  Employee Investments

        public EmpInvestmentDeclarationModel GetInvestmentCatogories(int financialYear, int employeeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IInvestmentCategoryRepo repository = DataRepositoryFactory.GetDataRepository<IInvestmentCategoryRepo>();
                var categorylist = repository.GetInvestmentCatogories(financialYear);
                var empInvestmentlist = repository.GetEmpInvestmentByEmpId(employeeId, financialYear);
                return FillUpEmployeeInvestmentDeclarationDetail(empInvestmentlist, categorylist, employeeId);
            });
        }

        public bool SaveEmployeeInvestments(int employeeId, int finYear, EmpInvestmentDeclarationModel investmentCatogories)
        {

            return ExecuteFaultHandledOperation(() =>
            {
                List<EmpInvestment> empInvestmentList = (from row in investmentCatogories.InvestmentCategories
                                                         from subcat in row.InvestmentSubCategories
                                                         select new EmpInvestment
                                                         {
                                                             EmployeeId = employeeId,
                                                             CategoryId = row.InvestmentCategoryId,
                                                             IsApproved = Convert.ToBoolean(subcat.IsApproved),
                                                             IsActive = true,
                                                             FinancialYear = finYear,
                                                             EmpInvestmentId = subcat.EmpInvestmentId == 0 ? 0 : subcat.EmpInvestmentId,
                                                             SubCategoryId = subcat.InvestmentSubCategoryId,
                                                             DeclaredAmount = Convert.ToDecimal(subcat.DefaultAmount),
                                                             ApprovedAmount = Convert.ToDecimal(subcat.ApprovedAmount) == 0 ? Convert.ToDecimal(subcat.DefaultAmount) : Convert.ToDecimal(subcat.ApprovedAmount),
                                                             CreatedBy = string.IsNullOrEmpty(row.CreatedBy) ? "vbsadmin" : row.CreatedBy,
                                                             CreatedDate = DateTime.Now
                                                         }).ToList();
                IInvestmentCategoryRepo repository = DataRepositoryFactory.GetDataRepository<IInvestmentCategoryRepo>();
                return repository.SaveEmployeeInvestments(empInvestmentList);
            });
        }

        #endregion

        #region Income Tax Computation

        /// <summary>
        /// Get all the information related to Employee's Income Tax Computation.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="financialYear"></param>
        /// <returns></returns>
        public EmployeeTaxation GetEmployeeIncomeTaxComputation(int employeeId, int financialYear)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IEmployeeRepository repo = DataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return repo.GetEmployeeIncomeTaxComputation(employeeId, financialYear);
            });
        }

        #endregion

        #endregion

        #region Private Methods

        private List<EmpSalaryDetail> GetMonthlySalaryComponents(EmpSalaryStructure empSalaryStructure, EmpSalaryStructure previousEmpSalaryStructure, FinancialPeriod financialPeriod)
        {
            List<EmpSalaryDetail> employeeSalaryDetails = new List<EmpSalaryDetail>();
            var salaryComponents = GetSalaryComponents();
            Decimal? deductedAmountfromCTC = 0;

            // Salary Components that has to be excluded from Monthly CTC to calculate Special Allowance.
            //{ "Basic", "HRA", "Conveyance", "PerformanceIncentive", "Medical", "FoodCoupons", "ProjectIncentive", "CarLease", "LTC", "PF", "Mediclaim", "Gratuity", "CabDeductions", };
            String[] deductedComponentFromCTC = { "SCBASC", "SCSHRA", "SCCONV", "SCPERF", "SCMEDC", "SCFCPN", "SCPROJ", "SCCARL", "SCTLTC", "SCEPFO", "SCMEDM", "SCGRAT", "SCCABD" };

            EmpSalaryDetail employeeSalaryDetail;

            foreach (SalaryComponent salaryComponent in salaryComponents)
            {
                var salaryComponentEnum = (SalaryComponentEnum)Enum.Parse(typeof(SalaryComponentEnum), salaryComponent.Name);

                Decimal? amount = null;
                Decimal? ctcMonthly = empSalaryStructure.CTC / 12;
                Decimal? basic = 40 * ctcMonthly / 100;

                switch (salaryComponentEnum)
                {
                    case SalaryComponentEnum.CTCPerMonth:
                        amount = ctcMonthly; // Divide yearly CTC in twelve months.
                        break;
                    case SalaryComponentEnum.Basic:
                        amount = basic;  // 40% of CTC
                        break;
                    case SalaryComponentEnum.HRA:
                        amount = 50 * basic / 100; // 50 % of Basic
                        break;
                    case SalaryComponentEnum.Conveyance:
                        amount = salaryComponent.DefaultAmount;
                        break;
                    case SalaryComponentEnum.SpecialAllowance:
                        continue;
                    case SalaryComponentEnum.PerformanceIncentive:
                        amount = GetMonthlyPerformanceIncentive(empSalaryStructure, previousEmpSalaryStructure); // 5 * CTCMonthly / 100;
                        break;
                    case SalaryComponentEnum.CabDeductions:
                        amount = empSalaryStructure.MonthlyCabDeductions;
                        break;
                    case SalaryComponentEnum.Medical:
                        amount = salaryComponent.DefaultAmount;
                        break;
                    case SalaryComponentEnum.FoodCoupons:
                        amount = empSalaryStructure.MonthlyFoodCoupons;
                        break;
                    case SalaryComponentEnum.ProjectIncentive:
                        amount = empSalaryStructure.MonthlyProjectIncentive;
                        break;
                    case SalaryComponentEnum.CarLease:
                        amount = empSalaryStructure.MonthlyCarLease;
                        break;
                    case SalaryComponentEnum.PF:
                        amount = 12 * basic / 100;
                        break;
                    case SalaryComponentEnum.Mediclaim:
                        amount = GetMonthlyMediclaim(empSalaryStructure);
                        break;
                    case SalaryComponentEnum.Gratuity:
                        amount = (((basic * 15) / 26) / 12);
                        break;
                    case SalaryComponentEnum.LeaveEncashment:
                    case SalaryComponentEnum.SalaryArrears:
                    case SalaryComponentEnum.OtherDeduction:
                    case SalaryComponentEnum.Commission:
                    case SalaryComponentEnum.Others:
                    case SalaryComponentEnum.LTC:
                        amount = 0;
                        break;
                }

                if (amount.IsNotNull() && amount.HasValue)
                {
                    amount = Math.Round(amount.Value, 2);
                }

                employeeSalaryDetail = new EmpSalaryDetail
                {
                    EmployeeId = empSalaryStructure.EmployeeId,
                    SalaryComponentId = salaryComponent.SalaryComponentId,
                    MonthId = financialPeriod.Month,
                    Year = financialPeriod.Year,
                    Amount = amount,
                    IsActive = true,
                    CreatedBy = empSalaryStructure.CreatedBy,
                    CreatedDate = DateTime.Now
                };

                if (deductedComponentFromCTC.Contains(salaryComponent.Code))
                {
                    deductedAmountfromCTC += employeeSalaryDetail.Amount.IsNotNull() && employeeSalaryDetail.Amount.HasValue ? employeeSalaryDetail.Amount : 0;
                }

                employeeSalaryDetails.Add(employeeSalaryDetail);
            }

            // Update value for SpecialAllowance based on below rules.
            // CTC -(Basic + HRA + Conveyance)- Performance Incentive - 
            // (Medical + Food Coupons + Project Incentive + Car Lease + LTC + PF + Mediclaim + Gratuity) - (Cab Deductions)
            employeeSalaryDetail = new EmpSalaryDetail
            {
                EmployeeId = empSalaryStructure.EmployeeId,
                SalaryComponentId = Convert.ToInt32(SalaryComponentEnum.SpecialAllowance),
                MonthId = financialPeriod.Month,
                Year = financialPeriod.Year,
                Amount = Math.Round((empSalaryStructure.CTC / 12), 2) - deductedAmountfromCTC,
                IsActive = true,
                CreatedBy = empSalaryStructure.CreatedBy,
                CreatedDate = DateTime.Now
            };
            employeeSalaryDetails.Add(employeeSalaryDetail);

            return employeeSalaryDetails;
        }

        /// <summary>
        /// Calculates the salary component for appraisal month.
        /// </summary>
        /// <param name="empSalaryStructure">The emp salary structure.</param>
        /// <returns></returns>
        private List<EmpSalaryDetail> GetAppraisalMonthSalaryComponents(EmpSalaryStructure empSalaryStructure, EmpSalaryStructure previousEmpSalaryStructure)
        {
            List<EmpSalaryDetail> employeeSalaryDetails = new List<EmpSalaryDetail>();
            var salaryComponents = GetSalaryComponents();
            Decimal? deductedAmountfromCTC = 0;

            // Salary Components that has to be excluded from Monthly CTC to calculate Special Allowance.
            String[] deductedComponentFromCTC = { "SCBASC", "SCSHRA", "SCCONV", "SCPERF", "SCMEDC", "SCFCPN", "SCPROJ", "SCCARL", "SCTLTC", "SCEPFO", "SCMEDM", "SCGRAT", "SCCABD" };
            EmpSalaryDetail employeeSalaryDetail;

            foreach (SalaryComponent salaryComponent in salaryComponents)
            {
                var salaryComponentEnum = (SalaryComponentEnum)Enum.Parse(typeof(SalaryComponentEnum), salaryComponent.Name);

                if (!salaryComponentEnum.Equals(SalaryComponentEnum.SpecialAllowance))
                {
                    int numberOfDaysFromCurrent = 30 - empSalaryStructure.EffectiveFrom.Day;
                    int numberOfDaysFromPast = empSalaryStructure.EffectiveFrom.Day;
                    Decimal? amount = GetByNumberOfDays(empSalaryStructure, previousEmpSalaryStructure, salaryComponent.DefaultAmount, salaryComponentEnum, numberOfDaysFromCurrent);

                    if (numberOfDaysFromPast > 0 && previousEmpSalaryStructure.IsNotNull())
                    {
                        amount += GetByNumberOfDays(previousEmpSalaryStructure, previousEmpSalaryStructure, salaryComponent.DefaultAmount, salaryComponentEnum, numberOfDaysFromPast);
                    }

                    if (amount.IsNotNull() && amount.HasValue)
                    {
                        amount = Math.Round(amount.Value, 2);
                    }

                    employeeSalaryDetail = new EmpSalaryDetail
                    {
                        EmployeeId = empSalaryStructure.EmployeeId,
                        SalaryComponentId = salaryComponent.SalaryComponentId,
                        MonthId = empSalaryStructure.EffectiveFrom.Month,
                        Year = empSalaryStructure.EffectiveFrom.Year,
                        Amount = amount,
                        IsActive = true,
                        CreatedBy = empSalaryStructure.CreatedBy,
                        CreatedDate = DateTime.Now
                    };

                    if (deductedComponentFromCTC.Contains(salaryComponent.Code))
                    {
                        deductedAmountfromCTC += employeeSalaryDetail.Amount.IsNotNull() && employeeSalaryDetail.Amount.HasValue ? employeeSalaryDetail.Amount : 0;
                    }

                    employeeSalaryDetails.Add(employeeSalaryDetail);
                }
            }

            // Update value for SpecialAllowance based on below rules.
            // CTC -(Basic + HRA + Conveyance)- Performance Incentive - 
            // (Medical + Food Coupons + Project Incentive + Car Lease + LTC + PF + Mediclaim + Gratuity) - (Cab Deductions)
            employeeSalaryDetail = new EmpSalaryDetail
            {
                EmployeeId = empSalaryStructure.EmployeeId,
                SalaryComponentId = Convert.ToInt32(SalaryComponentEnum.SpecialAllowance),
                MonthId = empSalaryStructure.EffectiveFrom.Month,
                Year = empSalaryStructure.EffectiveFrom.Year,
                Amount = Math.Round((empSalaryStructure.CTC / 12), 2) - deductedAmountfromCTC,
                IsActive = true,
                CreatedBy = empSalaryStructure.CreatedBy,
                CreatedDate = DateTime.Now
            };
            employeeSalaryDetails.Add(employeeSalaryDetail);

            return employeeSalaryDetails.OrderBy(order => order.MonthId).ThenBy(then => then.SalaryComponentId).ToList();
        }

        /// <summary>
        /// Gets the by number of days.
        /// </summary>
        /// <param name="empSalaryStructure">The emp salary structure.</param>
        /// <param name="defaultAmount">The default amount.</param>
        /// <param name="salaryComponentEnum">The salary component enum.</param>
        /// <param name="numberOfDays">The number of days.</param>
        /// <returns></returns>
        private decimal? GetByNumberOfDays(EmpSalaryStructure empSalaryStructure, EmpSalaryStructure previousEmpSalaryStructure, Decimal? defaultAmount, SalaryComponentEnum salaryComponentEnum, int numberOfDays)
        {
            Decimal? ctcMonthly = GetSalaryComponentAmountByDays(empSalaryStructure.CTC / 12, numberOfDays);
            Decimal? basic = 40 * ctcMonthly / 100;

            switch (salaryComponentEnum)
            {
                case SalaryComponentEnum.CTCPerMonth:
                    return ctcMonthly; // Divide yearly CTC in twelve months.

                case SalaryComponentEnum.Basic:
                    return basic;  // 40% of CTC

                case SalaryComponentEnum.HRA:
                    return 50 * basic / 100; // 50 % of Basic

                case SalaryComponentEnum.Conveyance:
                    return GetSalaryComponentAmountByDays(defaultAmount, numberOfDays);

                case SalaryComponentEnum.PerformanceIncentive:
                    return GetSalaryComponentAmountByDays(GetMonthlyPerformanceIncentive(empSalaryStructure, previousEmpSalaryStructure), numberOfDays); // 5 * CTCMonthly / 100;

                case SalaryComponentEnum.CabDeductions:
                    return GetSalaryComponentAmountByDays(empSalaryStructure.MonthlyCabDeductions, numberOfDays);

                case SalaryComponentEnum.Medical:
                    return GetSalaryComponentAmountByDays(defaultAmount, numberOfDays);

                case SalaryComponentEnum.FoodCoupons:
                    return empSalaryStructure.MonthlyFoodCoupons;

                case SalaryComponentEnum.ProjectIncentive:
                    return GetSalaryComponentAmountByDays(empSalaryStructure.MonthlyProjectIncentive, numberOfDays);

                case SalaryComponentEnum.CarLease:
                    return GetSalaryComponentAmountByDays(empSalaryStructure.MonthlyCarLease, numberOfDays);

                case SalaryComponentEnum.PF:
                    return GetSalaryComponentAmountByDays(12 * basic / 100, numberOfDays);

                case SalaryComponentEnum.Mediclaim:
                    return GetSalaryComponentAmountByDays(GetMonthlyMediclaim(empSalaryStructure), numberOfDays);

                case SalaryComponentEnum.Gratuity:
                    return GetSalaryComponentAmountByDays((((basic * 15) / 26) / 12), numberOfDays);

                case SalaryComponentEnum.LeaveEncashment:
                case SalaryComponentEnum.SalaryArrears:
                case SalaryComponentEnum.LTC:
                case SalaryComponentEnum.OtherDeduction:
                case SalaryComponentEnum.SpecialAllowance:
                case SalaryComponentEnum.Commission:
                case SalaryComponentEnum.Others:
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Gets the salary component amount by days.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="numberOfDays">The number of days.</param>
        /// <returns></returns>
        private Decimal GetSalaryComponentAmountByDays(Decimal? amount, int numberOfDays)
        {
            return amount.IsNotNull() && amount.HasValue ? Math.Round(amount.Value / 30 * numberOfDays, 2) : 0;
        }

        /// <summary>
        /// Gets the performance incentive.
        /// </summary>
        /// <param name="empSalaryStructure">The emp salary structure.</param>
        /// <returns></returns>
        private decimal? GetMonthlyPerformanceIncentive(EmpSalaryStructure empSalaryStructure, EmpSalaryStructure previousEmpSalaryStructure)
        {
            // 5 % of CTC paid Annually or (25% of increment in June and 25% in December)
            Decimal? monthlyPerformanceIncentive = null;

            // If there is no existing Salary Structure, which mean its a new Employee ten by default go for 5 % of CTC paid Annually.
            if (previousEmpSalaryStructure.IsNull())
            {
                monthlyPerformanceIncentive = 5 * empSalaryStructure.CTC / 12 / 100;
            }
            else if (empSalaryStructure.PerformanceIncentivePayable.Equals(Convert.ToInt32(PerformanceIncentive.TwentyFivePercentHalfYearly)))
            {
                monthlyPerformanceIncentive = ((50 * (empSalaryStructure.CTC - previousEmpSalaryStructure.CTC)) * 0.01M) / 12;
            }
            else
            {
                monthlyPerformanceIncentive = 5 * empSalaryStructure.CTC / 12 / 100;
            }

            return monthlyPerformanceIncentive;
        }

        /// <summary>
        /// Gets the calculated mediclaim by month.
        /// </summary>
        /// <param name="monthlyCTC">The monthly CTC.</param>
        /// <param name="monthId">The month identifier.</param>
        /// <returns></returns>
        private decimal? GetMonthlyMediclaim(EmpSalaryStructure empSalaryStructure)
        {
            IMedicalInsuranceRateRepo repo = DataRepositoryFactory.GetDataRepository<IMedicalInsuranceRateRepo>();
            return repo.GetCalculatedMediclaimByMonth(empSalaryStructure);
        }

        private EmpInvestmentDeclarationModel FillUpEmployeeInvestmentDeclarationDetail(List<EmpInvestment> empInvestmentDetail, List<InvestmentCategory> categoryList, int employeeId)
        {
            EmpInvestmentDeclarationModel empInvestmentDeclarationModel = new EmpInvestmentDeclarationModel
            {
                EmployeeId = employeeId
            };
            if (empInvestmentDetail.Count > 0)
            {
                int j = 0;
                List<InvestmentCategoryModel> categoryModelList = new List<InvestmentCategoryModel>();
                foreach (InvestmentCategory investmentCategory in categoryList)
                {
                    InvestmentCategoryModel categoryModel = new InvestmentCategoryModel
                    {
                        InvestmentCategoryId = investmentCategory.InvestmentCategoryId,
                        IsActive = investmentCategory.IsActive,
                        MappingId = investmentCategory.MappingId,
                        Name = investmentCategory.Name,
                        Description = investmentCategory.Description,
                        Code = investmentCategory.Code,
                        DisplayOrder = investmentCategory.DisplayOrder
                    };

                    if (investmentCategory.InvestmentCategoryId == empInvestmentDetail[j].CategoryId)
                    {
                        List<InvestmentSubCategoryModel> subCategoryList = new List<InvestmentSubCategoryModel>();
                        foreach (var subcat in investmentCategory.InvestmentSubCategories)  // iterate the sub categories for each category
                        {
                            InvestmentSubCategoryModel subcategoryModel = new InvestmentSubCategoryModel
                            {
                                EmpInvestmentId = empInvestmentDetail[j].EmpInvestmentId,
                                InvestmentCategoryId = subcat.InvestmentCategoryId,
                                InvestmentSubCategoryId = subcat.InvestmentSubCategoryId,
                                Name = subcat.Name,
                                MaximumLimit = subcat.MaximumLimit,
                                DefaultAmount = empInvestmentDetail[j].DeclaredAmount,
                                ApprovedAmount = empInvestmentDetail[j].ApprovedAmount,
                                Description = subcat.Description,
                                Code = subcat.Code,
                                Remark = subcat.Remark,
                                DisplayOrder = subcat.DisplayOrder,
                                IsActive = subcat.IsActive,
                                IsApproved = empInvestmentDetail[j].IsApproved,
                                ApprovedDate = empInvestmentDetail[j].ApprovedDate
                            };
                            subCategoryList.Add(subcategoryModel);
                            j++;
                        }
                        categoryModel.InvestmentSubCategories = subCategoryList;
                    }
                    categoryModelList.Add(categoryModel);
                }
                empInvestmentDeclarationModel.InvestmentCategories = categoryModelList;
            }
            else
            {
                List<InvestmentCategoryModel> categoryModelList = new List<InvestmentCategoryModel>();
                foreach (var row in categoryList)
                {
                    InvestmentCategoryModel categoryModel = new InvestmentCategoryModel
                    {
                        InvestmentCategoryId = row.InvestmentCategoryId,
                        IsActive = row.IsActive,
                        MappingId = row.MappingId,
                        Name = row.Name,
                        Description = row.Description,
                        Code = row.Code,
                        DisplayOrder = row.DisplayOrder
                    };

                    List<InvestmentSubCategoryModel> subCategoryList = row.InvestmentSubCategories.Select(subcat => new InvestmentSubCategoryModel
                    {
                        InvestmentCategoryId = subcat.InvestmentCategoryId,
                        InvestmentSubCategoryId = subcat.InvestmentSubCategoryId,
                        Name = subcat.Name,
                        MaximumLimit = subcat.MaximumLimit,
                        DefaultAmount = subcat.DefaultAmount,
                        Description = subcat.Description,
                        Code = subcat.Code,
                        Remark = subcat.Remark,
                        DisplayOrder = subcat.DisplayOrder,
                        IsActive = subcat.IsActive,
                        IsApproved = false
                    }).ToList();

                    categoryModel.InvestmentSubCategories = subCategoryList;
                    categoryModelList.Add(categoryModel);

                }

                empInvestmentDeclarationModel.InvestmentCategories = categoryModelList;
            }

            return empInvestmentDeclarationModel;
        }

        #endregion
    }
}