using Vserv.Accounting.Data.Entity;

namespace Vserv.Accounting.Data
{
    public class HomeRepository : DataRepositoryBase<UserProfile>, IHomeRepository
    {
        public void Dispose()
        {
            base.Dispose(true);
        }
    }
}