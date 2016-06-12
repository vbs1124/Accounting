using System.ComponentModel.Composition;
using Vserv.Accounting.Data.Entity;

namespace Vserv.Accounting.Data
{
    [Export(typeof(IEmployeeSalaryDetailRepo))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmployeeSalaryDetailRepo : DataRepositoryBase<EmployeeSalaryDetail>, IEmployeeSalaryDetailRepo
    {
    }
}
