using System.ComponentModel.Composition;
using System.Data.SqlClient;
using Vserv.Accounting.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Vserv.Accounting.Data
{
    [Export(typeof(IEmployeeSalaryDetailRepo))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmployeeSalaryDetailRepo : DataRepositoryBase<EmpSalaryDetail>, IEmployeeSalaryDetailRepo
    {
        /// <summary>
        /// Archives the emp salary detail.
        /// </summary>
        /// <param name="empSalaryStructureId">The emp salary structure identifier.</param>
        /// <param name="updatedByUserName">Name of the updated by user.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Resets the emp salary structure identifier.
        /// </summary>
        /// <param name="updatedEmpSalaryStructureId">The updated emp salary structure identifier.</param>
        /// <returns></returns>
        public int ResetEmpSalaryStructureId(int updatedEmpSalaryStructureId)
        {
            using (VservAccountingDBEntities context = new VservAccountingDBEntities())
            {
                return context.ResetEmpSalaryStructureId(updatedEmpSalaryStructureId);
            }
        }

        public List<EmpSalaryDetailArchive> GetSalaryStructureChangeHistory(int empSalaryStructureId)
        {
            using (VservAccountingDBEntities dbEntities = new VservAccountingDBEntities())
            {
                List<EmpSalaryDetailArchive> result = dbEntities.EmpSalaryDetailArchives
                    .Where(condition => condition.EmpSalaryStructureId == empSalaryStructureId).ToList();
                return result;
            }
        }
    }
}
