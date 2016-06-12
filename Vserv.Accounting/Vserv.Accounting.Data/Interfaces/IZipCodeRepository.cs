#region Namespaces

using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

#endregion

namespace Vserv.Accounting.Data
{
    public interface IZipCodeRepository : IDataRepository<ZipCode>
    {
        #region Namespaces
        /// <summary>
        /// Gets the zip codes.
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <returns></returns>
        List<ZipCode> GetZipCodes(int cityId); 
        #endregion
    }
}
