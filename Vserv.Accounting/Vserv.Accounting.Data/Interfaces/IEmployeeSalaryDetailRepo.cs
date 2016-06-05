using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

namespace Vserv.Accounting.Data
{
    public interface IEmployeeSalaryDetailRepo : IDataRepository<EmployeeSalaryDetail>
    {
    }
}
