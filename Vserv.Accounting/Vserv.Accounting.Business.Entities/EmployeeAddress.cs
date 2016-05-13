using System;
using System.Collections.Generic;

namespace Vserv.Accounting.Business.Entities
{
    public partial class EmployeeAddress
    {
        public int EmployeeAddressId { get; set; }
        public int EmployeeId { get; set; }
        public int AddressId { get; set; }
    }
}
