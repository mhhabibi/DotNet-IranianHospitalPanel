using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities.Entities
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }
        public string ExamTitle { get; set; }
        public string ExamDate { get; set; }
        public bool ExamType { get; set; }
        [Display(Name = "استاد")]
        [Required(ErrorMessage = "پر کردن فیلد {0} الزامیست")]
        public int Professor { get; set; }
        public string ProfessorFullName { get; set; }
        [Display(Name = "دوره")]
        [Required(ErrorMessage = "پر کردن فیلد {0} الزامیست")]
        public int ReqId { get; set; }
        public string CourseTitle { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int? RegUser { get; set; }
        public int? EditUser { get; set; }
        public DateTime? RegDate{ get; set; }
        public DateTime? EditDate{ get; set; }
    }
}
