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

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Data Pedido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime Data_Pedido { get; set; }

        [ForeignKey("Cliente")]
        public int ID_CLIENTE { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual ICollection<Item_Pedido> Itens_Pedidos { get; set; }

        public Pedido(decimal valor_Total, int iD_CLIENTE, ICollection<Item_Pedido> itens_Pedidos)
        {
            Valor_Total = valor_Total;
            Data_Pedido = DateTime.Now;
            ID_CLIENTE = iD_CLIENTE;
            Itens_Pedidos = itens_Pedidos;
        }

        public Pedido(int num_Pedido, decimal valor_Total, DateTime data_Pedido, int iD_CLIENTE, Cliente cliente, ICollection<Item_Pedido> itens_Pedidos)
        {
            Num_Pedido = num_Pedido;
            Valor_Total = valor_Total;
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
