using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vserv.Accounting.Data.Entity.Models
{
    public class EmpSalaryCompareResult
    {
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string April { get; set; }
        public string May { get; set; }
        public string June { get; set; }
        public string July { get; set; }
        public string August { get; set; }
        public string September { get; set; }
        public string October { get; set; }
        public string November { get; set; }
        public string December { get; set; }
        public string January { get; set; }
        public string February { get; set; }
        public string March { get; set; }
        public Nullable<int> EmpSalaryStructureId { get; set; }
        public string SCName { get; set; }
        public string SCCode { get; set; }
        public string SCDescription { get; set; }
    }
}
