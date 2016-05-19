#region Namespaces
using System;
using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;
#endregion

namespace Vserv.Accounting.Data
{
    public interface IAddressTypeRepository : IDataRepository<AddressType>
    {
        #region Methods
        /// <summary>
        /// Gets the address types.
        /// </summary>
        /// <returns></returns>
        List<AddressType> GetAddressTypes(); 
        #endregion
    }
}
