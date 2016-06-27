using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

namespace Vserv.Accounting.Data
{
    public interface IEmployeeSalaryDetailRepo : IDataRepository<EmpSalaryDetail>
    {
        bool ArchiveEmpSalaryDetail(int empSalaryStructureId, string updatedByUserName);
        int ResetEmpSalaryStructureId(int updatedEmpSalaryStructureId);
        List<EmpSalaryDetailArchive> GetSalaryStructureChangeHistory(int empSalaryStructureId);
    }
}
