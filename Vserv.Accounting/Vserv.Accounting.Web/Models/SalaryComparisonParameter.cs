using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vserv.Accounting.Web.Models
{
    public class SalaryComparisonParameter
    {
        public int EmployeeId { get; set; }
        public int FinancialYearFrom { get; set; }
        public Guid UniqueChangeId { get; set; }
    }
}