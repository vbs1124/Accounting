using System.ComponentModel.Composition;
using Vserv.Accounting.Data.Entity;

namespace Vserv.Accounting.Data
{
    [Export(typeof(IEmployeeSalaryDetailRepo))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmployeeSalaryDetailRepo : DataRepositoryBase<EmpSalaryDetail>, IEmployeeSalaryDetailRepo
    {
        public bool ArchiveEmpSalaryDetail(int empSalaryDetailId, string updatedByUserName)
        {
            using (VservAccountingDBEntities context = new VservAccountingDBEntities())
            {
                context.ArchiveEmpSalaryDetail(empSalaryDetailId, updatedByUserName);
            }

            return true;
        }
    }
}
