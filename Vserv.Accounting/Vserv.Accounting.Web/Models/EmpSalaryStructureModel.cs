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

        [Display(Name = "Cab Deductions (monthly)")]
        [MaxLength(20)]
        [DataType(DataType.Currency)]
        public string CabDeductions { get; set; }

        [Display(Name = "Project Incentive (monthly)")]
        [MaxLength(20)]
        [DataType(DataType.Currency)]
        public string ProjectIncentive { get; set; }

        [Display(Name = "Car Lease (monthly)")]
        [MaxLength(20)]
        [DataType(DataType.Currency)]
        public string CarLease { get; set; }

        [Display(Name = "Food Coupons (monthly)")]
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