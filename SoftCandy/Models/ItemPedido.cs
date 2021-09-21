using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SoftCandy.Models
{
    public class ItemPedido
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Preço Pago")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal PrecoPago { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Quantidade")]
        public int QuantidadePedido { get; set; }

        [ForeignKey("Produto")]
        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Id Produto")]
        public int IdProduto { get; set; }
        public  virtual Produto Produto { get; set; }

        [ForeignKey("Pedido")]
        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Id Pedido")]
        public int IdPedido { get; set; }
        public virtual  Pedido Pedido { get; set; }
    }
}
