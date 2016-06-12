#region Namespaces

using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

#endregion

namespace Vserv.Accounting.Data
{
    public interface IEmpSalaryStructureRepo : IDataRepository<EmpSalaryStructure>
    {
        EmpSalaryStructure SaveEmpSalaryStructure(EmpSalaryStructure empSalaryStructure);
    }
}
