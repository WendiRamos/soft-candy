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
        public ICollection<Item_Pedido> Itens_Pedidos { get; set; }
    }
}
