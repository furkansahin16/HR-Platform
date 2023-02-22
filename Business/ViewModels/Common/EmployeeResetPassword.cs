using System.ComponentModel.DataAnnotations;

namespace Business.ViewModels.Common
{
    public class EmployeeResetPassword
    {
        [Required(ErrorMessage = "E-mail girilmesi gerekli alandır.")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail Adresi")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor!")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }

    }
}
