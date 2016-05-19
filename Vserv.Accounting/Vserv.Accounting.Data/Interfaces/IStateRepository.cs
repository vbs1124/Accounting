#region Namespaces
using System;
using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts; 
#endregion

namespace Vserv.Accounting.Data
{
    public interface IStateRepository : IDataRepository<State>
    {
        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <returns></returns>
        List<State> GetStates();

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <param name="stateId">The state identifier.</param>
        /// <returns></returns>
        State GetState(int stateId);
    }
}
