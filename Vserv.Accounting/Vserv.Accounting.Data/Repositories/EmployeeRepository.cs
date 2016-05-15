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
    [Export(typeof(IEmployeeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmployeeRepository : DataRepositoryBase<Employee>, IEmployeeRepository
    {
        #region Methods

        #region Public Methods

        #region Employees

        public List<Employee> GetEmployees()
        {
            using (var context = new VservAccountingDBEntities())
            {
                var result = context.Employees
                    .Include("Designation")
                    .Include("OfficeBranch")
                    .Include("Salutation")
                    .Include("EmployeeAddresses").ToList();
                return result;
            }
        }

        public Employee GetEmployee(int employeeId)
        {

            using (var context = new VservAccountingDBEntities())
            {
                Employee aaa = new Employee();
                var result = context.Employees
                    .Include("Designation")
                    .Include("OfficeBranch")
                    .Include("Salutation")
                    .Include("EmployeeAddresses.Address").FirstOrDefault(userProfile => userProfile.EmployeeId == employeeId);
                return result;
            }
        }

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

        public Employee EditEmployee(Employee employee)
        {
            using (var context = new VservAccountingDBEntities())
            {
                Employee existingEmployee = context.Employees.Include("EmployeeAddresses.Address").FirstOrDefault(emp => emp.EmployeeId == employee.EmployeeId);
                if (existingEmployee.IsNotNull())
                {
                    foreach (var item in existingEmployee.EmployeeAddresses)
                    {
                        var existingAddress = item.Address;
                        var updatedAddress = employee.EmployeeAddresses.FirstOrDefault(condition => condition.Address.AddressId == existingAddress.AddressId).Address;
                        context.Entry(existingAddress).CurrentValues.SetValues(updatedAddress);
                    }

                    context.Entry(existingEmployee).CurrentValues.SetValues(employee);
                    context.SaveChanges();
                }
                return employee;
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                Employee existingEmployee = context.Employees.Include("EmployeeAddresses.Address").FirstOrDefault(emp => emp.EmployeeId == employeeId);

                if (existingEmployee.IsNotNull())
                {
                    var employeeAddresses = existingEmployee.EmployeeAddresses.ToList();
                    foreach (var item in employeeAddresses)
                    {
                        context.EmployeeAddresses.Remove(item);
                        if (item.Address.IsNotNull())
                        {
                            context.Addresses.Remove(item.Address);
                        }
                    }

                    context.Employees.Remove(existingEmployee);
                    context.SaveChanges();
                }
            }
        }

        public Boolean IsEmployeeIdAlreadyRegistered(string VBS_Id, int employeeId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Employees.Any(employee => employee.EmployeeId != employeeId && employee.VBS_Id.Contains(VBS_Id));
            }
        }

        public Boolean IsEmailAlreadyRegistered(string emailAddress, int employeeId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Employees.Any(employee => employee.EmployeeId != employeeId && employee.EmailAddress.Contains(emailAddress));
            }
        }

        public Boolean IsMobileNumberAlreadyRegistered(string mobileNumber, int employeeId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Employees.Any(employee => employee.EmployeeId != employeeId && employee.MobileNumber.Contains(mobileNumber));
            }
        }

        #endregion Employees

        #region Designations

        public List<Designation> GetDesignations()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Designations.ToList();
            }
        }

        public void AddDesignation(Designation designation)
        {
            using (var context = new VservAccountingDBEntities())
            {
                context.Designations.Add(designation);
                context.SaveChanges();
            }
        }

        public void UpdateDesignation(Designation designation)
        {
            using (var context = new VservAccountingDBEntities())
            {
                Designation existingDesignation = context.Designations.FirstOrDefault(desg => desg.DesignationId == designation.DesignationId);
                if (existingDesignation.IsNotNull())
                {
                    context.Entry(existingDesignation).CurrentValues.SetValues(designation);
                    context.SaveChanges();
                }
            }
        }

        public Designation GetDesignation(int designationId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Designations.FirstOrDefault(desg => desg.DesignationId == designationId);
            }
        }

        public Boolean IsDesignationExists(string name, int designationId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Designations.Any(desg => desg.DesignationId != designationId && desg.Name == name);
            }
        }

        #endregion Designations

        #region Address

        public List<Address> GetAddresses(int employeeId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return null;
                //var result = context.EmployeeAddresses.Where(condition => condition.EmployeeId == employeeId).ToList().Select(ss => ss.AddressId).ToList();
                //return context.Addresses.Where(address => result.Contains(address.AddressId)).ToList();
            }
        }

        public List<City> GetCities()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Cities.ToList();
            }
        }

        public List<City> GetCities(int stateId, int? cityId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                if (cityId.IsNotNull() && cityId.HasValue)
                {
                    return context.Cities.Where(city => city.CityId == cityId.Value).ToList();
                }

                return context.Cities.Where(city => city.StateId == stateId).ToList();
            }
        }

        public List<State> GetStates()
        {
            using (var context = new VservAccountingDBEntities())
            {
                var result = context.States.ToList();
                return result;
            }
        }

        public State GetState(int stateId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.States.FirstOrDefault(state => state.StateId == stateId);
            }
        }

        public List<ZipCode> GetZipCodes(int cityId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.ZipCodes.Where(zipcode => zipcode.CityId == cityId).ToList();
            }
        }

        public Address AddAddressInformation(Address address)
        {
            using (var context = new VservAccountingDBEntities())
            {
                context.Addresses.Add(address);
                context.SaveChanges();
                return address;
            }
        }

        #endregion Address

        #region Miscellaneous

        public List<AddressType> GetAddressTypes()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.AddressTypes.ToList();
            }
        }

        public List<Department> GetDepartments()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Departments.ToList();
            }
        }

        public List<OfficeBranch> GetOfficeBranches()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.OfficeBranches.ToList();
            }
        }

        public List<Salutation> GetSalutations()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Salutations.ToList();
            }
        }

        #endregion Miscellaneous

        #endregion Public Methods

        #region Private Methods

        #endregion Private Methods

        #endregion
    }
}