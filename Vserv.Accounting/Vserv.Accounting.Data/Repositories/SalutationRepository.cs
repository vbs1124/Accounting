#region Namespaces

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Vserv.Accounting.Data.Entity;

#endregion

namespace Vserv.Accounting.Data
{
    [Export(typeof(ISalutationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SalutationRepository : DataRepositoryBase<Salutation>, ISalutationRepository
    {
        #region Methods
        /// <summary>
        /// Gets the salutations.
        /// </summary>
        /// <returns></returns>
        public List<Salutation> GetSalutations()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Salutations.AsNoTracking().Where(condition => condition.IsActive).ToList();
            }
        } 
        #endregion
    }
}
