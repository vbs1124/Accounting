using System.ComponentModel.DataAnnotations;
using Heroic.AutoMapper;
using Vserv.Accounting.Data.Entity;

namespace Vserv.Accounting.Web.Models
{
    public class EmpSalaryStructureModel : IMapFrom<EmpSalaryStructure>
    {
        [Display(Name = "Cost to company(CTC)")]
        [MaxLength(20)]
        [Required(ErrorMessage = "Cost to company(CTC) is required.")]
        [DataType(DataType.Currency)]
        public string CTC { get; set; }

        [Display(Name = "Performance Incentive Payable")]
        [MaxLength(20)]
        [Required(ErrorMessage = "Performance Incentive Payable is required.")]
        public string PerformanceIncentivePayable { get; set; }

        [Display(Name = "Cab Deductions (Monthly)")]
        [MaxLength(20)]
        [DataType(DataType.Currency)]
        public string CabDeductions { get; set; }

        [Display(Name = "Project Incentive (Monthly)")]
        [MaxLength(20)]
        [DataType(DataType.Currency)]
        public string ProjectIncentive { get; set; }

        [Display(Name = "Car Lease (Monthly)")]
        [MaxLength(20)]
        [DataType(DataType.Currency)]
        public string CarLease { get; set; }

        [Display(Name = "Food Coupons (Monthly)")]
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