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

        [Display(Name = "Cliente")]
        public virtual Cliente Clientes { get; set; }

        [Display(Name = "Produto")]
        public IEnumerable<Produto> Produtos { get; set; }

        public IEnumerable<Cliente> clientes { get; set; }

        public RealizarPedido()
        {
        }
    }
}
