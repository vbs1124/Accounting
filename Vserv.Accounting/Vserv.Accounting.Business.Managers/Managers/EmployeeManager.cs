#region Namespaces

using System;
using System.Collections.Generic;
using Vserv.Accounting.Data;
//using Vserv.Accounting.Data.Contracts;
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
        public List<Employee> GetEmployees()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetEmployees();
            });
        }

        /// <summary>
        /// Adds the address information.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public Address AddAddressInformation(Address address)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.AddAddressInformation(address);
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
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetAddressTypes();
            });
        }

        /// <summary>
        /// Gets the departments.
        /// </summary>
        /// <returns></returns>
        public List<Department> GetDepartments()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetDepartments();
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
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetOfficeBranches();
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
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetSalutations();
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

        #region Designations

        /// <summary>
        /// Gets the designations.
        /// </summary>
        /// <returns></returns>
        public List<Designation> GetDesignations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetDesignations();
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
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetDesignation(designationId);
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
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.IsDesignationExists(name, designationId);
            });
        }

        #endregion Designations

        #region Address

        /// <summary>
        /// Gets the addresses.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public List<Address> GetAddresses(int employeeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetAddresses(employeeId);
            });
        }

        /// <summary>
        /// Gets the cities.
        /// </summary>
        /// <returns></returns>
        public List<City> GetCities()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetCities();
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
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetCities(stateId, cityId);
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
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetStates();
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
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetState(stateId);
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
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetZipCodes(cityId);
            });
        }

        #endregion Address

        #endregion
    }
}
