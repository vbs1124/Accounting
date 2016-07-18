using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vserv.Accounting.Data.Entity.Models
{
    public class EmployeeTaxation
    {
        public List<GetMonthlyTaxDeduction_Result> MonthlyTaxDeductions { get; set; }
        public List<TaxComputationComponent> YearlyTaxExemptions { get; set; }
        public List<TaxComputationComponent> YearlyContributionUnderChapterVIA { get; set; }
    }
}
