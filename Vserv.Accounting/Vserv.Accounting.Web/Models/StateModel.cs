using System;
using System.Collections.Generic;

namespace Vserv.Accounting.Web.Models
{
    public class StateModel
    {
        public int StateId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual ICollection<AddressModel> Addresses { get; set; }
        public virtual ICollection<CityModel> Cities { get; set; }
        public virtual ICollection<ZipCodeModel> ZipCodes { get; set; }
    }
}