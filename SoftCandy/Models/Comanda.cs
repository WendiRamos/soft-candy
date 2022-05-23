using SoftCandy.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Comanda
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

        public FormasPagamentoEnum FormaPagamento {get; set;}

        public virtual ICollection<ItemVenda> ItensVendas { get; set; }

        public Comanda()
        {
        }

        public void CalcularValorComanda()
        {
            decimal soma = 0;
            if (ItensVendas != null)
            {
                foreach (ItemVenda item in ItensVendas)
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
            ItensVendas.Add(item);
            CalcularValorComanda();
        }

        public void RemoverItem(ItemVenda item)
        {
            ItensVendas.Remove(item);
            CalcularValorComanda();
        }
    }
}
