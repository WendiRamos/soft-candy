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
        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Preço Pago")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal Preco_Pago { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        public int Quantidade { get; set; }

        [Key, Column(Order = 0)]
        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Id Cliente")]
        public Cliente Cliente { get; set; }

        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Número Pedido")]
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
