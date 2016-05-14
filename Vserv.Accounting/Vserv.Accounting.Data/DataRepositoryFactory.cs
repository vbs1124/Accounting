using System.ComponentModel.Composition;
using Vserv.Common;
using Vserv.Common.Contracts;

namespace Vserv.Accounting.Data
{
    [Export(typeof(IDataRepositoryFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        public T GetDataRepository<T>() where T : IDataRepository
        {
            return DependencyHelper.Container.GetExportedValue<T>();
        }
    }
}
