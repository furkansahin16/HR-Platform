using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Business.ViewModels.Common
{
    public class EmployeeLogin
    {
        [Required(ErrorMessage ="E-mail girilmesi gerekli alandır.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen geçerli bir e-posta adresi giriniz.")]
        [Display(Name = "E-mail Adresi")]
        public string Email { get; set; }

        [Display(Name = "Adı")]
        public string? UserName { get { return this.Email; } }

        [Required(ErrorMessage = "Şifre alanı boş geçilemez.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
