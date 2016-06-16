using System;
using Heroic.AutoMapper;
using Vserv.Accounting.Data.Entity;

namespace Vserv.Accounting.Web.Models
{
    public class EmployeePaySheet //: IMapFrom<GetEmployeeSalaryDetail_Result>
    {
        public int? EmployeeId { get; set; }
        public int? DisplayOrder { get; set; }
        public LookupAmount April { get; set; }
        public LookupAmount May { get; set; }
        public LookupAmount June { get; set; }
        public LookupAmount July { get; set; }
        public LookupAmount August { get; set; }
        public LookupAmount September { get; set; }
        public LookupAmount October { get; set; }
        public LookupAmount November { get; set; }
        public LookupAmount December { get; set; }
        public LookupAmount January { get; set; }
        public LookupAmount February { get; set; }
        public LookupAmount March { get; set; }
        public int? EmpSalaryStructureId { get; set; }
        public string SCName { get; set; }
        public string SCCode { get; set; }
        public string SCDescription { get; set; }
    }

    public class LookupAmount
    {
        public int EmployeeSalaryDetailId { get; set; }

        public Decimal? Amount { get; set; }
    }
}