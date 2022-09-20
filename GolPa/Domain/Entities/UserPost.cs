using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Entities
{
    public class UserPost
    {
        [Key]
        public long PersonId { get; set; }

        [Display(Name = "نام کوچک")]
        [Required(ErrorMessage = "پر کردن {0} الزامیست !")]
        [MaxLength(50, ErrorMessage = "فیلد {0} حد اکثر 50 کاراکتر ظرفیت دارد !")]
        public string Fname { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "پر کردن {0} الزامیست !")]
        [MaxLength(50, ErrorMessage = "فیلد {0} حد اکثر 50 کاراکتر ظرفیت دارد !")]
        public string Lname { get; set; }

        public string FullName { get; set; }

        [Display(Name = "شماره شناسنامه")]
        [MaxLength(50, ErrorMessage = "فیلد {0} حد اکثر 50 کاراکتر ظرفیت دارد !")]
        public string ShSh { get; set; }

        [Display(Name = "کد ملی")]
        [MaxLength(10, ErrorMessage = "فیلد {0} حد اکثر 10 کاراکتر ظرفیت دارد !")]
        public string NationalCode { get; set; }

        [Display(Name = "نام پدر")]
        [Required(ErrorMessage = "پر کردن {0} الزامیست !")]
        [MaxLength(50, ErrorMessage = "فیلد {0} حد اکثر 50 کاراکتر ظرفیت دارد !")]
        public string FatherName { get; set; }
        public bool IsDoctor { get; set; }
        public bool IsProfessor { get; set; }
        public string Degrees { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "پر کردن {0} الزامیست !")]
        [MaxLength(50, ErrorMessage = "فیلد {0} حد اکثر 50 کاراکتر ظرفیت دارد !")]
        public string UserName { get; set; }

        //[Display(Name = "رمز عبور")]
        //[Required(ErrorMessage = "پر کردن {0} الزامیست !")]
        //[MaxLength(50, ErrorMessage = "فیلد {0} حد اکثر 50 کاراکتر ظرفیت دارد !")]
        public string Password { get; set; }

        public int? JobId { get; set; }
        public int? GroupId { get; set; }
        public int? DepartmanId { get; set; }
        [Display(Name ="کد پرسنلی")]
        [Required(ErrorMessage ="پر کردن فیلد {0} الزامیست !")]
        public int? PersonelCode { get; set; }

        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "پر کردن فیلد {0} الزامیست !")]
        [MaxLength(11,ErrorMessage = "فیلد {0} حداکثر میتواند 11 رقم باشد !")]
        public string Mobile { get; set; }

        [Display(Name = "تلفن")]
        [Required(ErrorMessage = "پر کردن فیلد {0} الزامیست !")]
        [MaxLength(20, ErrorMessage = "فیلد {0} حداکثر میتواند 20 رقم باشد !")]
        public string Telephone { get; set; }

        public int? RoleId { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public string RegUser { get; set; }
        public string EditUser { get; set; }
        public List<int?> Permissions { get; set; }
        public string Token { get; set; }
        //public string Token { get; set; }
        public string ImagePath { get; set; }

        public List<int> ProfessorCourses{ get; set; }
        public List<int> DoctorExpert{ get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
    }
}
