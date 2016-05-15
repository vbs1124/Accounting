using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vserv.Accounting.Web.Models
{
    public class DesignationModel
    {
        public int DesignationId { get; set; }
        public string Code { get; set; }

        [Required(ErrorMessage = "Designation Name is required.")]
        [Display(Name = "Designation Name")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Designation Name is too short.")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Description is too short.")]
        public string Description { get; set; }
        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }

        public virtual ICollection<EmployeeModel> Employees { get; set; }
    }
}