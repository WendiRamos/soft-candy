
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Enums
{
    public enum MedidaEnum
    {
        [Display(Name = "Unidade")]
        UNIDADE = 1,

        [Display(Name = "Ml")]
        ML = 2
    }
}
