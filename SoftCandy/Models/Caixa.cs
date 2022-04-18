using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Caixa
    {
        [Key()]
        [Display(Name = "Id")]
        public int IdCaixa { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataHoraAbertura { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataHoraFechamento { get; set; }

        [Display(Name = "Valor de Abertura:")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal ValorAbertura { get; set; }


        [Display(Name = "Valor de Fechamento:")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal ValorFechamento { get; set; }

        public bool EstaAberto { get; set; }

        [ForeignKey("Funcionario")]
        [Display(Name = "Funcionário Abertura")]
        public int FuncionarioAberturaId { get; set; }

        public virtual Funcionario FuncionarioAbertura { get; set; }


        [ForeignKey("Funcionario")]
        [Display(Name = "Funcionário Fechamento")]
        public int FuncionarioFechamentoId { get; set; }

        public virtual Funcionario FuncionarioFechamento { get; set; }


        public ICollection<OperacaoCaixa> Operacoes { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }


        public Caixa(decimal valorAbertura)
        {
            ValorAbertura = valorAbertura;
        }

        public Caixa()
        {
        }

        private decimal CalcularTotalPedidos()
        {
            decimal soma = 0;
            foreach (Pedido p in Pedidos)
            {
                soma += p.ValorTotalPedido;
            }
            return soma;
        }

        private decimal CalcularTotalOperacoes()
        {
            decimal soma = 0;
            foreach (OperacaoCaixa o in Operacoes)
            {
                soma += o.Valor;
            }
            return soma;
        }

        public void AtualizaValorFechamento()
        {
            ValorFechamento = CalcularTotalOperacoes() + CalcularTotalPedidos();
        }
    }
}
