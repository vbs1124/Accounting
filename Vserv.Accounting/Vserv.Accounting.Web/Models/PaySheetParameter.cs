using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vserv.Accounting.Web.Models
{
    public class PaySheetParameter
    {
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> FinancialYearFrom { get; set; }
        public Nullable<int> FinancialYearTo { get; set; }
    }
}