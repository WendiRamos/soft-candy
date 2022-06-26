using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Enums
{
    public enum OperacoesEnum
    {
        [Display(Name = "Entrada")]
        ENTRADA = 1,

        [Display(Name = "Saída")]
        SAIDA = 2
    }
}
