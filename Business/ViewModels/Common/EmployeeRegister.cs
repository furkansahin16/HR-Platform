using Business.Helper.MailOptions;
using Business.Validation;
using Core.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace Business.ViewModels.Common
{
    public class EmployeeRegister
    {
        [Required(ErrorMessage = "İsim girilmesi gerekli alandır.")]
        [RegularExpression(@"^[a-zA-ZçÇğĞıİöÖşŞüÜ ]{3,20}$", ErrorMessage = "Rakam ve özel karakter girilemez.İsim bilgisi minimum 3 maksimum 20 karakter içermelidir.")]
        [Display(Name = "İsim")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyisim girilmesi gerekli alandır.")]
        [RegularExpression(@"^[a-zA-ZçÇğĞıİöÖşŞüÜ ]{3,25}$", ErrorMessage = "Rakam ve özel karakter girilemez.Soyisim bilgisi minimum 3 maksimum 25 karakter içermelidir.")]
        [Display(Name = "Soyisim")]
        public string LastName { get; set; }

        [Display(Name = "İkinci İsim")]
        [RegularExpression(@"^[a-zA-ZçÇğĞıİöÖşŞüÜ ]{3,20}$", ErrorMessage = "Rakam ve özel karakter girilemez.İkinci İsim bilgisi minimum 3 maksimum 20 karakter içermelidir.")]
        public string? SecondName { get; set; }

        [Display(Name = "İkinci Soyisim")]
        [RegularExpression(@"^[a-zA-ZçÇğĞıİöÖşŞüÜ ]{3,25}$", ErrorMessage = "Rakam ve özel karakter girilemez.İkinci Soyisim bilgisi minimum 3 maksimum 25 karakter içermelidir.")]
        public string? SecondLastName { get; set; }

        [Required(ErrorMessage = "Doğum tarihi girilmesi gerekli alandır.")]
        [DataType(DataType.Date)]
        [Display(Name = "Doğum Tarihi")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [EighteenWithSixtyFiveYearsAttribute]
        public DateTime BirthDate { get; set; } = new DateTime(1980, 1, 1);

        [Display(Name = "Doğum Yeri")]
        [Required(ErrorMessage = "Doğum yeri girilmesi gerekli alandır.")]
        [RegularExpression(@"^[a-zA-ZçÇğĞıİöÖşŞüÜ ]{3,30}$", ErrorMessage = "Doğum yeri sadece harfler içerebilir ve en az 3, en fazla 30 karakter içermelidir.")]

        public string PlaceOfBirth { get; set; }

        [Required(ErrorMessage = "Telefon numarası girilmesi gerekli alandır.")]
        [Display(Name = "Telefon Numarası")]
        [RegularExpression(@"^\(\d{3}\) \d{3}-\d{4}$", ErrorMessage = "Telefon numarası (XXX) XXX-XXXX formatında olmalıdır.")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Currency)]
        [MaxLength(11, ErrorMessage = "TC Kimlik 11haneli olmalıdır.")]
        [MinLength(11, ErrorMessage = "TC Kimlik 11haneli olmalıdır.")]
        [Range(00000000000, 99999999999, ErrorMessage = "Yanlış TC No Formatı")]
        [Required(ErrorMessage = "TCKN girilmesi gerekli alandır.")]
        //[Tckn]
        [Display(Name = "TC Kimlik No")]
        public string TCNo { get; set; }

        [Display(Name = "İşe Giriş Tarihi")]
        [Required(ErrorMessage = "İşe giriş tarihi girilmesi gerekli alandır.")]
        [DataType(DataType.Date)]
        [CompareDate("BirthDate", 18, ErrorMessage = "İşe başlangıç tarihi doğum tarihinden en az 18 yıl sonra olmalıdır.")]
    
        public DateTime StartDateOfWork { get; set; } = new DateTime(2002, 1, 1);

        [Display(Name = "İşten Çıkış Tarihi")]
        [DataType(DataType.Date)]
        [EndDateGreaterThanStartDate(ErrorMessage = "İşten Ayrılma Tarihi, İşe Başlama Tarihinden büyük olmalıdır")]
        [FutureDate(ErrorMessage = "İşten ayrılış tarihi en fazla 2 ay sonra olmalıdır.")]
        public DateTime? DismissalDate { get; set; } /*= new DateTime(2003, 1, 1);*/

        [Display(Name = "İşten Ayrıldın Mı?")]
        public bool IsActive
        {
            get
            {
                return (DismissalDate == null || DismissalDate > DateTime.Now);
            }
        }

        [Display(Name = "Unvan")]
        [Required(ErrorMessage = "Unvan seçilmesi gerekli alandır.")]
        public Jobs Job { get; set; }

        [Display(Name = "Departman")]
        [Required(ErrorMessage = "Departman seçilmesi gerekli alandır.")]
        public Departments Department { get; set; }

        [Required(ErrorMessage = "Adres girilmesi gerekli alandır.")]
        [Display(Name = "Adres")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Adres bilgisi minimum 3 maksimum 200 karakter içermelidir.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Şirket bilgisi girilmesi gerekli alandır.")]
        [Display(Name = "Şirket")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Adres bilgisi minimum 3 maksimum 200 karakter içermelidir.")]
        public string CompanyInfo { get; set; }

        private string _email;

        [Required(ErrorMessage = "İsim-Soyisim girilmesi gerekli alandır.")]
        public string Email
        {
            get { return _email ?? MailCreateHelper.CreateMail(this.FirstName, this.LastName, this.CompanyInfo); }
            set { _email = value; }
        }

        //[Required(ErrorMessage = "Şifre alanı boş geçilemez.")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Şifre")]
        //[RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        // ErrorMessage = "Şifre en az bir büyük harf, bir küçük harf, bir özel karakter ve bir rakam içermelidir.")]
        //[StringLength(20, MinimumLength = 8, ErrorMessage = "Şifre maksimum uzunluğu 20 karakter ve şifre minimum 8 karakter barındırabilmektedir.")]
        //public string Password { get; set; }

        //[Required(ErrorMessage = "Şifre tekrar alanı boş geçilemez.")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Şifre Tekrar")]
        //[Compare("Password", ErrorMessage = "Şifreler eşleşmiyor!")]
        //public string ConfirmPassword { get; set; }

        [Display(Name = "Dönüş URL?")]
        public string? ReturnUrl { get; set; }

        [Display(Name = "Rol?")]
        public string? RoleSelected { get; set; }

        [Display(Name = "Resim")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile? Photo { get; set; }
    }
}
