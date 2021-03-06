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
    
    public partial class EmpInvestment
    {
        public int EmpInvestmentId { get; set; }
        public int EmployeeId { get; set; }
        public int FinancialYear { get; set; }
        public Nullable<decimal> DeclaredAmount { get; set; }
        public Nullable<decimal> ApprovedAmount { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public bool IsApproved { get; set; }
        public Nullable<int> DocumentId { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
