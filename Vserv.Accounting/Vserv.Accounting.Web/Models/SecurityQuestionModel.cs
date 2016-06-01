using Heroic.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vserv.Accounting.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Vserv.Accounting.Web.Models
{
    public class SecurityQuestionModel : IMapFrom<SecurityQuestion>
    {
        [Display(Name="#")]
        public int SecurityQuestionId { get; set; }
        public Nullable<int> CollectionId { get; set; }
        public string Question { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}