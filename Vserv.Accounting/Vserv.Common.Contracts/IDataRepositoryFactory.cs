using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vserv.Common.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataRepositoryFactory
    {
        /// <summary>
        /// Gets the data repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetDataRepository<T>() where T : IDataRepository;
    }
}
