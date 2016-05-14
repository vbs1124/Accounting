using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vserv.Accounting.Business.Entities
{
    public class City
    {
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public Nullable<int> UpdatedById { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<ZipCode> ZipCodes { get; set; }
    }
}
