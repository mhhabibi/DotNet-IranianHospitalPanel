using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities.Common
{
    public class SBaseEntity
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "پر کردن {0} الزامیست")]
        public string Title { get; set; }
        public string RegUser { get; set; }
        public DateTime? RegDate { get; set; }
        public string EditUser { get; set; }
        public DateTime? EditDate { get; set; }
    }
}
