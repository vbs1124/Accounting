using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vserv.Accounting.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class DesignationModel
    {
        /// <summary>
        /// Gets or sets the designation identifier.
        /// </summary>
        /// <value>
        /// The designation identifier.
        /// </value>
        public int DesignationId { get; set; }
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(ErrorMessage = "Designation Name is required.")]
        [Display(Name = "Designation Name")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Designation Name is too short.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Display(Name = "Description")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Description is too short.")]
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the display order.
        /// </summary>
        /// <value>
        /// The display order.
        /// </value>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public string UpdatedBy { get; set; }
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
        public Nullable<DateTime> UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        /// <value>
        /// The employees.
        /// </value>
        public virtual ICollection<EmployeeModel> Employees { get; set; }
    }
}