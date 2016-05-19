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
    [Export(typeof(IAddressRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AddressRepository : DataRepositoryBase<Address>, IAddressRepository
    {
        #region Methods
        /// <summary>
        /// Gets the addresses.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public List<Address> GetAddresses(int employeeId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                var result = context.EmployeeAddresses.Where(condition => condition.EmployeeId == employeeId).ToList().Select(ss => ss.AddressId).ToList();
                return context.Addresses.Where(address => result.Contains(address.AddressId)).ToList();
            }
        }

        /// <summary>
        /// Adds the address information.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public Address AddAddressInformation(Address address)
        {
            using (var context = new VservAccountingDBEntities())
            {
                context.Addresses.Add(address);
                context.SaveChanges();
                return address;
            }
        } 
        #endregion
    }
}
