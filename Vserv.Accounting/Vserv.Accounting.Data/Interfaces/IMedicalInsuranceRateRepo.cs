﻿#region Namespaces

using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

#endregion

namespace Vserv.Accounting.Data
{
    public interface IMedicalInsuranceRateRepo : IDataRepository<MedicalInsuranceRate>
    {
        decimal? GetCalculatedMediclaimByMonth(EmpSalaryStructure empSalaryStructure);
    }
}