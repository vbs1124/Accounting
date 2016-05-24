using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vserv.Accounting.Data.Entity
{
    public class CompareEmployeeModel
    {
        public EmployeeArchive PreviousEmployeeInfo { get; set; }

        public Employee CurrentEmployeeInfo { get; set; }

        public int ModifiedColumnCount { get; set; }
    }
}
