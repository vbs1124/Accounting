using System;
using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

namespace Vserv.Accounting.Data.Contracts
{
    public interface IEmployeeRepository : IDisposable
    {
        List<Employee> GetEmployees();

        Employee GetEmployee(int employeeId);

        Employee AddEmployee(Employee employee);

        Employee EditEmployee(Employee employee);

        List<AddressType> GetAddressTypes();

        List<Department> GetDepartments();

        List<Designation> GetDesignations();

        List<OfficeBranch> GetOfficeBranches();

        List<Salutation> GetSalutations();

        Address AddAddressInformation(Address address);


        #region Address

        List<Address> GetAddresses(int employeeId);

        List<City> GetCities();

        List<City> GetCities(int stateId, int? cityId);

        List<State> GetStates();

        State GetState(int stateId);

        List<ZipCode> GetZipCodes(int cityId);

        #endregion Address
    }
}
