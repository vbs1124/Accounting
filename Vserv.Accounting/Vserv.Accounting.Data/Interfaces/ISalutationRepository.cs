#region Namespaces

using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

#endregion

namespace Vserv.Accounting.Data
{
    public interface ISalutationRepository : IDataRepository<Salutation>
    {
        #region Namespaces
        /// <summary>
        /// Gets the salutations.
        /// </summary>
        /// <returns></returns>
        List<Salutation> GetSalutations(); 
        #endregion
    }
}
