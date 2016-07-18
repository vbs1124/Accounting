using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vserv.Accounting.Data.Entity.Models
{
    public class TaxComputationComponent
    {
        public int DisplayOrder { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Amount { get; set; }
    }
}
