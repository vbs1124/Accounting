using System.ComponentModel.Composition;
using Vserv.Accounting.Data.Entity;

namespace Vserv.Accounting.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Vserv.Accounting.Data.IHomeRepository" />
    [Export(typeof(IHomeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeRepository : DataRepositoryBase<UserProfile>, IHomeRepository
    {
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            base.Dispose(true);
        }
    }
}