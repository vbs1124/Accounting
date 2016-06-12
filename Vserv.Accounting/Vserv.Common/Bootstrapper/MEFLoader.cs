using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;

namespace Vserv.Common.Bootstrapper
{
    /// <summary>
    /// 
    /// </summary>
    public static class MEFLoader
    {
        /// <summary>
        /// Initializes the specified catalog parts.
        /// </summary>
        /// <param name="catalogParts">The catalog parts.</param>
        /// <returns></returns>
        public static CompositionContainer Initialize(ICollection<ComposablePartCatalog> catalogParts)
        {
            AggregateCatalog catalog = new AggregateCatalog();

            if (catalogParts != null)
            {
                foreach (var part in catalogParts)
                {
                    catalog.Catalogs.Add(part);
                }
            }

            CompositionContainer container = new CompositionContainer(catalog, true);

            return container;

        }
    }
}
