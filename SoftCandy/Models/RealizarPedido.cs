using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class RealizarPedido
    {
        [Display(Name = "Id")]
        public int IdCliente { get; set; }

        [Display(Name = "Produto")]
        public IEnumerable<Produto> Produtos { get; set; }
        public RealizarPedido()
        {
        }
    }
}
