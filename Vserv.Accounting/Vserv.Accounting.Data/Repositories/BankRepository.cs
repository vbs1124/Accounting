#region Namespaces

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Vserv.Accounting.Data.Entity;

#endregion

namespace Vserv.Accounting.Data
{
    [Export(typeof(IBankRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BankRepository : DataRepositoryBase<Bank>, IBankRepository
    {
        /// <summary>
        /// Gets the cities.
        /// </summary>
        /// <returns></returns>
        public List<Bank> GetBanks()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Banks.AsNoTracking().Where(condition => condition.IsActive).OrderBy(order => order.DisplayOrder).ToList();
            }
        }
    }
}
