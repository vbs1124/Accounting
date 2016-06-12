using System.ComponentModel.DataAnnotations;
using Heroic.AutoMapper;
using Vserv.Accounting.Data.Entity;

namespace Vserv.Accounting.Web.Models
{
    public class SalarySummaryModel : IMapFrom<EmpSalaryStructure>
    {
        //public string CTC { get; set; }
        //public string CabDeductions { get; set; }
        //public string ProjectIncentive { get; set; }
        //public string CarLease { get; set; }
        //public string FoodCoupons { get; set; }
        //public string PerformanceIncentive { get; set; }
        //public string EffectiveFrom { get; set; }
        //public string EmployeeId { get; set; }
        //public string UserName { get; set; }

        [Display(Name = "Cost to company(CTC)")]
        [MaxLength(20)]
        [Required(ErrorMessage = "Cost to company(CTC) is required.")]
        [DataType(DataType.Currency)]
        public string CTC { get; set; }

        [Display(Name = "Cab Deductions")]
        [MaxLength(20)]
        [DataType(DataType.Currency)]
        public string CabDeductions { get; set; }

        [Display(Name = "Project Incentive")]
        [MaxLength(20)]
        [DataType(DataType.Currency)]
        public string ProjectIncentive { get; set; }

        [Display(Name = "Car Lease")]
        [MaxLength(20)]
        [DataType(DataType.Currency)]
        public string CarLease { get; set; }

        [Display(Name = "Food Coupons")]
        [MaxLength(20)]
        [DataType(DataType.Currency)]
        public string FoodCoupons { get; set; }

        [Display(Name = "Effective From")]
        [MaxLength(20)]
        [Required(ErrorMessage = "Effective From is required.")]
        [DataType(DataType.DateTime)]
        public string EffectiveFrom { get; set; }
    }
}