using System.ComponentModel.DataAnnotations;

namespace Core.Enums
{
    public enum StatusTypes
    {
        [Display(Name = "Onay Bekliyor")]
        Pending,
        [Display(Name = "Onaylandı")]
        Approved,
        [Display(Name = "Reddedildi")]
        Rejected,
        [Display(Name = "İptal Edildi")]
        Canceled
    }
}
