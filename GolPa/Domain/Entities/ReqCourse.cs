using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Entities
{
    public class ReqCourse
    {
        [Key]
        public int ReqId { get; set; }
        [Display(Name = "دپارتمان")]
        [Required(ErrorMessage = "پر کردن فیلد {0} الزامیست")]
        public int? DepartmanId { get; set; }
        public int ReqNo{ get; set; }
        public DateTime? ReqDate{ get; set; }
        [Display(Name = "دوره")]
        [Required(ErrorMessage = "پر کردن فیلد {0} الزامیست")]
        public int CourseId { get; set; }
        [Display(Name = "زمان مورد نیاز")]
        [Required(ErrorMessage = "پر کردن فیلد {0} الزامیست")]
        public int NeedTime { get; set; }
        [Display(Name = "ظرفیت آقایان")]
        [Required(ErrorMessage = "پر کردن فیلد {0} الزامیست")]
        public int ManCapacity { get; set; }
        [Display(Name = "ظرفیت بانوان")]
        [Required(ErrorMessage = "پر کردن فیلد {0} الزامیست")]
        public int WomanCapacity { get; set; }
        public bool IsInHospital { get; set; }
        public bool IsOutSide { get; set; }
        public string Description { get; set; }
        [Display(Name ="تاریخ شروع")]
        [Required(ErrorMessage = "پر کردن فیلد {0} الزامیست")]
        public string PerformDate { get; set; }
        [Display(Name = "تاریخ پایان")]
        [Required(ErrorMessage = "پر کردن فیلد {0} الزامیست")]
        public string FinishDate { get; set; }
        public string EventPlace { get; set; }
        public string RegUser { get; set; }
        public DateTime? RegDate { get; set; }
        public string EditUser { get; set; }
        public DateTime? EditDate { get; set; }
        public List<int> Personels { get; set; }
    }
}
