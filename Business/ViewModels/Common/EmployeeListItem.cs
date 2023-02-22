using Core.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Business.ViewModels.Common
{
    public class EmployeeListItem
    {
        [Display(Name = "Resim")]
        [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Yalnızca jpg, jpeg ve png dosyalarına izin verilir.")]
        public string Photo { get; set; }

        [Display(Name = "İsim")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail Adresi")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Telefon numarası girilmesi gerekli alandır.")]
        [Display(Name = "Telefon Numarası")]
        [RegularExpression(@"^\(\d{3}\) \d{3}-\d{4}$", ErrorMessage = "Telefon numarası (XXX) XXX-XXXX formatında olmalıdır.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Departman")]
        [Required(ErrorMessage = "Departman seçilmesi gerekli alandır.")]
        public Departments Department { get; set; }
    }
}
