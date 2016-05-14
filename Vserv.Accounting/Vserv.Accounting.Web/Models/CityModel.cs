using System;
using System.Collections.Generic;

namespace Vserv.Accounting.Web.Models
{
    public class CityModel
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

        public virtual ICollection<AddressModel> Addresses { get; set; }
        public virtual StateModel State { get; set; }
        public virtual ICollection<ZipCodeModel> ZipCodes { get; set; }
    }
}