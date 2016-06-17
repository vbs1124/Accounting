using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

namespace Vserv.Accounting.Data
{
    public interface IEmployeeSalaryDetailRepo : IDataRepository<EmpSalaryDetail>
    {
        bool ArchiveEmpSalaryDetail(int empSalaryDetailId, string updatedByUserName);
    }
}
