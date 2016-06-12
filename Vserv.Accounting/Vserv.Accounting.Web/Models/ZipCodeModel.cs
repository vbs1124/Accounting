
using System;
using System.Collections.Generic;

namespace Vserv.Accounting.Web.Models
{
    public class ZipCodeModel
    {
        /// <summary>
        /// Gets or sets the zip code identifier.
        /// </summary>
        /// <value>
        /// The zip code identifier.
        /// </value>
        public int ZipCodeId { get; set; }
        /// <summary>
        /// Gets or sets the pin code.
        /// </summary>
        /// <value>
        /// The pin code.
        /// </value>
        public string PinCode { get; set; }
        /// <summary>
        /// Gets or sets the name of the division.
        /// </summary>
        /// <value>
        /// The name of the division.
        /// </value>
        public string DivisionName { get; set; }
        /// <summary>
        /// Gets or sets the taluk.
        /// </summary>
        /// <value>
        /// The taluk.
        /// </value>
        public string Taluk { get; set; }
        /// <summary>
        /// Gets or sets the city identifier.
        /// </summary>
        /// <value>
        /// The city identifier.
        /// </value>
        public int CityId { get; set; }
        /// <summary>
        /// Gets or sets the state identifier.
        /// </summary>
        /// <value>
        /// The state identifier.
        /// </value>
        public int StateId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }
        /// <summary>
        /// Gets or sets the created by identifier.
        /// </summary>
        /// <value>
        /// The created by identifier.
        /// </value>
        public int CreatedById { get; set; }
        /// <summary>
        /// Gets or sets the updated by identifier.
        /// </summary>
        /// <value>
        /// The updated by identifier.
        /// </value>
        public int? UpdatedById { get; set; }
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>
        /// The addresses.
        /// </value>
        public virtual ICollection<AddressModel> Addresses { get; set; }
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public virtual CityModel City { get; set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public virtual StateModel State { get; set; }
    }
}