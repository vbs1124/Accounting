using System;
using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

namespace Vserv.Accounting.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Vserv.Common.Contracts.IDataRepository{Vserv.Accounting.Data.Entity.Designation}" />
    public interface IDesignationRepository : IDataRepository<Designation>
    {
        #region Methods

        #region Designations

        /// <summary>
        /// Gets the designations.
        /// </summary>
        /// <returns></returns>
        List<Designation> GetDesignations();
        /// <summary>
        /// Adds the designation.
        /// </summary>
        /// <param name="designation">The designation.</param>
        void AddDesignation(Designation designation);
        /// <summary>
        /// Updates the designation.
        /// </summary>
        /// <param name="designation">The designation.</param>
        void UpdateDesignation(Designation designation);
        /// <summary>
        /// Gets the designation.
        /// </summary>
        /// <param name="designationId">The designation identifier.</param>
        /// <returns></returns>
        Designation GetDesignation(int designationId);
        /// <summary>
        /// Determines whether [is designation exists] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="designationId">The designation identifier.</param>
        /// <returns></returns>
        Boolean IsDesignationExists(string name, int designationId);

        #endregion Designations

        #endregion
    }
}
