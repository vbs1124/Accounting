#region Namespaces

using System.ComponentModel.Composition;
using Vserv.Accounting.Data.Entity;

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
    }
}