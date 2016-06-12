using System;
using System.ComponentModel.DataAnnotations;
using Heroic.AutoMapper;
using Vserv.Accounting.Data.Entity;

namespace Vserv.Accounting.Web.Models
{
    public class SecurityQuestionModel : IMapFrom<SecurityQuestion>
    {
        [Display(Name="#")]
        public int SecurityQuestionId { get; set; }
        public int? CollectionId { get; set; }
        public string Question { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}