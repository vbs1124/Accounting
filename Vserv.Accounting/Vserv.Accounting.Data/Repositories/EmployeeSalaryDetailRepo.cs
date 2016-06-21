using System.Collections.Generic;
using System.ComponentModel.Composition;
using Vserv.Accounting.Data.Entity;
using System.Linq;
using Vserv.Accounting.Common;
namespace Vserv.Accounting.Data
{
    [Export(typeof(IEmployeeSalaryDetailRepo))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmployeeSalaryDetailRepo : DataRepositoryBase<EmpSalaryDetail>, IEmployeeSalaryDetailRepo
    {
        public bool ArchiveEmpSalaryDetail(int empSalaryStructureId, string updatedByUserName)
        {
            using (VservAccountingDBEntities context = new VservAccountingDBEntities())
            {
                context.ArchiveEmpSalaryDetail(empSalaryStructureId, updatedByUserName);
            }

            return true;
        }

        public int ResetEmpSalaryStructureId(int updatedEmpSalaryStructureId)
        {
            using (VservAccountingDBEntities context = new VservAccountingDBEntities())
            {
                return context.ResetEmpSalaryStructureId(updatedEmpSalaryStructureId);
            }
        }
    }
}
