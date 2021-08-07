using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Pedido
    {
        [Key]
        public int Num_Pedido { get; set; }
        public decimal Valor_Total { get; set; }
        public decimal Desconto { get; set; }
        public DateTime Data_Pedido { get; set; }
       // [ForeignKey("Id Cliente")]
        public Cliente Cliente { get; set; }


        public Pedido(int num_Pedido, decimal valor_Total, decimal desconto, DateTime data_Pedido, Cliente cliente )
        {
            Num_Pedido = num_Pedido;
            Valor_Total = valor_Total;
            Desconto = desconto;
            Data_Pedido = data_Pedido;
            Cliente = Cliente;
        }
        public Pedido()
        {
        }
    }
}
