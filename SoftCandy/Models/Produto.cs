using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Produto
    {
        [Key]
        [Display(Name = "Código")]
        public int Cod_Produto { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Nome")]
        public string Nome_Produto { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Preço Venda")]
        public decimal Preco_Venda { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(60, MinimumLength = 0, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        public string Descricao { get; set; }

        [ForeignKey("Categoria")]
        [Display(Name = "Categoria")]
        public int Id_Categoria { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual ICollection<Item_Pedido> Itens_Pedidos { get; set; }
    }

}
