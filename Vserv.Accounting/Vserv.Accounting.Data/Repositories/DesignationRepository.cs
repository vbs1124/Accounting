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

        public List<Designation> GetDesignations()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Designations.ToList();
            }
        }

        public void AddDesignation(Designation designation)
        {
            using (var context = new VservAccountingDBEntities())
            {
                context.Designations.Add(designation);
                context.SaveChanges();
            }
        }

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

        public Designation GetDesignation(int designationId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.Designations.FirstOrDefault(desg => desg.DesignationId == designationId);
            }
        }

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