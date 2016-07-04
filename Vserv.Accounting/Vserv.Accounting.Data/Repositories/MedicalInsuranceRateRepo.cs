#region Namespaces

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Common;

#endregion

namespace Vserv.Accounting.Data
{
    [Export(typeof(IMedicalInsuranceRateRepo))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MedicalInsuranceRateRepo : DataRepositoryBase<MedicalInsuranceRate>, IMedicalInsuranceRateRepo
    {
        public decimal? GetCalculatedMediclaimByMonth(EmpSalaryStructure empSalaryStructure)
        {
            using (VservAccountingDBEntities dbEntities = new VservAccountingDBEntities())
            {
                decimal? mediclaimAmount = null;
                MedicalInsuranceRate medicalInsuranceRate = dbEntities.MedicalInsuranceRates
                    .FirstOrDefault(condition => empSalaryStructure.EffectiveFrom >= condition.EffectiveFrom && empSalaryStructure.EffectiveFrom < condition.EffectiveTo
                    && (empSalaryStructure.CTC >= condition.MinCTCAmount || condition.MinCTCAmount == null) && (empSalaryStructure.CTC <= condition.MaxCTCAmount || condition.MaxCTCAmount == null));

                if (medicalInsuranceRate.IsNotNull())
                {
                    mediclaimAmount = medicalInsuranceRate.MediclaimAmount;
                }
                return mediclaimAmount;
            }
        }
    }
}
