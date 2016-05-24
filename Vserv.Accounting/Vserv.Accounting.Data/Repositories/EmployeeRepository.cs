#region Namespaces
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Extensions;
#endregion

namespace Vserv.Accounting.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Vserv.Accounting.Data.DataRepositoryBase{Vserv.Accounting.Data.Entity.Employee}" />
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
        public List<Employee> GetEmployees()
        {
            using (var context = new VservAccountingDBEntities())
            {
                var result = context.Employees
                    .Include("Designation")
                    .Include("OfficeBranch")
                    .Include("Salutation").ToList();
                return result;
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
                Employee aaa = new Employee();
                var result = context.Employees
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
            try
            {
                using (var context = new VservAccountingDBEntities())
                {
                    context.Employees.Add(employee);
                    context.SaveChanges();
                    return employee;
                }
            }
            catch
            {

                throw;
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
                return context.Employees.Any(employee => employee.EmployeeId != employeeId && employee.VBS_Id.Contains(VBS_Id));
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
                return context.Employees.Any(employee => employee.EmployeeId != employeeId && employee.OfficialEmailAddress.Contains(emailAddress));
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
                return context.Employees.Any(employee => employee.EmployeeId != employeeId && employee.MobileNumber.Contains(mobileNumber));
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
                return context.Employees.Count();
            }
        }

        /// <summary>
        /// Archive Employee information to maintain audit trail.
        /// </summary>
        /// <param name="employeeId"></param>
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
                compareEmployeeModel.PreviousEmployeeInfo = context.EmployeeArchives
                    .Include("Bank")
                    .Include("Designation")
                    .Include("Gender")
                    .Include("State")
                    .Include("OfficeBranch")
                    .Include("State1")
                    .Include("Salutation")
                    .FirstOrDefault(emp => emp.EmployeeArchiveId == employeeArchiveId);

                if (compareEmployeeModel.PreviousEmployeeInfo.IsNotNull())
                {
                    compareEmployeeModel.CurrentEmployeeInfo = context.Employees
                        .Include("Bank")
                        .Include("Designation")
                        .Include("Gender")
                        .Include("State")
                        .Include("OfficeBranch")
                        .Include("State1")
                        .Include("Salutation")
                        .FirstOrDefault(emp => emp.EmployeeId == compareEmployeeModel.PreviousEmployeeInfo.EmployeeId);
                }
            }

            //SetModifiedColumnCount(compareEmployeeModel);
            return compareEmployeeModel;
        }

        private void SetModifiedColumnCount(CompareEmployeeModel compareEmployeeModel)
        {
            int modifiedColumnCount = 0;
            if (!compareEmployeeModel.CurrentEmployeeInfo.FirstName.Equals(compareEmployeeModel.PreviousEmployeeInfo.FirstName))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.MiddleName.Equals(compareEmployeeModel.PreviousEmployeeInfo.MiddleName))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.LastName.Equals(compareEmployeeModel.PreviousEmployeeInfo.LastName))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.FatherName.Equals(compareEmployeeModel.PreviousEmployeeInfo.FatherName))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.MotherName.Equals(compareEmployeeModel.PreviousEmployeeInfo.MotherName))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.PermanentAccountNumber.Equals(compareEmployeeModel.PreviousEmployeeInfo.PermanentAccountNumber))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.UniversalAccountNumber.Equals(compareEmployeeModel.PreviousEmployeeInfo.UniversalAccountNumber))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.EPFNumber.Equals(compareEmployeeModel.PreviousEmployeeInfo.EPFNumber))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.AADHAARNumber.Equals(compareEmployeeModel.PreviousEmployeeInfo.AADHAARNumber))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.ESINumber.Equals(compareEmployeeModel.PreviousEmployeeInfo.ESINumber))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.MobileNumber.Equals(compareEmployeeModel.PreviousEmployeeInfo.MobileNumber))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.OfficialEmailAddress.Equals(compareEmployeeModel.PreviousEmployeeInfo.OfficialEmailAddress))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.PersonalEmailAddress.Equals(compareEmployeeModel.PreviousEmployeeInfo.PersonalEmailAddress))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.BirthDay.Equals(compareEmployeeModel.PreviousEmployeeInfo.BirthDay))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.JoiningDate.Equals(compareEmployeeModel.PreviousEmployeeInfo.JoiningDate))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.RelievingDate.Equals(compareEmployeeModel.PreviousEmployeeInfo.RelievingDate))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.ResignationDate.Equals(compareEmployeeModel.PreviousEmployeeInfo.ResignationDate))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.VBS_Id.Equals(compareEmployeeModel.PreviousEmployeeInfo.VBS_Id))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.DesignationId.Equals(compareEmployeeModel.PreviousEmployeeInfo.DesignationId))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.SalutationId.Equals(compareEmployeeModel.PreviousEmployeeInfo.SalutationId))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.GenderId.Equals(compareEmployeeModel.PreviousEmployeeInfo.GenderId))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.OfficeBranchId.Equals(compareEmployeeModel.PreviousEmployeeInfo.OfficeBranchId))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.PermanentAddress1.Equals(compareEmployeeModel.PreviousEmployeeInfo.PermanentAddress1))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.PermanentAddress2.Equals(compareEmployeeModel.PreviousEmployeeInfo.PermanentAddress2))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.PermanentCity.Equals(compareEmployeeModel.PreviousEmployeeInfo.PermanentCity))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.PermanentZipCode.Equals(compareEmployeeModel.PreviousEmployeeInfo.PermanentZipCode))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.PermanentStateId.Equals(compareEmployeeModel.PreviousEmployeeInfo.PermanentStateId))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.PermanentCountryId.Equals(compareEmployeeModel.PreviousEmployeeInfo.PermanentCountryId))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.MailingAddress1.Equals(compareEmployeeModel.PreviousEmployeeInfo.MailingAddress1))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.MailingAddress2.Equals(compareEmployeeModel.PreviousEmployeeInfo.MailingAddress2))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.MailingCity.Equals(compareEmployeeModel.PreviousEmployeeInfo.MailingCity))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.MailingZipCode.Equals(compareEmployeeModel.PreviousEmployeeInfo.MailingZipCode))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.MailingStateId.Equals(compareEmployeeModel.PreviousEmployeeInfo.MailingStateId))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.MailingCountryId.Equals(compareEmployeeModel.PreviousEmployeeInfo.MailingCountryId))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.IsMetro.Equals(compareEmployeeModel.PreviousEmployeeInfo.IsMetro))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.BankAccountNumber.Equals(compareEmployeeModel.PreviousEmployeeInfo.BankAccountNumber))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.BankId.Equals(compareEmployeeModel.PreviousEmployeeInfo.BankId))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.BankIFSCCode.Equals(compareEmployeeModel.PreviousEmployeeInfo.BankIFSCCode))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.BankMICRCode.Equals(compareEmployeeModel.PreviousEmployeeInfo.BankMICRCode))
                modifiedColumnCount++;
            if (!compareEmployeeModel.CurrentEmployeeInfo.IsActive.Equals(compareEmployeeModel.PreviousEmployeeInfo.IsActive))
                modifiedColumnCount++;

            compareEmployeeModel.ModifiedColumnCount = modifiedColumnCount;
        }

        #endregion Public Methods

        #region Private Methods

        #endregion Private Methods

        #endregion
    }
}