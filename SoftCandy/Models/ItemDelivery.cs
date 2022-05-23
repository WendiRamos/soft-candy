using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SoftCandy.Models
{
    public class ItemDelivery
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
        [Display(Name = "Id Delivery")]
        public int IdDelivery { get; set; }
        public virtual Delivery Delivery { get; set; }

        public ItemDelivery()
        {
        }
    }
}
