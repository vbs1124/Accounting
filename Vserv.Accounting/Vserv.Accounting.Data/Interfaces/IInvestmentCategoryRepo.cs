#region Namespaces

using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;

#endregion

namespace Vserv.Accounting.Data
{
    public interface IInvestmentCategoryRepo : IDataRepository<InvestmentCategory>
    {
        List<InvestmentCategory> GetInvestmentCatogories(int financialYear);
        bool SaveEmployeeInvestments(List<EmpInvestment> investmentCatogories);
        List<InvestmentCategory> GetInvestmentCategories(int financialYear, int EmployeeId);
        List<EmpInvestment> GetEmpInvestmentByEmpId(int employeeId);
    }
}
