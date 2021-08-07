using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Item_Pedido
    {
        
        public decimal Preco_Pago { get; set; }
        public int Quantidade { get; set; }

        [Key, Column(Order = 0)]
        public Cliente Cliente { get; set; }

        [Key, Column(Order = 1)]
        public Pedido Pedido { get; set; }

        public Item_Pedido(decimal preco_Pago, int quantidade, Cliente cliente, Pedido pedido)
        {
            Preco_Pago = preco_Pago;
            Quantidade = quantidade;
            Cliente = cliente;
            Pedido = pedido;
        }

        public Item_Pedido()
        {
        }
    }
}
