using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

    public partial class PaySheet
    {
        public int PaySheetId { get; set; }
        public int EmployeeId { get; set; }
        public decimal CTCPerMonth { get; set; }
        public decimal Basic { get; set; }
        public decimal HRA { get; set; }
        public Nullable<decimal> Conveyance { get; set; }
        public decimal SpecialAllowance { get; set; }
        public decimal PerformanceIncentive { get; set; }
        public Nullable<decimal> LeaveEncashment { get; set; }
        public Nullable<decimal> SalaryArrears { get; set; }
        public Nullable<decimal> CabDeductions { get; set; }
        public Nullable<decimal> OtherDeduction { get; set; }
        public Nullable<decimal> Commission { get; set; }
        public Nullable<decimal> Others { get; set; }
        public Nullable<decimal> Medical { get; set; }
        public Nullable<decimal> FoodCoupons { get; set; }
        public Nullable<decimal> ProjectIncentive { get; set; }
        public Nullable<decimal> CarLease { get; set; }
        public Nullable<decimal> LTC { get; set; }
        public decimal PF { get; set; }
        public decimal Mediclaim { get; set; }
        public decimal Gratuity { get; set; }
        public Nullable<int> Month { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}