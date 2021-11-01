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
        [Display(Name = "Id")]
        public int IdProduto { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Nome")]
        public string NomeProduto { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Quantidade")]
        public int QuantidadeProduto { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Preço Venda")]
        public decimal PrecoVendaProduto { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(60, MinimumLength = 0, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        public string DescricaoProduto { get; set; }

        public bool AtivoProduto { get; set; }

        [ForeignKey("Categoria")]
        [Display(Name = "Categoria")]
        public int IdCategoria { get; set; }

        public virtual Categoria Categoria { get; set; }

        [ForeignKey("Fornecedor")]
        [Display(Name = "Fornecedor")]
        public int IdFornecedor { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }

        public virtual ICollection<ItemPedido> ItensPedidos { get; set; }
    }

}
