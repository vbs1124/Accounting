using System;
using System.Collections.Generic;

namespace Vserv.Accounting.Business.Entities
{
    public partial class EPFOffice
    {
        public int EPFOfficeId { get; set; }
        public string EPFOfficeName { get; set; }
        public string EPFOfficeCode { get; set; }
        public string StateId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public Nullable<int> UpdatedById { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
