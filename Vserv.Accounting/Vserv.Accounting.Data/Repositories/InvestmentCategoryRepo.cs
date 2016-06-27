#region Namespaces

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Vserv.Accounting.Common;
using Vserv.Accounting.Data.Entity;
using System.Linq;
#endregion

namespace Vserv.Accounting.Data
{
    [Export(typeof(IInvestmentCategoryRepo))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InvestmentCategoryRepo : DataRepositoryBase<InvestmentCategory>, IInvestmentCategoryRepo
    {
        public List<InvestmentCategory> GetInvestmentCatogories(int financialYear)
        {
            using (VservAccountingDBEntities context = new VservAccountingDBEntities())
            {
                InvtDeclarationComponent invtDeclarationComponent = context.InvtDeclarationComponents.FirstOrDefault(mapping => mapping.Year == financialYear);

                if (invtDeclarationComponent.IsNotNull())
                {
                    return context.InvestmentCategories.Include("InvestmentSubCategories").Where(condition => condition.MappingId == invtDeclarationComponent.MappingId).ToList();
                }

                return new List<InvestmentCategory>(); // return blank list.
            }
        }
    }
}
