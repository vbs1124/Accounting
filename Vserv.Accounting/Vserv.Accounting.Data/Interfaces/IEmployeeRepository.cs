#region Namespaces
using System;
using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;
#endregion

namespace Vserv.Accounting.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Vserv.Common.Contracts.IDataRepository{Vserv.Accounting.Data.Entity.Employee}" />
    public interface IEmployeeRepository : IDataRepository<Employee>
    {
        #region Methods

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns></returns>
        List<Employee> GetEmployees();

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

        #endregion
    }
}
