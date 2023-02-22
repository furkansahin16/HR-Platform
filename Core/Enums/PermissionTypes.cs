using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum PermissionTypes
    {
        [Display(Name ="Yıllık İzin")]
        Yıllık,
        [Display(Name = "Evlilik İzni")]
        Evlilik,
        [Display(Name = "Özel İzin")]
        Özel,
        [Display(Name = "Babalık İzni")]
        Babalık,
        [Display(Name = "Hamilelik İzni")]
        Hamilelik,
        [Display(Name = "Cenaze İzni")]
        Cenaze,

    }
}
