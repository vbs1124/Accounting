#region Namespaces

using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

#endregion

namespace Vserv.Accounting.Data
{
    public interface ICityRepository : IDataRepository<City>
    {
        #region Methods
        /// <summary>
        /// Gets the cities.
        /// </summary>
        /// <returns></returns>
        List<City> GetCities();

        /// <summary>
        /// Gets the cities.
        /// </summary>
        /// <param name="stateId">The state identifier.</param>
        /// <param name="cityId">The city identifier.</param>
        /// <returns></returns>
        List<City> GetCities(int stateId, int? cityId); 
        #endregion
    }
}
