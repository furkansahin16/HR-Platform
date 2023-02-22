using Core.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.ViewModels.PermissionVM
{
    public class PermissionListItem
    {
        public Guid Id { get; set; }

        [Display(Name ="İzin Talep Zamanı")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime RequestTime { get; set; }

        [DisplayName("Talep Yanıt Zamanı")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime ResponseTime { get; set; }

        [DisplayName("Başlangıç Tarihi")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime StartTime { get; set; }

        [DisplayName("Bitiş Tarihi")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime EndTime { get; set; }

        [DisplayName("Toplam Gün Sayısı")]
        public int Day { get; set; }

        [DisplayName("İzin Türü")]
        public string PermissonType { get; set; }

        [DisplayName("Personel Adı")]
        public string Employee { get; set; }

        [DisplayName("Durum")]
        public string Status { get; set; }

    }
}
