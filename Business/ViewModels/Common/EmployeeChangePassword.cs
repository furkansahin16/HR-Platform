using Business.SystemResult.Enums;
using Business.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Business.ViewModels.Common
{
    public class EmployeeChangePassword
    {
        [Required(ErrorMessage = "Mevcut şifre boş geçilemez.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mevcut Şifre")]
        [Compare("CurrentPassword", ErrorMessage = "Mevcut şifre yanlış.")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş geçilemez.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", 
        ErrorMessage = "Şifre en az bir büyük harf, bir küçük harf, bir özel karakter ve bir rakam içermelidir.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Şifre maksimum uzunluğu 20 karakter ve şifre minimum 8 karakter barındırabilmektedir.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrar alanı boş geçilemez.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor!")]
        public string ConfirmPassword { get; set; }
    }
}
