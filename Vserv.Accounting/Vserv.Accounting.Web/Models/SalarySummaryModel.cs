using Heroic.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vserv.Accounting.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Vserv.Accounting.Web.Models
{
    public class SalarySummaryModel : IMapFrom<SalarySummary>
    {
        public string CTC { get; set; }
        public string CabDeductions { get; set; }
        public string ProjectIncentive { get; set; }
        public string CarLease { get; set; }
        public string FoodCoupons { get; set; }
        public string PerformanceIncentive { get; set; }
        public string Year { get; set; }
        public string EmployeeId { get; set; }
        public string UserName { get; set; }

        //[Display(Name = "CTC")]
        //[MaxLength(20)]
        //[Required(ErrorMessage = "CTC is required.")]
        //public Decimal? CTC { get; set; }

        //[Display(Name = "Cab Deductions")]
        //[MaxLength(20)]
        //[Required(ErrorMessage = "Cab Deductions is required.")]
        //public Decimal? CabDeductions { get; set; }

        //[Display(Name = "Project Incentive")]
        //[MaxLength(20)]
        //[Required(ErrorMessage = "Project Incentive is required.")]
        //public Decimal? ProjectIncentive { get; set; }

        //[Display(Name = "Car Lease")]
        //[MaxLength(20)]
        //[Required(ErrorMessage = "Car Lease is required.")]
        //public Decimal? CarLease { get; set; }

        //[Display(Name = "Food Coupons")]
        //[MaxLength(20)]
        //[Required(ErrorMessage = "Food Coupons is required.")]
        //public Decimal? FoodCoupons { get; set; }

        //[Display(Name = "Performance Incentive")]
        //[MaxLength(20)]
        //[Required(ErrorMessage = "Performance Incentive is required.")]
        //public Decimal? PerformanceIncentive { get; set; }

        //[Display(Name = "Year")]
        //[MaxLength(20)]
        //[Required(ErrorMessage = "Year is required.")]
        //public int Year { get; set; }

        //[Display(Name = "Employee Id")]
        //[MaxLength(20)]
        //[Required(ErrorMessage = "Employee Id is required.")]
        //public int EmployeeId { get; set; }

        //[Display(Name = "User Name")]
        //[MaxLength(20)]
        //[Required(ErrorMessage = "User Name is required.")]
        //public string UserName { get; set; }
    }
}