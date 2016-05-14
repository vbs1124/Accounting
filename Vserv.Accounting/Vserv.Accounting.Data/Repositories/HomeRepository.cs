using System.ComponentModel.Composition;
using Vserv.Accounting.Data.Entity;

namespace Vserv.Accounting.Data
{
    [Export(typeof(IHomeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeRepository : DataRepositoryBase<UserProfile>, IHomeRepository
    {
        public void Dispose()
        {
            base.Dispose(true);
        }
    }
}