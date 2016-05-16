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

        #region Employees

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
                    .Include("Salutation")
                    .Include("EmployeeAddresses").ToList();
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
                    .Include("Salutation")
                    .Include("EmployeeAddresses.Address").FirstOrDefault(userProfile => userProfile.EmployeeId == employeeId);
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

        /// <summary>
        /// Deletes the employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
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
                return context.Employees.Any(employee => employee.EmployeeId != employeeId && employee.EmailAddress.Contains(emailAddress));
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

        #endregion Employees

        #region Designations

        /// <summary>
        /// Gets the designations.
        /// </summary>
        /// <returns></returns>
        public List<Designation> GetDesignations()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Designations.ToList();
            }
        }

        /// <summary>
        /// Adds the designation.
        /// </summary>
        /// <param name="designation">The designation.</param>
        public void AddDesignation(Designation designation)
        {
            using (var context = new VservAccountingDBEntities())
            {
                context.Designations.Add(designation);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the designation.
        /// </summary>
        /// <param name="designation">The designation.</param>
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

        /// <summary>
        /// Gets the designation.
        /// </summary>
        /// <param name="designationId">The designation identifier.</param>
        /// <returns></returns>
        public Designation GetDesignation(int designationId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Designations.FirstOrDefault(desg => desg.DesignationId == designationId);
            }
        }

        /// <summary>
        /// Determines whether [is designation exists] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="designationId">The designation identifier.</param>
        /// <returns></returns>
        public Boolean IsDesignationExists(string name, int designationId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Designations.Any(desg => desg.DesignationId != designationId && desg.Name == name);
            }
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
            using (var context = new VservAccountingDBEntities())
            {
                return null;
                //var result = context.EmployeeAddresses.Where(condition => condition.EmployeeId == employeeId).ToList().Select(ss => ss.AddressId).ToList();
                //return context.Addresses.Where(address => result.Contains(address.AddressId)).ToList();
            }
        }

        /// <summary>
        /// Gets the cities.
        /// </summary>
        /// <returns></returns>
        public List<City> GetCities()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Cities.ToList();
            }
        }

        /// <summary>
        /// Gets the cities.
        /// </summary>
        /// <param name="stateId">The state identifier.</param>
        /// <param name="cityId">The city identifier.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <returns></returns>
        public List<State> GetStates()
        {
            using (var context = new VservAccountingDBEntities())
            {
                var result = context.States.ToList();
                return result;
            }
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <param name="stateId">The state identifier.</param>
        /// <returns></returns>
        public State GetState(int stateId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.States.FirstOrDefault(state => state.StateId == stateId);
            }
        }

        /// <summary>
        /// Gets the zip codes.
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <returns></returns>
        public List<ZipCode> GetZipCodes(int cityId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.ZipCodes.Where(zipcode => zipcode.CityId == cityId).ToList();
            }
        }

        /// <summary>
        /// Adds the address information.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the address types.
        /// </summary>
        /// <returns></returns>
        public List<AddressType> GetAddressTypes()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.AddressTypes.ToList();
            }
        }

        /// <summary>
        /// Gets the departments.
        /// </summary>
        /// <returns></returns>
        public List<Department> GetDepartments()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Departments.ToList();
            }
        }

        /// <summary>
        /// Gets the office branches.
        /// </summary>
        /// <returns></returns>
        public List<OfficeBranch> GetOfficeBranches()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.OfficeBranches.ToList();
            }
        }

        /// <summary>
        /// Gets the salutations.
        /// </summary>
        /// <returns></returns>
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