using System.ComponentModel.Composition;
using Vserv.Accounting.Data.Entity;

namespace Vserv.Accounting.Data
{
    [Export(typeof(ISalaryComponentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SalaryComponentRepository : DataRepositoryBase<SalaryComponent>, ISalaryComponentRepository
    {
    }
}
