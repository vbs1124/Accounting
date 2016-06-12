using System;

namespace Vserv.Accounting.Web.Models
{
    public class CalculateBreakupModel
    {
        public Decimal CTC
        {
            get;
            set;
        }

        public Decimal CabDeductions
        {
            get;
            set;
        }

        public Decimal FoodCoupons
        {
            get;
            set;
        }

        public Decimal ProjectIncentive
        {
            get;
            set;
        }

        public Decimal CarLease
        {
            get;
            set;
        }
    }
}