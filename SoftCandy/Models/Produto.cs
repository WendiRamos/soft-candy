using SoftCandy.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SoftCandy.Models
{
    public class Produto
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Quantidade Mínima")]
        public int QuantidadeMinima { get; set; }

        [Display(Name = "Quantidade Descartada")]
        public int QuantidadeDescartada { get; set; }

        [Display(Name = "Quantidade Decremento")]
        public int QuantidadeDecremento { get; set; }

        public bool Ativo { get; set; }

        public MedidaEnum Medida {get; set;}

        [NotMapped]
        public int QuantidadeEstoque { get; set; }

        [ForeignKey("Categoria")]
        [Display(Name = "Categoria")]
        public int IdCategoria { get; set; }

        public virtual Categoria Categoria { get; set; }

        [ForeignKey("Fornecedor")]
        [Display(Name = "Fornecedor")]
        public int IdFornecedor { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }

        public virtual List<Lote> Lotes { get; set; }


        public Produto()
        {
        }


        public bool ProblemaAoSubtrair(int quantidadeParaSubtrair)
        {
            if (quantidadeParaSubtrair > QuantidadeEstoque)
            {
                return true;
            }
            QuantidadeDescartada -= quantidadeParaSubtrair;
            return false;
        }

        public void Devolver(int quantidadeParaDevolver)
        {
            QuantidadeEstoque += quantidadeParaDevolver;
        }

        public void SomarQuantidade()
        {
            QuantidadeEstoque = Lotes.Where(p => p.Ativo).Select(lt => lt.QuantidadeEstoque).Sum();
        }
    }

}
