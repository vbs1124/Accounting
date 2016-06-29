#region Namespaces

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Common.Enums;
using System;

#endregion

namespace Vserv.Accounting.Data.Repositories
{
    [Export(typeof(IEmployeePaySlipRepo))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmployeePaySlipRepo : DataRepositoryBase<EmployeePaySlip>, IEmployeePaySlipRepo
    {
        public EmployeePaySlip GetEmployeePaySlip(int employeeId, int monthId, int year)
        {

            MonthEnum month = (MonthEnum)Enum.Parse(typeof(MonthEnum), Convert.ToString(monthId));
            string currentMonth = month.ToString();
            EmployeePaySlip employeePaySlip = new EmployeePaySlip();

            using (VservAccountingDBEntities dbEntities = new VservAccountingDBEntities())
            {
                employeePaySlip = dbEntities.EmployeePaySlips.FirstOrDefault(condition => condition.IsActive &&
                    condition.IsApproved && condition.PaySlipMonth == month.ToString() &&
                    condition.EmployeeId == employeeId && condition.PaySlipYear == year);
            }

            return employeePaySlip;
        }
    }
}
