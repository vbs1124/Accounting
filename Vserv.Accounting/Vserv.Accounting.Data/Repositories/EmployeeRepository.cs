#region Namespaces

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Vserv.Accounting.Common;
using Vserv.Accounting.Data.Entity;

#endregion

namespace Vserv.Accounting.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso></seealso>
    /// <seealso cref="Vserv.Accounting.Data.IEmployeeRepository" />
    [Export(typeof(IEmployeeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmployeeRepository : DataRepositoryBase<Employee>, IEmployeeRepository
    {
        #region Methods

        #region Public Methods

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetEmployees(EmployeeFilter employeeFilter)
        {
            using (var context = new VservAccountingDBEntities())
            {
                if (employeeFilter.Equals(EmployeeFilter.ActiveEmployees))
                {
                    return context.Employees.AsNoTracking()
                    .Include("Designation")
                    .Include("OfficeBranch")
                    .Include("Salutation").Where(employee => employee.IsActive).ToList();
                }
                else if (employeeFilter.Equals(EmployeeFilter.InactiveEmployees))
                {
                    return context.Employees.AsNoTracking()
                   .Include("Designation")
                   .Include("OfficeBranch")
                   .Include("Salutation").Where(employee => !employee.IsActive).ToList();
                }
                else
                {
                    return context.Employees.AsNoTracking()
                   .Include("Designation")
                   .Include("OfficeBranch")
                   .Include("Salutation").ToList();
                }
            }
        }

        /// <summary>
        /// Gets the employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public Employee GetEmployee(int employeeId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                var result = context.Employees.AsNoTracking()
                    .Include("Designation")
                    .Include("OfficeBranch")
                    .Include("Salutation").FirstOrDefault(userProfile => userProfile.EmployeeId == employeeId);
                return result;
            }
        }

        /// <summary>
        /// Adds the employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        public Employee AddEmployee(Employee employee)
        {
            using (var context = new VservAccountingDBEntities())
            {
                context.Employees.Add(employee);
                context.SaveChanges();
                return employee;
            }
        }

        /// <summary>
        /// Edits the employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        public Employee EditEmployee(Employee employee)
        {
            using (var context = new VservAccountingDBEntities())
            {
                Employee existingEmployee = context.Employees.FirstOrDefault(emp => emp.EmployeeId == employee.EmployeeId);
                if (existingEmployee.IsNotNull())
                {
                    context.Entry(existingEmployee).CurrentValues.SetValues(employee);
                    context.SaveChanges();
                }
                return employee;
            }
        }

        /// <summary>
        /// Deletes the employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        public void DeleteEmployee(int employeeId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                Employee existingEmployee = context.Employees.FirstOrDefault(emp => emp.EmployeeId == employeeId);

                if (existingEmployee.IsNotNull())
                {
                    context.Employees.Remove(existingEmployee);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Determines whether [is employee identifier already registered] [the specified vb s_ identifier].
        /// </summary>
        /// <param name="VBS_Id">The vb s_ identifier.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public Boolean IsEmployeeIdAlreadyRegistered(string VBS_Id, int employeeId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Employees.AsNoTracking().Any(employee => employee.EmployeeId != employeeId && employee.VBS_Id.Contains(VBS_Id));
            }
        }

        /// <summary>
        /// Determines whether [is email already registered] [the specified email address].
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public Boolean IsEmailAlreadyRegistered(string emailAddress, int employeeId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Employees.AsNoTracking().Any(employee => employee.EmployeeId != employeeId && employee.OfficialEmailAddress.Contains(emailAddress));
            }
        }

        /// <summary>
        /// Determines whether [is mobile number already registered] [the specified mobile number].
        /// </summary>
        /// <param name="mobileNumber">The mobile number.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public Boolean IsMobileNumberAlreadyRegistered(string mobileNumber, int employeeId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Employees.AsNoTracking().Any(employee => employee.EmployeeId != employeeId && employee.MobileNumber.Contains(mobileNumber));
            }
        }

        /// <summary>
        /// Retrieves the Employee count.
        /// </summary>
        /// <returns></returns>
        public int GetEmployeeCount()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Employees.AsNoTracking().Count(con => con.IsActive);
            }
        }

        /// <summary>
        /// Archive Employee information to maintain audit trail.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="updatedByUserName"></param>
        public void ArchiveEmployee(int employeeId, string updatedByUserName)
        {
            using (var context = new VservAccountingDBEntities())
            {
                context.ArchiveEmployee(employeeId, updatedByUserName);
            }
        }

        /// <summary>
        /// Retrieves Employee History based on EmployeeId.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public List<EmployeeArchive> GetEmployeeHistory(int employeeId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.EmployeeArchives.Where(emp => emp.EmployeeId == employeeId).OrderByDescending(order => order.EmployeeArchiveId).ToList();
            }
        }

        public CompareEmployeeModel GetMatchingEmployeeInformation(int employeeArchiveId)
        {
            CompareEmployeeModel compareEmployeeModel = new CompareEmployeeModel();
            using (var context = new VservAccountingDBEntities())
            {
                var employeeArchive = context.EmployeeArchives.AsNoTracking()
                    .Include("Bank")
                    .Include("Designation")
                    .Include("Gender")
                    .Include("State")
                    .Include("OfficeBranch")
                    .Include("State1")
                    .Include("Salutation")
                    .FirstOrDefault(emp => emp.EmployeeArchiveId == employeeArchiveId);

                compareEmployeeModel.PreviousEmployeeInfo = new EmployeeMapper
                {
                    EmployeeId = employeeArchive.EmployeeId,
                    FirstName = employeeArchive.FirstName ?? String.Empty,
                    MiddleName = employeeArchive.MiddleName ?? String.Empty,
                    LastName = employeeArchive.LastName ?? String.Empty,
                    FatherName = employeeArchive.FatherName ?? String.Empty,
                    MotherName = employeeArchive.MotherName ?? String.Empty,
                    PermanentAccountNumber = employeeArchive.PermanentAccountNumber ?? String.Empty,
                    UniversalAccountNumber = employeeArchive.UniversalAccountNumber ?? String.Empty,
                    EPFNumber = employeeArchive.EPFNumber ?? String.Empty,
                    AADHAARNumber = employeeArchive.AADHAARNumber ?? String.Empty,
                    ESINumber = employeeArchive.ESINumber ?? String.Empty,
                    MobileNumber = employeeArchive.MobileNumber ?? String.Empty,
                    OfficialEmailAddress = employeeArchive.OfficialEmailAddress,
                    PersonalEmailAddress = employeeArchive.PersonalEmailAddress,
                    BirthDay = employeeArchive.BirthDay,
                    JoiningDate = employeeArchive.JoiningDate,
                    RelievingDate = employeeArchive.RelievingDate,
                    ResignationDate = employeeArchive.ResignationDate,
                    VBS_Id = employeeArchive.VBS_Id,
                    DesignationName = employeeArchive.Designation.Name,
                    SalutationName = employeeArchive.Salutation.Name,
                    GenderName = employeeArchive.Gender.Name,
                    OfficeBranchName = employeeArchive.OfficeBranch.Name,
                    PermanentAddress1 = employeeArchive.PermanentAddress1,
                    PermanentAddress2 = employeeArchive.PermanentAddress2,
                    PermanentCity = employeeArchive.PermanentCity,
                    PermanentZipCode = employeeArchive.PermanentZipCode,
                    PermanentStateName = employeeArchive.State1.Name,
                    MailingAddress1 = employeeArchive.MailingAddress1,
                    MailingAddress2 = employeeArchive.MailingAddress2,
                    MailingCity = employeeArchive.MailingCity,
                    MailingZipCode = employeeArchive.MailingZipCode,
                    MailingStateName = employeeArchive.State.Name,
                    IsMetro = employeeArchive.IsMetro,
                    BankAccountNumber = employeeArchive.BankAccountNumber,
                    BankName = employeeArchive.Bank.Name,
                    BankIFSCCode = employeeArchive.BankIFSCCode,
                    BankMICRCode = employeeArchive.BankMICRCode,
                    IsActive = employeeArchive.IsActive,
                    CreatedBy = employeeArchive.CreatedBy,
                    UpdatedBy = employeeArchive.UpdatedBy,
                    CreatedDate = employeeArchive.CreatedDate,
                    UpdatedDate = employeeArchive.UpdatedDate
                };

                if (compareEmployeeModel.PreviousEmployeeInfo.IsNotNull())
                {
                    var employee = context.Employees.AsNoTracking()
                        .Include("Bank")
                        .Include("Designation")
                        .Include("Gender")
                        .Include("State")
                        .Include("OfficeBranch")
                        .Include("State1")
                        .Include("Salutation")
                        .FirstOrDefault(emp => emp.EmployeeId == compareEmployeeModel.PreviousEmployeeInfo.EmployeeId);

                    compareEmployeeModel.CurrentEmployeeInfo = new EmployeeMapper
                    {
                        EmployeeId = employee.EmployeeId,
                        FirstName = employee.FirstName,
                        MiddleName = employee.MiddleName,
                        LastName = employee.LastName,
                        FatherName = employee.FatherName,
                        MotherName = employee.MotherName,
                        PermanentAccountNumber = employee.PermanentAccountNumber,
                        UniversalAccountNumber = employee.UniversalAccountNumber,
                        EPFNumber = employee.EPFNumber,
                        AADHAARNumber = employee.AADHAARNumber,
                        ESINumber = employee.ESINumber,
                        MobileNumber = employee.MobileNumber,
                        OfficialEmailAddress = employee.OfficialEmailAddress,
                        PersonalEmailAddress = employee.PersonalEmailAddress,
                        BirthDay = employee.BirthDay,
                        JoiningDate = employee.JoiningDate,
                        RelievingDate = employee.RelievingDate,
                        ResignationDate = employee.ResignationDate,
                        VBS_Id = employee.VBS_Id,
                        DesignationName = employee.Designation.Name,
                        SalutationName = employee.Salutation.Name,
                        GenderName = employee.Gender.Name,
                        OfficeBranchName = employee.OfficeBranch.Name,
                        PermanentAddress1 = employee.PermanentAddress1,
                        PermanentAddress2 = employee.PermanentAddress2,
                        PermanentCity = employee.PermanentCity,
                        PermanentZipCode = employee.PermanentZipCode,
                        PermanentStateName = employee.State1.Name,
                        MailingAddress1 = employee.MailingAddress1,
                        MailingAddress2 = employee.MailingAddress2,
                        MailingCity = employee.MailingCity,
                        MailingZipCode = employee.MailingZipCode,
                        MailingStateName = employee.State.Name,
                        IsMetro = employee.IsMetro,
                        BankAccountNumber = employee.BankAccountNumber,
                        BankName = employee.Bank.Name,
                        BankIFSCCode = employee.BankIFSCCode,
                        BankMICRCode = employee.BankMICRCode,
                        IsActive = employee.IsActive,
                        CreatedBy = employee.CreatedBy,
                        UpdatedBy = employee.UpdatedBy,
                        CreatedDate = employee.CreatedDate,
                        UpdatedDate = employee.UpdatedDate
                    };
                }
            }
            //SetModifiedColumnCount(compareEmployeeModel);
            return compareEmployeeModel;
        }

        public List<GetEmployeeSalaryDetail_Result> GetYearlyPaySheet(int? employeeId, int? financialYearFrom, int? financialYearTo)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.GetEmployeeSalaryDetail(employeeId, financialYearFrom, financialYearTo).ToList();
            }
        }

        #endregion Public Methods

        #region Private Methods

        #endregion Private Methods

        #endregion

        #region Compiled Query

        //  public static readonly Func<VservAccountingDBEntities, Int32, Employee> CompiledGetEmployee =
        //CompiledQuery.Compile<VservAccountingDBEntities, Int32, Employee>((dbContext, employeeId) => dbContext.Employees
        //              .Include("Designation").Include("OfficeBranch")
        //              .Include("Salutation").FirstOrDefault(userProfile => userProfile.EmployeeId == employeeId));
        #endregion
    }
}