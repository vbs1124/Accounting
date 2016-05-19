#region Namespaces
using System;
using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;
#endregion

namespace Vserv.Accounting.Data
{
   public interface IOfficeBranchRepository : IDataRepository<OfficeBranch>
    {
        #region Methods
        /// <summary>
        /// Gets the office branches.
        /// </summary>
        /// <returns></returns>
        List<OfficeBranch> GetOfficeBranches(); 
        #endregion
    }
}
