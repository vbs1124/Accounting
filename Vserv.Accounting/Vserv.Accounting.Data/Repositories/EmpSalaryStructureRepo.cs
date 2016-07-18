#region Namespaces

using System.Collections.Generic;
using System.ComponentModel.Composition;
using Vserv.Accounting.Data.Entity;
using System.Linq;
using Vserv.Accounting.Common;
using System;
using Vserv.Accounting.Data.Entity.Models;
using System.Data.Entity;
using System.Data.SqlClient;
#endregion

namespace Vserv.Accounting.Data.Repositories
{
    [Export(typeof(IEmpSalaryStructureRepo))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmpSalaryStructureRepo : DataRepositoryBase<EmpSalaryStructure>, IEmpSalaryStructureRepo
    {
        /// <summary>
        /// Gets the employee appraisal history.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public List<GetEmpAppraisalHistory_Result> GetEmployeeAppraisalHistory(int employeeId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                var result = context.GetEmpAppraisalHistory(employeeId).ToList();
                return result;
            }
        }

        /// <summary>
        /// Gets the current emp salary structure.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public EmpSalaryStructure GetCurrentEmpSalaryStructure(int employeeId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                var result = context.EmpSalaryStructures.Include("EmpSalaryDetails.SalaryComponent").OrderByDescending(order => order.CreatedDate).FirstOrDefault(structure => structure.EmployeeId == employeeId);
                return result;
            }
        }

        /// <summary>
        /// Inserts the emp salary structure.
        /// </summary>
        /// <param name="empSalaryStructure">The emp salary structure.</param>
        /// <returns></returns>
        public bool InsertEmpSalaryStructure(EmpSalaryStructure empSalaryStructure)
        {
            using (var context = new VservAccountingDBEntities())
            {
                context.EmpSalaryStructures.Add(empSalaryStructure);
                return context.SaveChanges() > 0;
            }
        }

        public List<EmpSalaryCompareResult> GetEmpSalaryStructureComparisonList(int employeeId, int financialYearFrom, Guid uniqueChangeId)
        {
            List<EmpSalaryCompareResult> empSalaryCompareResult = new List<EmpSalaryCompareResult>();

            using (var context = new VservAccountingDBEntities())
            {
                empSalaryCompareResult = context.Database.SqlQuery<EmpSalaryCompareResult>(
                         "[dbo].[GetEmpSalaryStructureComparisonList] @EmployeeId, @UniqueChangeId, @FinancialYearFrom",
                         new SqlParameter("@EmployeeId", employeeId),
                         new SqlParameter("@UniqueChangeId", uniqueChangeId),
                         new SqlParameter("@FinancialYearFrom", financialYearFrom)
                         ).ToList<EmpSalaryCompareResult>();
            }

            return empSalaryCompareResult;
        }
    }
}