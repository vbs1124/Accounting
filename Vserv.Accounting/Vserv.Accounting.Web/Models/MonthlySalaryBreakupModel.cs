using System;

namespace Vserv.Accounting.Web.Models
{
    public class MonthlySalaryBreakupModel
    {
        public PaySheet April { get; set; }
        public PaySheet May { get; set; }
        public PaySheet June { get; set; }
        public PaySheet July { get; set; }
        public PaySheet August { get; set; }
        public PaySheet September { get; set; }
        public PaySheet October { get; set; }
        public PaySheet November { get; set; }
        public PaySheet December { get; set; }
        public PaySheet January { get; set; }
        public PaySheet February { get; set; }
        public PaySheet March { get; set; }
        public PaySheet Total { get; set; }
    }

    public class PaySheet
    {
        public int PaySheetId { get; set; }
        public int EmployeeId { get; set; }
        public decimal CTCPerMonth { get; set; }
        public decimal Basic { get; set; }
        public decimal HRA { get; set; }
        public decimal? Conveyance { get; set; }
        public decimal SpecialAllowance { get; set; }
        public decimal PerformanceIncentive { get; set; }
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
        public decimal PF { get; set; }
        public decimal Mediclaim { get; set; }
        public decimal Gratuity { get; set; }
        public int? Month { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}