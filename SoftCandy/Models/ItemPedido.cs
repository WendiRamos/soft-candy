using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SoftCandy.Models
{
    public class Item_Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Preço Pago")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal PrecoPago { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        public int Quantidade { get; set; }

        [ForeignKey("Produto")]
        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Código Produto")]
        public int Cod_Produto { get; set; }
        public  virtual Produto Produto { get; set; }

        [ForeignKey("Pedido")]
        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Número Pedido")]
        public int Num_Pedido { get; set; }
        public virtual  Pedido Pedido { get; set; }
    }
}
