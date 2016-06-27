//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vserv.Accounting.Data.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmpSalaryStructure
    {
        public EmpSalaryStructure()
        {
            this.EmployeeSalaryDetails = new HashSet<EmployeeSalaryDetail>();
            this.EmpSalaryDetails = new HashSet<EmpSalaryDetail>();
            this.EmpSalaryDetailArchives = new HashSet<EmpSalaryDetailArchive>();
            this.EmpSalaryStructure1 = new HashSet<EmpSalaryStructure>();
        }
    
        public int EmpSalaryStructureId { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public decimal CTC { get; set; }
        public Nullable<decimal> MonthlyCabDeductions { get; set; }
        public Nullable<decimal> MonthlyProjectIncentive { get; set; }
        public Nullable<decimal> MonthlyCarLease { get; set; }
        public Nullable<decimal> MonthlyFoodCoupons { get; set; }
        public System.DateTime EffectiveFrom { get; set; }
        public System.DateTime EffectiveTo { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual ICollection<EmployeeSalaryDetail> EmployeeSalaryDetails { get; set; }
        public virtual ICollection<EmpSalaryDetail> EmpSalaryDetails { get; set; }
        public virtual ICollection<EmpSalaryDetailArchive> EmpSalaryDetailArchives { get; set; }
        public virtual ICollection<EmpSalaryStructure> EmpSalaryStructure1 { get; set; }
        public virtual EmpSalaryStructure EmpSalaryStructure2 { get; set; }
    }
}
