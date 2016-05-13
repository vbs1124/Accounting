using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vserv.Accounting.Business.Entities
{
   public class ZipCode
    {
        public int ZipCodeId { get; set; }
        public string PinCode { get; set; }
        public string DivisionName { get; set; }
        public string Taluk { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public Nullable<int> UpdatedById { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual City City { get; set; }
        public virtual State State { get; set; }
    }
}
