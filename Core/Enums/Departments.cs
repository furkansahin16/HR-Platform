using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum Departments
    {
        Akademi,
        Finans,
        Teknoloji,
        [Display(Name ="İnsan Kaynakları")]
        İK,
        Yönetim
    }
}
