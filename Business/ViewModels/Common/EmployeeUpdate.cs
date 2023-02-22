using Business.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Business.ViewModels.Common
{
    public class EmployeeUpdate
    {
        public string Id { get; set; }

        [Display(Name = "Resim")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile? NewPhoto { get; set; }
        
        [Required(ErrorMessage = "Adres girilmesi gerekli alandır.")]
        [Display(Name = "Adres")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Adres bilgisi minimum 3 maksimum 200 karakter içermelidir.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Telefon numarası girilmesi gerekli alandır.")]
        [Display(Name = "Telefon Numarası")]
        //[RegularExpression(@"^\(\d{3}\) \d{3}-\d{4}$", ErrorMessage = "Telefon numarası (XXX) XXX-XXXX formatında olmalıdır.")]
        public string PhoneNumber { get; set; }
    }
}
