#region Namespaces
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Extensions;
#endregion

namespace Vserv.Accounting.Data.Repositories
{
    [Export(typeof(IAddressTypeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AddressTypeRepository : DataRepositoryBase<AddressType>, IAddressTypeRepository
    {
        /// <summary>
        /// Gets the address types.
        /// </summary>
        /// <returns></returns>
        public List<AddressType> GetAddressTypes()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.AddressTypes.ToList();
            }
        }
    }
}
