using Core.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Business.ViewModels.Common
{
    public class EmployeeSummary
    {
        [Display(Name = "İsim")]
        public string FullName { get; set; }

        [Display(Name = "Resim")]
        public string? Photo { get; set; }

        [Display(Name = "Ünvan")]
        public string Job { get; set; }

        [Display(Name = "Departman")]
        public Departments Department { get; set; }

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail Adresi")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Şirket")]
        public string CompanyInfo { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }
    }
}
