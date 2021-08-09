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


        [ForeignKey("Id Cliente")]
        [Display(Name = "Id Cliente")]
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
