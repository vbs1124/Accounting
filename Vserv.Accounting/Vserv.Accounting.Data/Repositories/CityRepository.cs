#region Namespaces

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Common;

#endregion

namespace Vserv.Accounting.Data
{
    [Export(typeof(ICityRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CityRepository : DataRepositoryBase<City>, ICityRepository
    {
        /// <summary>
        /// Gets the cities.
        /// </summary>
        /// <returns></returns>
        public List<City> GetCities()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Cities.AsNoTracking().ToList();
            }
        }

        /// <summary>
        /// Gets the cities.
        /// </summary>
        /// <param name="stateId">The state identifier.</param>
        /// <param name="cityId">The city identifier.</param>
        /// <returns></returns>
        public List<City> GetCities(int stateId, int? cityId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                if (cityId.IsNotNull() && cityId.HasValue)
                {
                    return context.Cities.AsNoTracking().Where(city => city.CityId == cityId.Value).ToList();
                }

                return context.Cities.AsNoTracking().Where(city => city.StateId == stateId).ToList();
            }
        }
    }
}
