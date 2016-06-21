#region Namespaces

using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

#endregion

namespace Vserv.Accounting.Data
{
    public interface IEmpSalaryStructureRepo : IDataRepository<EmpSalaryStructure>
    {
        EmpSalaryStructure SaveEmpSalaryStructure(EmpSalaryStructure empSalaryStructure);
        List<GetEmpAppraisalHistory_Result> GetEmployeeAppraisalHistory(int employeeId);
        EmpSalaryStructure GetCurrentEmpSalaryStructure(int employeeId);
        bool SaveEmpSalaryStructure(EmpSalaryStructure empSalaryStructure, List<EmpSalaryDetail> employeeSalaryDetails, string userName);
        bool InsertEmpSalaryStructure(EmpSalaryStructure empSalaryStructure);
    }
}
