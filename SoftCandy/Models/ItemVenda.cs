using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SoftCandy.Models
{
    public class ItemVenda
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

        [ForeignKey("Comanda")]
        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Id Pedido")]
        public int IdComanda { get; set; }
        public virtual Comanda Comanda { get; set; }

        public ItemVenda()
        {
        }
    }
}
