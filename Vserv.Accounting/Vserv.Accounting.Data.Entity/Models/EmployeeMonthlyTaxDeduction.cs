using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vserv.Accounting.Data.Entity.Models
{
    public class EmployeeMonthlyTaxDeduction
    {
        public Nullable<int> MonthId { get; set; }
        public string MonthName { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<decimal> TaxDeducted { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> Surcharge { get; set; }
        public Nullable<decimal> EduacationCess { get; set; }
    }
}
