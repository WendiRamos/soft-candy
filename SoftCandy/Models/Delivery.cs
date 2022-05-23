using SoftCandy.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Delivery
    {
        [Key()]
        [Display(Name = "Id Comanda")]
        public int Id { get; set; }

        [Display(Name = "Valor Total")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal ValorTotal { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Data/Hora Criação")]
        public DateTime DataHoraCriacao { get; set; }

        [Display(Name = "Data/Hora Recebimento")]
        public DateTime DataHoraRecebimento { get; set; }
        public bool Recebido { get; set; }

        [ForeignKey("Caixa")]
        public int IdCaixa { get; set; }

        public virtual Caixa Caixa { get; set; }

        [ForeignKey("Motoboy")]
        public int IdMotoboy { get; set; }

        public virtual Motoboy Motoboy { get; set; }

        public FormasPagamentoEnum FormaPagamento {get; set;}

        public virtual ICollection<ItemVenda> ItensVenda { get; set; }

        public Delivery()
        {
        }

        public void CalcularValor()
        {
            decimal soma = 0;
            if (ItensVenda != null)
            {
                foreach (ItemVenda item in ItensVenda)
                {
                    soma += item.Lote.PrecoVenda * item.Quantidade;
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

        public void AdicionarItem(ItemVenda item)
        {
            ItensVenda.Add(item);
            CalcularValor();
        }

        public void RemoverItem(ItemVenda item)
        {
            ItensVenda.Remove(item);
            CalcularValor();
        }
    }
}
