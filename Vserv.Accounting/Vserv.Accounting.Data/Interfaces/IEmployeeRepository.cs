using System;
using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

namespace Vserv.Accounting.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Vserv.Common.Contracts.IDataRepository{Vserv.Accounting.Data.Entity.Employee}" />
    public interface IEmployeeRepository : IDataRepository<Employee>// IDisposable
    {
        #region Methods

        #region Employees

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

        #endregion Employees

        #region Designations

        /// <summary>
        /// Gets the designations.
        /// </summary>
        /// <returns></returns>
        List<Designation> GetDesignations();
        /// <summary>
        /// Adds the designation.
        /// </summary>
        /// <param name="designation">The designation.</param>
        void AddDesignation(Designation designation);
        /// <summary>
        /// Updates the designation.
        /// </summary>
        /// <param name="designation">The designation.</param>
        void UpdateDesignation(Designation designation);
        /// <summary>
        /// Gets the designation.
        /// </summary>
        /// <param name="designationId">The designation identifier.</param>
        /// <returns></returns>
        Designation GetDesignation(int designationId);
        /// <summary>
        /// Determines whether [is designation exists] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="designationId">The designation identifier.</param>
        /// <returns></returns>
        Boolean IsDesignationExists(string name, int designationId);

        #endregion Designations

        #region Address

        /// <summary>
        /// Gets the addresses.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        List<Address> GetAddresses(int employeeId);
        /// <summary>
        /// Gets the cities.
        /// </summary>
        /// <returns></returns>
        List<City> GetCities();
        /// <summary>
        /// Gets the cities.
        /// </summary>
        /// <param name="stateId">The state identifier.</param>
        /// <param name="cityId">The city identifier.</param>
        /// <returns></returns>
        List<City> GetCities(int stateId, int? cityId);
        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <returns></returns>
        List<State> GetStates();
        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <param name="stateId">The state identifier.</param>
        /// <returns></returns>
        State GetState(int stateId);
        /// <summary>
        /// Gets the zip codes.
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <returns></returns>
        List<ZipCode> GetZipCodes(int cityId);
        /// <summary>
        /// Gets the address types.
        /// </summary>
        /// <returns></returns>
        List<AddressType> GetAddressTypes();
        /// <summary>
        /// Adds the address information.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        Address AddAddressInformation(Address address);

        #endregion Address

        #region Miscellaneous

        /// <summary>
        /// Gets the departments.
        /// </summary>
        /// <returns></returns>
        List<Department> GetDepartments();
        /// <summary>
        /// Gets the office branches.
        /// </summary>
        /// <returns></returns>
        List<OfficeBranch> GetOfficeBranches();
        /// <summary>
        /// Gets the salutations.
        /// </summary>
        /// <returns></returns>
        List<Salutation> GetSalutations();

        #endregion Miscellaneous

        #endregion
    }
}
