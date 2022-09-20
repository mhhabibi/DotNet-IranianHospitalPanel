using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities.Entities
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "کد بیمار")]
        public int DocNumber { get; set; }

        [Display(Name ="کد ملی")]
        [MaxLength(10,ErrorMessage ="فیلد {0} فقط میتواند 10 کاراکتر باشد !")]
        public string NationalCode { get; set; }

        [Display(Name = "نام بیمار")]
        [MaxLength(50, ErrorMessage = "فیلد {0} فقط میتواند 50 کاراکتر باشد !")]
        public string FName { get; set; }

        [Display(Name = "فامیلی بیمار")]
        [MaxLength(50, ErrorMessage = "فیلد {0} فقط میتواند 50 کاراکتر باشد !")]
        public string LName { get; set; }

        [Display(Name = "نام همراه بیمار")]
        [MaxLength(50, ErrorMessage = "فیلد {0} فقط میتواند 50 کاراکتر باشد !")]
        public string FName2 { get; set; }

        [Display(Name = "فامیلی همراه بیمار")]
        [MaxLength(50, ErrorMessage = "فیلد {0} فقط میتواند 50 کاراکتر باشد !")]
        public string LName2 { get; set; }

        [Display(Name = "شماره شناسنامه")]
        [MaxLength(50, ErrorMessage = "فیلد {0} فقط میتواند 50 کاراکتر باشد !")]
        public string ShSh { get; set; }

        [Display(Name = "شماره پرونده")]
        [MaxLength(10, ErrorMessage = "فیلد {0} فقط میتواند 10 کاراکتر باشد !")]
        public string InsuranceNo { get; set; }

        [Display(Name = "پدر بیمار")]
        [MaxLength(50, ErrorMessage = "فیلد {0} فقط میتواند 50 کاراکتر باشد !")]
        public string FatherName { get; set; }

        public string BirthDate { get; set; }
        public bool IsMarried { get; set; }

        [Display(Name = "تلفن")]
        [MaxLength(50, ErrorMessage = "فیلد {0} فقط میتواند 50 کاراکتر باشد !")]
        public string Telephone { get; set; }

        [Display(Name = "تلفن همراه بیمار")]
        [MaxLength(50, ErrorMessage = "فیلد {0} فقط میتواند 50 کاراکتر باشد !")]
        public string Telephone2 { get; set; }

        [Display(Name = "موبایل بیمار")]
        [MaxLength(50, ErrorMessage = "فیلد {0} فقط میتواند 50 کاراکتر باشد !")]
        public string Mobile { get; set; }

        [Display(Name = "موبایل همراه بیمار")]
        [MaxLength(50, ErrorMessage = "فیلد {0} فقط میتواند 50 کاراکتر باشد !")]
        public string Mobile2 { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "آدرس")]
        public string Address { get; set; }

        public int? RegUser { get; set; }
        public string RegDate { get; set; }
        public string EditDate { get; set; }
        public int? EditUser { get; set; }
    }
}
