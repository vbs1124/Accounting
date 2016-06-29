using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vserv.Accounting.Web.Models
{
    public class EmpSalaryCompareModel
    {
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public SalaryComponentDetail April { get; set; }
        public SalaryComponentDetail May { get; set; }
        public SalaryComponentDetail June { get; set; }
        public SalaryComponentDetail July { get; set; }
        public SalaryComponentDetail August { get; set; }
        public SalaryComponentDetail September { get; set; }
        public SalaryComponentDetail October { get; set; }
        public SalaryComponentDetail November { get; set; }
        public SalaryComponentDetail December { get; set; }
        public SalaryComponentDetail January { get; set; }
        public SalaryComponentDetail February { get; set; }
        public SalaryComponentDetail March { get; set; }
        public Nullable<int> EmpSalaryStructureId { get; set; }
        public string SCName { get; set; }
        public string SCCode { get; set; }
        public string SCDescription { get; set; }
    }

    public class SalaryComponentDetail
    {
        public Decimal? Amount { get; set; }
        public int EmpSalaryDetailId { get; set; }

        public Decimal? CurrentAmount { get; set; }
        public int CurrentEmpSalaryDetailId { get; set; }
        
        public bool IsDirty { get; set; }
    }
}