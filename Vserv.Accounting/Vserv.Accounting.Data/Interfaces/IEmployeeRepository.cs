using System;
using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

namespace Vserv.Accounting.Data
{
    public interface IEmployeeRepository : IDisposable
    {
        #region Methods

        #region Employees

        List<Employee> GetEmployees();
        Employee GetEmployee(int employeeId);
        Employee AddEmployee(Employee employee);
        Employee EditEmployee(Employee employee);
        Boolean IsEmployeeIdAlreadyRegistered(string VBS_Id, int employeeId);
        Boolean IsEmailAlreadyRegistered(string emailAddress, int employeeId);
        Boolean IsMobileNumberAlreadyRegistered(string mobileNumber, int employeeId);

        #endregion Employees

        #region Designations

        List<Designation> GetDesignations();
        void AddDesignation(Designation designation);
        void UpdateDesignation(Designation designation);
        Designation GetDesignation(int designationId);
        Boolean IsDesignationExists(string name, int designationId);

        #endregion Designations

        #region Address

        List<Address> GetAddresses(int employeeId);
        List<City> GetCities();
        List<City> GetCities(int stateId, int? cityId);
        List<State> GetStates();
        State GetState(int stateId);
        List<ZipCode> GetZipCodes(int cityId);
        List<AddressType> GetAddressTypes();
        Address AddAddressInformation(Address address);


        #endregion Address

        #region Miscellaneous

        List<Department> GetDepartments();
        List<OfficeBranch> GetOfficeBranches();
        List<Salutation> GetSalutations();

        #endregion Miscellaneous

        #endregion
    }
}
