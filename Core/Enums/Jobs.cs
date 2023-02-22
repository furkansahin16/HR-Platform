using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum Jobs
    {
        Eğitmen,
        Asistan,
        Koordinatör,
        [Display(Name = "İK Uzmanı")]
        İK,
        Yönetici
    }
}
