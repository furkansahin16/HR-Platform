using Business.Helper.PermissionHelper;
using Business.SystemResult.Enums;
using Business.Validation.PersonelValidation;
using Core.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.ViewModels.PermissionVM
{
    public class PermissionCreate
    {
        [DisplayName("İzin Türleri")]
        public PermissionTypes PermissionTypes { get; set; }

        [Required(ErrorMessage ="İzin başlangıç tarihi boş geçilemez.")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        [DisplayName("İzin Başlangıç Tarihi")]
        [StartDateValidation(ErrorMessage = "İzin başlangıç tarihi bugünden sonraki bir tarih olmalıdır.")]
        public DateTime StartTime { get; set; } = DateTime.Now.Date;

        [Required(ErrorMessage = "İzin bitiş tarihi boş geçilemez.")]
        [DataType(DataType.Date)]
        [DisplayName("İzin Bitiş Tarihi")]
        [EndDateValidation(ErrorMessage = "İzin bitiş tarihi başlangıç tarihinden büyük ve başlangıç tarihinden en fazla 3 ay içinde olmalıdır.")]
        public DateTime EndTime { get; set; } = DateTime.Now.AddDays(1).Date;

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DisplayName("İstek Zamanı")]
        public DateTime RequestTime { get; set; } = DateTime.Now.Date;
    }
}
