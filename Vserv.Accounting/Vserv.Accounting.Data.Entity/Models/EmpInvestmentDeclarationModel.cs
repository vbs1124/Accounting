using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vserv.Accounting.Data.Entity.Models
{
    public class EmpInvestmentDeclarationModel
    {
        public int EmployeeId { get; set; }
        public List<InvestmentCategoryModel> InvestmentCategories { get; set; }
    }
}
