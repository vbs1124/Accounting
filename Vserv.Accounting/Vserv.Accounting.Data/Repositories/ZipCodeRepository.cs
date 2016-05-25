﻿#region Namespaces
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Extensions;
#endregion

namespace Vserv.Accounting.Data
{
    [Export(typeof(IZipCodeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ZipCodeRepository : DataRepositoryBase<ZipCode>, IZipCodeRepository
    {
        #region Methods
        /// <summary>
        /// Gets the zip codes.
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <returns></returns>
        public List<ZipCode> GetZipCodes(int cityId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.ZipCodes.Where(zipcode => zipcode.CityId == cityId).ToList();
            }
        } 
        #endregion
    }
}