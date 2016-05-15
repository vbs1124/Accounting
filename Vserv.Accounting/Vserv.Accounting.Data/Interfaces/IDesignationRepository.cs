using System;
using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

namespace Vserv.Accounting.Data
{
    public interface IDesignationRepository : IDataRepository<Designation>
    {
        #region Methods

        #region Designations

        List<Designation> GetDesignations();
        void AddDesignation(Designation designation);
        void UpdateDesignation(Designation designation);
        Designation GetDesignation(int designationId);
        Boolean IsDesignationExists(string name, int designationId);

        #endregion Designations

        #endregion
    }
}
