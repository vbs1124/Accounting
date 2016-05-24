#region Namespaces
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Extensions;
#endregion

namespace Vserv.Accounting.Data
{
    [Export(typeof(IOfficeBranchRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OfficeBranchRepository : DataRepositoryBase<OfficeBranch>, IOfficeBranchRepository
    {
        /// <summary>
        /// Gets the office branches.
        /// </summary>
        /// <returns></returns>
        public List<OfficeBranch> GetOfficeBranches()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.OfficeBranches.Where(condition => condition.IsActive).ToList();
            }
        }
    }
}
