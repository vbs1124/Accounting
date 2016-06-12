#region Namespaces

using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

#endregion

namespace Vserv.Accounting.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IHomeRepository : IDataRepository<UserProfile>
    {
    }
}
