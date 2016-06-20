#region Namespaces

using System.Collections.Generic;
using System.ComponentModel.Composition;
using Vserv.Accounting.Data.Entity;
using System.Linq;

#endregion

namespace Vserv.Accounting.Data.Repositories
{
    [Export(typeof(IEmpSalaryStructureRepo))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmpSalaryStructureRepo : DataRepositoryBase<EmpSalaryStructure>, IEmpSalaryStructureRepo
    {
        public EmpSalaryStructure SaveEmpSalaryStructure(EmpSalaryStructure empSalaryStructure)
        {
            using (var context = new VservAccountingDBEntities())
            {
                context.EmpSalaryStructures.Add(empSalaryStructure);
                context.SaveChanges();
                return empSalaryStructure;
            }
        }

        public List<GetEmpAppraisalHistory_Result> GetEmployeeAppraisalHistory(int employeeId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                var result = context.GetEmpAppraisalHistory(employeeId).ToList();
                return result;
            }
        }

        public EmpSalaryStructure GetCurrentEmpSalaryStructure(int employeeId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                var result = context.EmpSalaryStructures.Include("EmpSalaryDetails").OrderByDescending(order => order.CreatedDate).FirstOrDefault(structure => structure.EmployeeId == employeeId);
                return result;
            }
        }
    }
}