#region Namespaces

using System;
using System.Collections.Generic;
using Vserv.Accounting.Data;
//using Vserv.Accounting.Data.Contracts;
using Vserv.Accounting.Data.Entity;

#endregion

namespace Vserv.Accounting.Business.Managers
{
    public class EmployeeManager : ManagerBase
    {
        #region Properties

        public IEmployeeRepository EmployeeRepository
        {
            get;
            set;
        }

        #endregion Properties

        #region Constructor

        public EmployeeManager()
        {

        }

        #endregion Constructor

        #region Public Methods

        public List<Employee> GetEmployees()
        {
            CreateFactoryInstance();
            return ExecuteFaultHandledOperation(() =>
            {
                using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
                {
                    return EmployeeRepository.GetEmployees();
                }
            });
        }

        public Address AddAddressInformation(Address address)
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                return EmployeeRepository.AddAddressInformation(address);
            }
        }

        public Employee GetEmployee(int employeeId)
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                return EmployeeRepository.GetEmployee(employeeId);
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                EmployeeRepository.DeleteEmployee(employeeId);
            }
        }

        public Employee AddEmployee(Employee employee)
        {
            try
            {
                CreateFactoryInstance();
                using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
                {
                    return EmployeeRepository.AddEmployee(employee);
                }
            }
            catch
            {

                throw;
            }

        }

        public Employee EditEmployee(Employee employee)
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                return EmployeeRepository.EditEmployee(employee);
            }
        }

        public List<AddressType> GetAddressTypes()
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                return EmployeeRepository.GetAddressTypes();
            }
        }

        public List<Department> GetDepartments()
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                return EmployeeRepository.GetDepartments();
            }
        }

        public List<OfficeBranch> GetOfficeBranches()
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                return EmployeeRepository.GetOfficeBranches();
            }
        }

        public List<Salutation> GetSalutations()
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                return EmployeeRepository.GetSalutations();
            }
        }

        public Boolean IsEmployeeIdAlreadyRegistered(string VBS_Id, int employeeId)
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                return EmployeeRepository.IsEmployeeIdAlreadyRegistered(VBS_Id, employeeId);
            }
        }

        public Boolean IsEmailAlreadyRegistered(string emailAddress, int employeeId)
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                return EmployeeRepository.IsEmailAlreadyRegistered(emailAddress, employeeId);
            }
        }

        public Boolean IsMobileNumberAlreadyRegistered(string mobileNumber, int employeeId)
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                return EmployeeRepository.IsMobileNumberAlreadyRegistered(mobileNumber, employeeId);
            }
        }

        #region Designations

        public List<Designation> GetDesignations()
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                return EmployeeRepository.GetDesignations();
            }
        }

        public Designation GetDesignation(int designationId)
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                return EmployeeRepository.GetDesignation(designationId);
            }
        }

        public void AddDesignation(Designation designation)
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                EmployeeRepository.AddDesignation(designation);
            }
        }

        public void UpdateDesignation(Designation designation)
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                EmployeeRepository.UpdateDesignation(designation);
            }
        }

        public Boolean IsDesignationExists(string name, int designationId)
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                return EmployeeRepository.IsDesignationExists(name, designationId);
            }
        }
        #endregion Designations

        #region Address

        public List<Address> GetAddresses(int employeeId)
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                return EmployeeRepository.GetAddresses(employeeId);
            }
        }

        public List<City> GetCities()
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                return EmployeeRepository.GetCities();
            }
        }

        public List<City> GetCities(int stateId, int? cityId)
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                return EmployeeRepository.GetCities(stateId, cityId);
            }
        }

        public List<State> GetStates()
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                var result = EmployeeRepository.GetStates();
                return result;
            }
        }

        public State GetState(int stateId)
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                return EmployeeRepository.GetState(stateId);
            }
        }

        public List<ZipCode> GetZipCodes(int cityId)
        {
            CreateFactoryInstance();
            using (EmployeeRepository = VservHostFactory.GetEmployeeRepositoryInstance())
            {
                return EmployeeRepository.GetZipCodes(cityId);
            }
        }

        #endregion Address

        #endregion
    }
}
