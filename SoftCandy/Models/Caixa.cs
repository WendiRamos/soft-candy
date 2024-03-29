﻿using System;
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
        [Display(Name = "Data/Hora Abertura")]
        public DateTime DataHoraAbertura { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data/Hora Fechamento")]
        public DateTime DataHoraFechamento { get; set; }

        public bool EstaAberto { get; set; }

        [ForeignKey("Funcionario")]
        public int FuncionarioAberturaId { get; set; }
        [Display(Name = "Funcionário Abertura")]
        public virtual Funcionario FuncionarioAbertura { get; set; }


        [ForeignKey("Funcionario")]
        public int FuncionarioFechamentoId { get; set; }
        [Display(Name = "Funcionário Fechamento")]
        public virtual Funcionario FuncionarioFechamento { get; set; }


        [Display(Name = "Valor de Abertura")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Required(ErrorMessage = "{0} obrigatório")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal ValorDinheiroAbertura { get; set; }

        [Display(Name = "Valor Total do Fechamento em Dinheiro")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal ValorTotalFechamentoDinheiro { get; set; }

        [Display(Name = "Vendas em Dinheiro")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal ValorVendasDinheiro { get; set; }

        [Display(Name = "Vendas em Cartão Débito")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal ValorVendasCartaoDebito { get; set; }

        [Display(Name = "Vendas em Cartão Crédito")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal ValorVendasCartaoCredito { get; set; }

        [Display(Name = "Vendas em Pix")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal ValorVendasPix { get; set; }

        [Display(Name = "Valor Total das Vendas")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal ValorTotalVendas { get; set; }

        [Display(Name = "Operações de Entrada")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal ValorOperacoesEntrada { get; set; }

        [Display(Name = "Operações de Saída")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal ValorOperacoesSaida { get; set; }

        [Display(Name = "Valor Total das Operaçoes")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal ValorTotalOperacoes { get; set; }

        public virtual ICollection<OperacaoCaixa> Operacoes { get; set; }
        public virtual ICollection<Comanda> Comandas { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }


        public Caixa(decimal valorAbertura)
        {
            ValorDinheiroAbertura = valorAbertura;
        }

        public Caixa()
        {
        }

        private void SomarEmValorVendasDinheiro(decimal valor)
        {
            ValorVendasDinheiro = valor + ValorVendasDinheiro;
        }

        private void SomarEmValorVendasCartaoCredito(decimal valor)
        {
            ValorVendasCartaoCredito = valor + ValorVendasCartaoCredito;
        }

        private void SomarEmValorVendasCartaoDebido(decimal valor)
        {
            ValorVendasCartaoDebito = valor + ValorVendasCartaoDebito;
        }

        private void SomarEmValorVendasPix(decimal valor)
        {
            ValorVendasPix = valor + ValorVendasPix;
        }
        private void AtualizarValorTotalVendas()
        {
            ValorTotalVendas = ValorVendasCartaoCredito + ValorVendasCartaoDebito + ValorVendasPix + ValorVendasDinheiro;
        }

        private void SomarValorOperacoesEntrada(decimal valor)
        {
            ValorOperacoesEntrada = valor + ValorOperacoesEntrada;
        }

        private void SomarValorOperacoesSaida(decimal valor)
        {
            ValorOperacoesSaida = valor + ValorOperacoesSaida;
        }

        private void AtualizarValorTotalOperacoes()
        {
            ValorTotalOperacoes = ValorOperacoesEntrada - ValorOperacoesSaida;
        }

        private void AtualizarValorTotalFechamentoDinheiro()
        {
            ValorTotalFechamentoDinheiro = ValorDinheiroAbertura + ValorVendasDinheiro + ValorTotalOperacoes;
        }

        public void SomarEmValorVendas(Comanda pedido)
        {
            if (pedido.FormaPagamentoIsDinheiro())
            {
                SomarEmValorVendasDinheiro(pedido.ValorTotal);
                AtualizarValorTotalFechamentoDinheiro();
            }
            else if (pedido.FormaPagamentoIsCredito())
            {
                SomarEmValorVendasCartaoCredito(pedido.ValorTotal);
            }
            else if (pedido.FormaPagamentoIsDebito())
            {
                SomarEmValorVendasCartaoDebido(pedido.ValorTotal);
            }
            else if (pedido.FormaPagamentoIsPix())
            {
                SomarEmValorVendasPix(pedido.ValorTotal);
            }
            AtualizarValorTotalVendas();
        }

        public void SomarEmValorDelivery(Delivery delivery)
        {
            if (delivery.FormaPagamentoIsDinheiro())
            {
                SomarEmValorVendasDinheiro(delivery.ValorTotal);
                AtualizarValorTotalFechamentoDinheiro();
            }
            else if (delivery.FormaPagamentoIsCredito())
            {
                SomarEmValorVendasCartaoCredito(delivery.ValorTotal);
            }
            else if (delivery.FormaPagamentoIsDebito())
            {
                SomarEmValorVendasCartaoDebido(delivery.ValorTotal);
            }
            else if (delivery.FormaPagamentoIsPix())
            {
                SomarEmValorVendasPix(delivery.ValorTotal);
            }
            AtualizarValorTotalVendas();
        }

        public void SomarEmValorOperacoes(OperacaoCaixa operacao)
        {
            if (operacao.TipoIsEntrada())
            {
                SomarValorOperacoesEntrada(operacao.Valor);
            }
            else if (operacao.TipoIsSaida())
            {
                SomarValorOperacoesSaida(operacao.Valor);
            }
            AtualizarValorTotalOperacoes();
            AtualizarValorTotalFechamentoDinheiro();
        }

        public bool ExisteVendaPendente()
        {
            return Comandas.Any(c => c.Recebido == false)
                || Deliveries.Any(d => d.Recebido == false);
        }
    }
}