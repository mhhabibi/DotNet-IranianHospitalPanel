using Domain.Common;
using Domain.Entities.Common;
using Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.ForIn;

namespace ViewModels
{
    public class UserViewModel
    {
        [Key]
        
        public int PersonId { get; set; }

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

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "پر کردن {0} الزامیست !")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public string ImagePath { get; set; }
        public string RegUser { get; set; }
        public DateTime? RegDate { get; set; }
        public string EditUser { get; set; }
        public DateTime? EditDate { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public int PersonelCode { get; set; }
        public List<ViewModels.Extra.PersonelPermission> Permissions { get; set; }
        public ForInJob job { get; set; }
        public ForInDepartman departman { get; set; }
        public ForInGroup group { get; set; }
        public List<ForInExpertPersonel> experts {get; set;}
        public List<int> courses { get; set; }
    }
}
