using SoftCandy.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Pedido
    {
        [Key()]
        [Display(Name = "Id Pedido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Valor Total")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal ValorTotal { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Data/Hora Pedido")]
        [DataType(DataType.DateTime)]
        public DateTime DataHora { get; set; }

        public bool Ativo { get; set; }
        public bool Recebido { get; set; }


        [Display(Name = "Cliente")]
        public int? IdCliente { get; set; }
        public virtual Cliente Cliente { get; set; }


        [ForeignKey("Funcionario")]
        public int IdFuncionario { get; set; }

        public virtual Funcionario Funcionario { get; set; }


        [ForeignKey("Caixa")]
        public int IdCaixa { get; set; }

        public virtual Caixa Caixa { get; set; }

        public FormasPagamentoEnum FormaPagamento {get; set;}

        public virtual ICollection<ItemPedido> ItensPedidos { get; set; }

        public Pedido()
        {
        }

        public void CalcularValorPedido()
        {
            decimal soma = 0;
            if (ItensPedidos != null)
            {
                foreach (ItemPedido item in ItensPedidos)
                {
                    soma += item.Lote.Preco * item.Quantidade;
                }
            }
            ValorTotal = soma;
        }

        public bool FormaPagamentoIsDinheiro()
        {
            return FormaPagamento == FormasPagamentoEnum.DINHEIRO;
        }

        public bool FormaPagamentoIsCredito()
        {
            return FormaPagamento == FormasPagamentoEnum.CARTAO_CREDITO;
        }

        public bool FormaPagamentoIsDebito()
        {
            return FormaPagamento == FormasPagamentoEnum.CARTAO_DEBITO;
        }

        public bool FormaPagamentoIsPix()
        {
            return FormaPagamento == FormasPagamentoEnum.PIX;
        }
    }
}
