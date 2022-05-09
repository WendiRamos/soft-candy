using SoftCandy.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class OperacaoCaixa
    {
        [Key()]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Valor")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Tipo")]
        public OperacoesEnum Tipo { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataHora { get; set; }

        [ForeignKey("Funcionario")]
        [Display(Name = "Funcionário Operação")]
        public int IdFuncionario { get; set; }

        public virtual Funcionario Funcionario { get; set; }

        [ForeignKey("Caixa")]
        [Display(Name = "Caixa")]
        public int IdCaxa { get; set; }

        public virtual Caixa Caixa { get; set; }
        public OperacaoCaixa(decimal valor, OperacoesEnum tipo, string operacao, string descricao, DateTime dataHora, int idFuncionario)
        {
            Valor = valor;
            Tipo = tipo;
            Nome = operacao;
            Descricao = descricao;
            DataHora = dataHora;
            IdFuncionario = idFuncionario;
        }
        public OperacaoCaixa()
        {
        }

        public bool TipoIsEntrada()
        {
            return Tipo == OperacoesEnum.ENTRADA;
        }

        public bool TipoIsSaida()
        {
            return Tipo == OperacoesEnum.SAIDA;
        }
    }
}
