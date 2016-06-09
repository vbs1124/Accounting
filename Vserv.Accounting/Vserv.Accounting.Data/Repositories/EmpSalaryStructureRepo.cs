#region Namespaces

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Common;

#endregion

namespace Vserv.Accounting.Data.Repositories
{
    [Export(typeof(IEmpSalaryStructureRepo))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmpSalaryStructureRepo : DataRepositoryBase<EmpSalaryStructure>, IEmpSalaryStructureRepo
    {
        public EmpSalaryStructure SaveEmpSalaryStructure(EmpSalaryStructure empSalaryStructure)
        {
            try
            {
                using (var context = new VservAccountingDBEntities())
                {
                    context.EmpSalaryStructures.Add(empSalaryStructure);
                    context.SaveChanges();
                    return empSalaryStructure;
                }
            }
            catch
            {

                throw;
            }
        }
    }
}