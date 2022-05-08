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

        [Display(Name = "Nome")]
        public string NomeProduto { get; set; }

        [Display(Name = "Preço Venda")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal PrecoVendaProduto { get; set; }

        [Display(Name = "Quantidade")]
        public int QuantidadeProduto { get; set; }

        [Display(Name = "Quantidade Mínima")]
        public int QuantidadeMinimaProduto { get; set; }

        [Display(Name = "Descrição")]
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

        public Produto()
        {
        }

        public Produto(string nomeProduto, decimal precoVendaProduto, int quantidadeProduto, int quantidadeMinimaProduto, string descricaoProduto, bool ativoProduto, int idCategoria, Categoria categoria, int idFornecedor, Fornecedor fornecedor)
        {
            NomeProduto = nomeProduto;
            PrecoVendaProduto = precoVendaProduto;
            QuantidadeProduto = quantidadeProduto;
            QuantidadeMinimaProduto = quantidadeMinimaProduto;
            DescricaoProduto = descricaoProduto;
            AtivoProduto = ativoProduto;
            IdCategoria = idCategoria;
            IdFornecedor = idFornecedor;

        }

        public bool ProblemaAoSubtrair(int quantidadeParaSubtrair)
        {
            if (quantidadeParaSubtrair > QuantidadeProduto)
            {
                return true;
            }
            QuantidadeProduto -= quantidadeParaSubtrair;
            return false;
        }

        public void devolver(int quantidadeParaDevolver)
        {
            QuantidadeProduto += quantidadeParaDevolver;
        }
    }

}
