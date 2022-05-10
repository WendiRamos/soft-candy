using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SoftCandy.Models
{
    public class ItemPedido
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

        [ForeignKey("Lote")]
        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Id Lote")]
        public int IdLote { get; set; }
        public virtual Lote Lote { get; set; }

        [ForeignKey("Pedido")]
        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Id Pedido")]
        public int IdPedido { get; set; }
        public virtual Pedido Pedido { get; set; }

        public ItemPedido()
        {
        }
    }
}
