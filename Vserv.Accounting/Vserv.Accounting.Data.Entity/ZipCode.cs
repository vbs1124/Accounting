//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vserv.Accounting.Data.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class ZipCode
    {
        public int ZipCodeId { get; set; }
        public string PinCode { get; set; }
        public string DivisionName { get; set; }
        public string Taluk { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual City City { get; set; }
        public virtual State State { get; set; }
    }
}
