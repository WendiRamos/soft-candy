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
        [Display(Name = "Id Delivery")]
        public int Id { get; set; }

        [Display(Name = "Valor Total")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal ValorTotal { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Valor Frete")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal ValorFrete { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Data/Hora Criação")]
        public DateTime DataHoraCriacao { get; set; }

        [Display(Name = "Data/Hora Recebimento")]
        public DateTime DataHoraRecebimento { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Nome do Cliente")]
        public string NomeCliente { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Endereço Entrega")]
        public string EnderecoEntrega { get; set; }
        public bool Recebido { get; set; }
        public bool Ativo { get; set; }

        [ForeignKey("Caixa")]
        public int IdCaixa { get; set; }

        public virtual Caixa Caixa { get; set; }

        [ForeignKey("Motoboy")]
        public int IdMotoboy { get; set; }

        public virtual Motoboy Motoboy { get; set; }
        [Display(Name = "Forma de Pagamento")]
        public FormasPagamentoEnum FormaPagamento {get; set;}

        public virtual ICollection<ItemDelivery> ItensDelivery { get; set; }

        public Delivery()
        {
        }

        public void CalcularValor()
        {
            decimal soma = 0;
            if (ItensDelivery != null)
            {
                foreach (ItemDelivery item in ItensDelivery)
                {
                    soma += item.Lote.PrecoVenda * item.Quantidade;
                }
            }
            ValorTotal = soma + ValorFrete;
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

        public void AdicionarItem(ItemDelivery item)
        {
            ItensDelivery.Add(item);
            CalcularValor();
        }

        public void RemoverItem(ItemDelivery item)
        {
            ItensDelivery.Remove(item);
            CalcularValor();
        }

        public bool RecebidaDentroDoPeriodo(DateTime? minDate, DateTime? maxDate)
        {
            return Recebido
                && minDate <= DataHoraRecebimento
                && DataHoraRecebimento <= maxDate;
        }
    }
}
