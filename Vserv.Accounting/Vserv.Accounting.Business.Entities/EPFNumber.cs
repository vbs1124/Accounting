using System;
using System.Collections.Generic;

namespace Vserv.Accounting.Business.Entities
{
    public partial class EPFNumber
    {
        public int EPFNumberId { get; set; }
        public int EmployeeId { get; set; }
        public int EPFOfficeId { get; set; }
        public string EstablishmentCode { get; set; }
        public string Extension { get; set; }
        public string AccountNumber { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public Nullable<int> UpdatedById { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
