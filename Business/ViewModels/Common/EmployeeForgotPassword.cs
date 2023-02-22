using System.ComponentModel.DataAnnotations;

namespace Business.ViewModels.Common
{
    public class EmployeeForgotPassword
    {
        [Required(ErrorMessage = "E-mail girilmesi gerekli alandır.")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail Adresi")]
        public string Email { get; set; }
    }
}
