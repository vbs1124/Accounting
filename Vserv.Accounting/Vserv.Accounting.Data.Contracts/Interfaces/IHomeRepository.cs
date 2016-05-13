#region Namespaces
using System;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts; 
#endregion

namespace Vserv.Accounting.Data.Contracts
{
    public interface IHomeRepository : IDataRepository<UserProfile>, IDisposable
    {
    }
}
