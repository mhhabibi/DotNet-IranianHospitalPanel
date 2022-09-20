using Domain.Common;
using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Entities
{
    public class Departman: SBaseEntity
    {
        [Key]
        public int DepartmanId { get; set; }
        [Display(Name= "مدیر دپارتمان")]
        [Required(ErrorMessage = "پر کردن {0} الزامیست")]
        public int Manager { get; set; }
    }
}
