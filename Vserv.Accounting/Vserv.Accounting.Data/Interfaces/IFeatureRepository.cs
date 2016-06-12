#region Namespaces

using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

#endregion

namespace Vserv.Accounting.Data
{
    public interface IFeatureRepository : IDataRepository<Feature>
    {
        List<Feature> GetFeatures(bool excludeInactiveFeatures);
    }
}
