using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Enums
{
    public enum CargosEnum
    {
        [Display(Name = "Administrador")]
        ADMINISTRADOR = 1,

        [Display(Name = "Estoquista")]
        ESTOQUISTA = 2,

        [Display(Name = "Vendedor")]
        VENDEDOR = 3,

        [Display(Name = "Caixa")]
        CAIXA = 4
    }
}
