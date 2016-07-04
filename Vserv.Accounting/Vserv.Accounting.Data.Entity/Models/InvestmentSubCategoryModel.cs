using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vserv.Accounting.Data.Entity.Models
{
    public class InvestmentSubCategoryModel
    {
        public int InvestmentSubCategoryId { get; set; }
        public int InvestmentCategoryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string MaximumLimit { get; set; }
        public Nullable<decimal> DefaultAmount { get; set; }
        public string Remark { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public int EmpInvestmentId { get; set; }
        public Nullable<decimal> ApprovedAmount { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
}
