#region Namespaces

using System;
using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Data.Entity.Models;
using Vserv.Common.Contracts;

#endregion

namespace Vserv.Accounting.Data
{
    public interface IEmployeePaySlipRepo : IDataRepository<EmployeePaySlip>
    {
        EmployeePaySlip GetEmployeePaySlip(int employeeId, int monthId, int year);
    }
}
