using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Lote
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Quantidade Estoque")]
        public int QuantidadeEstoque { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Data/Hora de Fabricação")]
        [DataType(DataType.DateTime)]
        public DateTime DataHoraFabricacao { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Data/Hora de Validade")]
        [DataType(DataType.DateTime)]
        public DateTime DataHoraValidade { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Preço de Compra")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal PrecoCompra { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Preço de Venda")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal PrecoVenda { get; set; }

        public bool Ativo { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Dias de Validade")]
        public int DiasVencimento { get; set; }

        [ForeignKey("Produto")]
        [Display(Name = "Produto")]
        public int IdProduto { get; set; }
        public virtual Produto Produto { get; set; }

        public Lote()
        {
        }

        public bool DisponivelParaVenda()
        {
            return Ativo
                && QuantidadeEstoque > 0
                && DataHoraValidade > DateTime.Now;
        }

        public bool DecrementarVenda(int quantidadeRetirar)
        {
            if (quantidadeRetirar > QuantidadeEstoque)
            {
                return false;
            }
            else
            {
                QuantidadeEstoque -= quantidadeRetirar;
                return true;
            }
        }

        public void DevolverQuantidade(int quantidade)
        {
            QuantidadeEstoque += quantidade;
        }

        public bool EstaVencido()
        {
            return DataHoraValidade < DateTime.Now;
        }

        public void Descartar()
        {
            Produto.QuantidadeDescartada += QuantidadeEstoque;
            QuantidadeEstoque = 0;
        }

        public bool PossuiEstoque()
        {
            return QuantidadeEstoque > 0;
        }

        public bool MostrarNoCardVencido()
        {
            return Ativo && PossuiEstoque() && EstaVencido();
        }
    }
}
