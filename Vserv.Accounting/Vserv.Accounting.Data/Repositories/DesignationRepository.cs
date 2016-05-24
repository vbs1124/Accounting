#region Namespaces
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Extensions;
#endregion

namespace Vserv.Accounting.Data
{
    [Export(typeof(IDesignationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DesignationRepository : DataRepositoryBase<Designation>, IDesignationRepository
    {
        #region Methods

        #region Public Methods

        #region Designations

        /// <summary>
        /// Gets the designations.
        /// </summary>
        /// <returns></returns>
        public List<Designation> GetDesignations()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Designations.Where(condition => condition.IsActive).ToList();
            }
        }

        /// <summary>
        /// Adds the designation.
        /// </summary>
        /// <param name="designation">The designation.</param>
        public void AddDesignation(Designation designation)
        {
            using (var context = new VservAccountingDBEntities())
            {
                context.Designations.Add(designation);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the designation.
        /// </summary>
        /// <param name="designation">The designation.</param>
        public void UpdateDesignation(Designation designation)
        {
            using (var context = new VservAccountingDBEntities())
            {
                Designation existingDesignation = context.Designations.FirstOrDefault(desg => desg.DesignationId == designation.DesignationId);
                if (existingDesignation.IsNotNull())
                {
                    context.Entry(existingDesignation).CurrentValues.SetValues(designation);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Gets the designation.
        /// </summary>
        /// <param name="designationId">The designation identifier.</param>
        /// <returns></returns>
        public Designation GetDesignation(int designationId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Designations.FirstOrDefault(desg => desg.DesignationId == designationId);
            }
        }

        /// <summary>
        /// Determines whether [is designation exists] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="designationId">The designation identifier.</param>
        /// <returns></returns>
        public Boolean IsDesignationExists(string name, int designationId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Designations.Any(desg => desg.DesignationId != designationId && desg.Name == name);
            }
        }

        #endregion Designations

        #endregion Public Methods

        #region Private Methods

        #endregion Private Methods

        #endregion
    }
}