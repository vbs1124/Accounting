#region Namespaces

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Vserv.Accounting.Common;
using Vserv.Accounting.Data.Entity;

#endregion

namespace Vserv.Accounting.Data
{
    [Export(typeof(IStateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StateRepository : DataRepositoryBase<State>, IStateRepository
    {
        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <returns></returns>
        public List<State> GetStates()
        {
            using (var context = new VservAccountingDBEntities())
            {
                var result = context.States.AsNoTracking().Where(condition => condition.IsActive).ToList();
                return result;
            }
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <param name="stateId">The state identifier.</param>
        /// <returns></returns>
        public State GetState(int stateId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.States.AsNoTracking().FirstOrDefault(state => state.StateId == stateId);
            }
        }

        public State GetStateByCityName(string cityName)
        {
            using (var context = new VservAccountingDBEntities())
            {
                var city = context.Cities.AsNoTracking().Include("State").FirstOrDefault(condition => condition.Name == cityName);

                if (city.IsNotNull())
                {
                    return city.State;
                }

                return null;
            }
        }
    }
}
