using System;
using System.Collections.Generic;

namespace Vserv.Accounting.Business.Entities
{
    public partial class Address
    {
        public int AddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public bool IsCommunicationAddress { get; set; }
        public int AddressTypeId { get; set; }
        public int? StateId { get; set; }
        public int? CountryId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public Nullable<int> UpdatedById { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
