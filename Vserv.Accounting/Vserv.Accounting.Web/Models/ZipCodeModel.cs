
using System;
using System.Collections.Generic;

namespace Vserv.Accounting.Web.Models
{
    public class ZipCodeModel
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

        public virtual ICollection<AddressModel> Addresses { get; set; }
        public virtual CityModel City { get; set; }
        public virtual StateModel State { get; set; }
    }
}