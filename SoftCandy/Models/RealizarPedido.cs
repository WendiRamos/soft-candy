using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class RealizarPedido
    {
        public int Id_Cliente { get; set; }
        public virtual Cliente Clientes { get; set; }
        public IEnumerable<Produto> Produtos { get; set; }
        public IEnumerable<Cliente> clientes { get; set; }

        public RealizarPedido()
        {
        }
    }
}
