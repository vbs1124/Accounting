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

        /// <summary>
        /// Retrieves the Employee count.
        /// </summary>
        /// <returns></returns>
        public int GetEmployeeCount()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Employees.Count();
            }
        }

        #endregion Public Methods

        #region Private Methods

        #endregion Private Methods

        #endregion
    }
}