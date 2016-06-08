using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vserv.Accounting.Data.Entity.Models
{
    public class SalaryComponentAmountMapper
    {
        public decimal? CTCPerMonth { get; set; }
        public decimal? Basic { get; set; }
        public decimal? HRA { get; set; }
        public decimal? Conveyance { get; set; }
        public decimal? SpecialAllowance { get; set; }
        public decimal? PerformanceIncentive { get; set; }
        public decimal? LeaveEncashment { get; set; }
        public decimal? SalaryArrears { get; set; }
        public decimal? CabDeductions { get; set; }
        public decimal? OtherDeduction { get; set; }
        public decimal? Commission { get; set; }
        public decimal? Others { get; set; }
        public decimal? Medical { get; set; }
        public decimal? FoodCoupons { get; set; }
        public decimal? ProjectIncentive { get; set; }
        public decimal? CarLease { get; set; }
        public decimal? LTC { get; set; }
        public decimal? PF { get; set; }
        public decimal? Mediclaim { get; set; }
        public decimal? Gratuity { get; set; }
    }
}
