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
        [Key()]
        [Display(Name = "Id Pedido")]
        public int IdPedido { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Valor Total")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal ValorTotalPedido { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Data Pedido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime DataPedido { get; set; }

        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        
        public virtual Cliente Cliente { get; set; }

        public virtual ICollection<ItemPedido> ItensPedidos { get; set; }

        public Pedido(decimal ValorTotalPedido, int IdCliente, ICollection<ItemPedido> ItensPedidos)
        {
            ValorTotalPedido = ValorTotalPedido;
            DataPedido = DateTime.Now;
            IdCliente = IdCliente;
            ItensPedidos = ItensPedidos;
        }

        public Pedido(int IdPedido, decimal ValorTotalPedido, DateTime DataPedido, int IdCliente, Cliente cliente, ICollection<ItemPedido> ItensPedidos)
        {
            IdPedido = IdPedido;
            ValorTotalPedido = ValorTotalPedido;
            DataPedido = DataPedido;
            IdCliente = IdCliente;
            Cliente = cliente;
            ItensPedidos = ItensPedidos;
        }

        public Pedido()
        {
        }
    }
}
