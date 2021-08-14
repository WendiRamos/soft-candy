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
        [Display(Name = "Número Pedido")]
        public int Num_Pedido { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Valor Total")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal Valor_Total { get; set; }

        [Display(Name = "Desconto")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal Desconto { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Data Pedido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime Data_Pedido { get; set; }


        [ForeignKey("Cliente")]
        public int ID_CLIENTE { get; set; }
        public virtual Cliente Cliente { get; set; }
        public ICollection<Item_Pedido> Itens_Pedidos { get; set; }

        public Pedido(int num_Pedido, decimal valor_Total, decimal desconto, DateTime data_Pedido, int iD_CLIENTE, Cliente cliente, ICollection<Item_Pedido> itens_Pedidos)
        {
            Num_Pedido = num_Pedido;
            Valor_Total = valor_Total;
            Desconto = desconto;
            Data_Pedido = data_Pedido;
            ID_CLIENTE = iD_CLIENTE;
            Cliente = cliente;
            Itens_Pedidos = itens_Pedidos;
        }

        public Pedido()
        {
        }
    }
}
