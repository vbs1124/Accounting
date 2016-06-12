using Heroic.AutoMapper;
using Vserv.Accounting.Data.Entity;

namespace Vserv.Accounting.Web.Models
{
    public class EmployeePaySheet : IMapFrom<GetEmployeeSalaryDetail_Result>
    {
        public int? EmployeeId { get; set; }
        public string ComponentName { get; set; }
        public int? Year { get; set; }
        public int? DisplayOrder { get; set; }
        public decimal? April { get; set; }
        public decimal? May { get; set; }
        public decimal? June { get; set; }
        public decimal? July { get; set; }
        public decimal? August { get; set; }
        public decimal? September { get; set; }
        public decimal? October { get; set; }
        public decimal? November { get; set; }
        public decimal? December { get; set; }
        public decimal? January { get; set; }
        public decimal? February { get; set; }
        public decimal? March { get; set; }
    }
}