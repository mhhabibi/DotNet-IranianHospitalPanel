using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Common
{
    public class BaseEntity
    {
        [Required]
        [Display(Name ="عنوان")]
        public string Title { get; set; }
        public string ReqUser { get; set; }
        public DateTime? ReqDate { get; set; }
        public string EditUser { get; set; }
        public DateTime? EditDate { get; set; }
    }
}
