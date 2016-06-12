#region Namespaces

using System;
using System.Collections.Generic;
using Vserv.Accounting.Common;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

#endregion

namespace Vserv.Accounting.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEmployeeRepository : IDataRepository<Employee>
    {
        #region Methods

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns></returns>
        List<Employee> GetEmployees(EmployeeFilter employeeFilter);

        /// <summary>
        /// Gets the employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        Employee GetEmployee(int employeeId);

        /// <summary>
        /// Adds the employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        Employee AddEmployee(Employee employee);

        /// <summary>
        /// Edits the employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        Employee EditEmployee(Employee employee);

        /// <summary>
        /// Deletes the employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        void DeleteEmployee(int employeeId);

        /// <summary>
        /// Determines whether [is employee identifier already registered] [the specified vb s_ identifier].
        /// </summary>
        /// <param name="VBS_Id">The vb s_ identifier.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        Boolean IsEmployeeIdAlreadyRegistered(string VBS_Id, int employeeId);

        /// <summary>
        /// Determines whether [is email already registered] [the specified email address].
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        Boolean IsEmailAlreadyRegistered(string emailAddress, int employeeId);

        /// <summary>
        /// Determines whether [is mobile number already registered] [the specified mobile number].
        /// </summary>
        /// <param name="mobileNumber">The mobile number.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        Boolean IsMobileNumberAlreadyRegistered(string mobileNumber, int employeeId);

        /// <summary>
        /// Retrieves the Employee count.
        /// </summary>
        /// <returns></returns>
        int GetEmployeeCount();

        /// <summary>
        /// Archive Employee information to maintain audit trail.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="updatedByUserName"></param>
        void ArchiveEmployee(int employeeId, string updatedByUserName);

        /// <summary>
        /// Retrieves Employee History based on EmployeeId.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        List<EmployeeArchive> GetEmployeeHistory(int employeeId);

        CompareEmployeeModel GetMatchingEmployeeInformation(int employeeArchiveId);

        List<GetEmployeeSalaryDetail_Result> GetYearlyPaySheet(int? employeeId, int? financialYearFrom, int? financialYearTo);

        #endregion
    }
}
