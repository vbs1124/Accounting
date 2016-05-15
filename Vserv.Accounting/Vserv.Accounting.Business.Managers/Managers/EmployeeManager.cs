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
    public class EmployeeManager : ManagerBase
    {
        #region Properties

        #endregion Properties

        #region Constructor

        public EmployeeManager()
        {

        }

        public EmployeeManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        #endregion Constructor

        #region Public Methods

        public List<Employee> GetEmployees()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetEmployees();
            });
        }

        public Address AddAddressInformation(Address address)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.AddAddressInformation(address);
            });
        }

        public Employee GetEmployee(int employeeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetEmployee(employeeId);
            });
        }

        public void DeleteEmployee(int employeeId)
        {
            ExecuteFaultHandledOperation(() =>
          {
              var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
              _employeeRepository.DeleteEmployee(employeeId);
          });
        }

        public Employee AddEmployee(Employee employee)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.AddEmployee(employee);
            });
        }

        public Employee EditEmployee(Employee employee)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.EditEmployee(employee);
            });
        }

        public List<AddressType> GetAddressTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetAddressTypes();
            });
        }

        public List<Department> GetDepartments()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetDepartments();
            });
        }

        public List<OfficeBranch> GetOfficeBranches()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetOfficeBranches();
            });
        }

        public List<Salutation> GetSalutations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetSalutations();
            });
        }

        public Boolean IsEmployeeIdAlreadyRegistered(string VBS_Id, int employeeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.IsEmployeeIdAlreadyRegistered(VBS_Id, employeeId);
            });
        }

        public Boolean IsEmailAlreadyRegistered(string emailAddress, int employeeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.IsEmailAlreadyRegistered(emailAddress, employeeId);
            });
        }

        public Boolean IsMobileNumberAlreadyRegistered(string mobileNumber, int employeeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.IsMobileNumberAlreadyRegistered(mobileNumber, employeeId);
            });
        }

        #region Designations

        public List<Designation> GetDesignations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetDesignations();
            });
        }

        public Designation GetDesignation(int designationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetDesignation(designationId);
            });
        }

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

        public List<Address> GetAddresses(int employeeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetAddresses(employeeId);
            });
        }

        public List<City> GetCities()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetCities();
            });
        }

        public List<City> GetCities(int stateId, int? cityId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetCities(stateId, cityId);
            });
        }

        public List<State> GetStates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetStates();
            });
        }

        public State GetState(int stateId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetState(stateId);
            });
        }

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
