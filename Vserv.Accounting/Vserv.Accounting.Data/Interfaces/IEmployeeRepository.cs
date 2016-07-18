#region Namespaces

using System;
using System.Collections.Generic;
using Vserv.Accounting.Common;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Data.Entity.Models;
using Vserv.Common.Contracts;

#endregion

namespace Vserv.Accounting.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEmployeeRepository : IDataRepository<Employee>
    {
        #region Methods

        List<Employee> GetEmployees(EmployeeFilter employeeFilter);
        Employee GetEmployee(int employeeId);
        Employee AddEmployee(Employee employee);
        Employee EditEmployee(Employee employee);
        void DeleteEmployee(int employeeId);
        Boolean IsEmployeeIdAlreadyRegistered(string VBS_Id, int employeeId);
        Boolean IsEmailAlreadyRegistered(string emailAddress, int employeeId);
        Boolean IsMobileNumberAlreadyRegistered(string mobileNumber, int employeeId);
        int GetEmployeeCount();
        void ArchiveEmployee(int employeeId, string updatedByUserName);
        List<EmployeeArchive> GetEmployeeHistory(int employeeId);
        CompareEmployeeModel GetMatchingEmployeeInformation(int employeeArchiveId);
        List<GetEmployeeSalaryDetail_Result> GetYearlyPaySheet(int? employeeId, int? financialYearFrom, int? financialYearTo);
        EmployeeTaxation GetEmployeeIncomeTaxComputation(int employeeId, int financialYear);

        #endregion
    }
}
