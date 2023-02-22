using Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites.Concrete
{
    public class Employee : IdentityUser
    {
        [Display(Name = "İsim")]
        public string FirstName { get; set; }

        [Display(Name = "Soyisim")]
        public string LastName { get; set; }

        [Display(Name = "İkinci İsim")]
        public string? SecondName { get; set; }

        [Display(Name = "İkinci Soyisim")]
        public string? SecondLastName { get; set; }

        [Display(Name = "Doğum Tarihi")]

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Doğum Yeri")]
        public string PlaceOfBirth { get; set; }

        [Display(Name = "Resim")]
        public string? Photo { get; set; }

        [Display(Name = "TC Kimlik No")]
        public string TCNo { get; set; }

        [Display(Name = "İşe Giriş Tarihi")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime StartDateOfWork { get; set; }

        [Display(Name = "İşten Çıkış Tarihi")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DismissalDate { get; set; }

        [Display(Name = "Aktif mi?")]
        public bool IsActive { get; set; }

        [Display(Name = "Ünvan")]
        public Jobs Job { get; set; }

        [Display(Name = "Departman")]
        public Departments Department { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Şirket")]
        public string CompanyInfo { get; set; }

        [NotMapped]
        public string? RoleId { get; set; }

        [NotMapped]
        public string? Role { get; set; }

        public int WorkTimeByYear { get; set; }

        public int AnnualPermission { get; set; }

        public ICollection<Permission> Permissons { get; set; }

        public Employee()
        {
            this.Permissons = new HashSet<Permission>();
        }

        public override string ToString()
        {
            return FirstName + " "
                + (SecondName == null ? "" : (SecondName + " "))
                + LastName
                + (SecondLastName == null ? "" : (" " + SecondName));
        }
    }
}
