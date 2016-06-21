using System.Collections.Generic;
using System.ComponentModel.Composition;
using Vserv.Accounting.Data.Entity;
using System.Linq;
using Vserv.Accounting.Common;
using System.Data.SqlClient;
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
                //var sqlQuery = string.Format("EXECUTE [dbo].[ArchiveEmpSalaryDetail] {0}, {1}", "@EmpSalaryStructureId", "@UpdatedByUserName");
                //var results = context.Database.SqlQuery<Employee>(sqlQuery, new SqlParameter("EmpSalaryStructureId", empSalaryStructureId), new SqlParameter("UpdatedByUserName", updatedByUserName));

                context.Database.ExecuteSqlCommand("EXECUTE [dbo].[ArchiveEmpSalaryDetail] @EmpSalaryStructureId, @UpdatedByUserName",
                          new SqlParameter("@EmpSalaryStructureId", empSalaryStructureId),
                          new SqlParameter("@UpdatedByUserName", updatedByUserName));
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
