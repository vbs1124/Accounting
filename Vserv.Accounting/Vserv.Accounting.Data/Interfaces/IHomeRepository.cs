#region Namespaces
using System;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts; 
#endregion

namespace Vserv.Accounting.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Vserv.Common.Contracts.IDataRepository{Vserv.Accounting.Data.Entity.UserProfile}" />
    /// <seealso cref="System.IDisposable" />
    public interface IHomeRepository : IDataRepository<UserProfile>, IDisposable
    {
    }
}
