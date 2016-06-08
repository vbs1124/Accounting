using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vserv.Accounting.Data.Entity;

namespace Vserv.Accounting.Data
{
    [Export(typeof(ISalaryComponentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SalaryComponentRepository : DataRepositoryBase<SalaryComponent>, ISalaryComponentRepository
    {
    }
}
