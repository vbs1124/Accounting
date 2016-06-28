#region Namespaces

using System;
using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Data.Entity.Models;
using Vserv.Common.Contracts;

#endregion

namespace Vserv.Accounting.Data
{
    public interface IEmpSalaryStructureRepo : IDataRepository<EmpSalaryStructure>
    {
        List<GetEmpAppraisalHistory_Result> GetEmployeeAppraisalHistory(int employeeId);
        EmpSalaryStructure GetCurrentEmpSalaryStructure(int employeeId);
        bool InsertEmpSalaryStructure(EmpSalaryStructure empSalaryStructure);
        List<EmpSalaryCompareResult> GetEmpSalaryStructureComparisonList(int employeeId, int financialYearFrom, Guid uniqueChangeId);
    }
}
