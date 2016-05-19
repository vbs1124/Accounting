#region Namespaces
using System;
using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;
#endregion

namespace Vserv.Accounting.Data
{
    public interface IAddressRepository : IDataRepository<Address>
    {
        #region Methods
        /// <summary>
        /// Gets the addresses.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        List<Address> GetAddresses(int employeeId);

        /// <summary>
        /// Adds the address information.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        Address AddAddressInformation(Address address); 
        #endregion
    }
}
