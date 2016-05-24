#region Namespaces
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Extensions;
#endregion

namespace Vserv.Accounting.Data
{
    [Export(typeof(IFeatureRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FeatureRepository : DataRepositoryBase<Feature>, IFeatureRepository
    {
        public List<Feature> GetFeatures(bool excludeInactiveFeatures)
        {
            using (var context = new VservAccountingDBEntities())
            {
                if (excludeInactiveFeatures)
                {
                    return context.Features.Where(feature => feature.IsActive).ToList();
                }

                return context.Features.ToList();
            }
        }
    }
}
