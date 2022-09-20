using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities.Entities
{
    public class AcceptCourse
    {
        public int? editUser { get; set; }
        [Display(Name="آیدی دوره درخواست شده")]
        [Required(ErrorMessage ="پر کردن فیلد {0} الزامیست !")]
        public int ReqId { get; set; }
        [Display(Name = "استاد")]
        [Required(ErrorMessage = "پر کردن فیلد {0} الزامیست !")]
        public int ProfessorId { get; set; }
        [Display(Name = "وقت پایان")]
        [Required(ErrorMessage = "پر کردن فیلد {0} الزامیست !")]
        public string FinishDate { get; set; }
        public string Description { get; set; }
    }
}
