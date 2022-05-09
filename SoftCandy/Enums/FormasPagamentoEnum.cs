using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Enums
{
    public enum FormasPagamentoEnum
    {
        [Display(Name = "Dinheiro")]
        DINHEIRO = 1,
        [Display(Name = "Cartão de Débito")]
        CARTAO_DEBITO = 2,
        [Display(Name = "Cartão de Crédito")]
        CARTAO_CREDITO = 3,
        [Display(Name = "Pix")]
        PIX = 4
    }
}
