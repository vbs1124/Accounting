using Heroic.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vserv.Accounting.Data.Entity;

namespace Vserv.Accounting.Web.Models
{
    public class EmployeePaySheet : IMapFrom<GetEmployeeSalaryDetail_Result>
    {
        public Nullable<int> EmployeeId { get; set; }
        public string ComponentName { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<decimal> April { get; set; }
        public Nullable<decimal> May { get; set; }
        public Nullable<decimal> June { get; set; }
        public Nullable<decimal> July { get; set; }
        public Nullable<decimal> August { get; set; }
        public Nullable<decimal> September { get; set; }
        public Nullable<decimal> October { get; set; }
        public Nullable<decimal> November { get; set; }
        public Nullable<decimal> December { get; set; }
        public Nullable<decimal> January { get; set; }
        public Nullable<decimal> February { get; set; }
        public Nullable<decimal> March { get; set; }
    }
}