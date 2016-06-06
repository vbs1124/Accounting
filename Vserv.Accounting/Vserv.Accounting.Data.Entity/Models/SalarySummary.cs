using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vserv.Accounting.Data.Entity
{
    public class SalarySummary
    {
        public Decimal? CTC { get; set; }
        public Decimal? CabDeductions { get; set; }
        public Decimal? ProjectIncentive { get; set; }
        public Decimal? CarLease { get; set; }
        public Decimal? FoodCoupons { get; set; }
        public Decimal? PerformanceIncentive { get; set; }
        public int Year { get; set; }
        public int EmployeeId { get; set; }
        public string UserName { get; set; }
    }
}
