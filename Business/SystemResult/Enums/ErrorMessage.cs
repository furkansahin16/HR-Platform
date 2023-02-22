using System.ComponentModel.DataAnnotations;

namespace Business.SystemResult.Enums
{
    public enum ErrorMessage
    {
        [Display(Name ="Güncelleme işlemi sırasında hata oluştu.")]
        UpdateErrorMessage,
        [Display(Name = "Detaylari getirme işlemi sırasında hata oluştu.")]
        DetailErrorMessage
    }
}
