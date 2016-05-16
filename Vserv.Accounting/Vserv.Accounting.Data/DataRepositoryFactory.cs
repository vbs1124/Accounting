using System.ComponentModel.Composition;
using Vserv.Common;
using Vserv.Common.Contracts;

namespace Vserv.Accounting.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Vserv.Common.Contracts.IDataRepositoryFactory" />
    [Export(typeof(IDataRepositoryFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        /// <summary>
        /// Gets the data repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetDataRepository<T>() where T : IDataRepository
        {
            return DependencyHelper.Container.GetExportedValue<T>();
        }
    }
}
