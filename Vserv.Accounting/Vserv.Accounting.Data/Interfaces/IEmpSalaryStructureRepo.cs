#region Namespaces

using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

#endregion

namespace Vserv.Accounting.Data
{
    public interface IEmpSalaryStructureRepo : IDataRepository<EmpSalaryStructure>
    {
        List<GetEmpAppraisalHistory_Result> GetEmployeeAppraisalHistory(int employeeId);
        EmpSalaryStructure GetCurrentEmpSalaryStructure(int employeeId);
        bool InsertEmpSalaryStructure(EmpSalaryStructure empSalaryStructure);
    }
}
