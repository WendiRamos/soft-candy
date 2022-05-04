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
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Tipo")]
        public int Tipo { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Operação")]
        public string Operacao { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataHora { get; set; }

        [ForeignKey("Funcionario")]
        [Display(Name = "Funcionário Operação")]
        public int IdFuncionario { get; set; }

        public virtual Funcionario Funcionario{ get; set; }

        public OperacaoCaixa(decimal valor, int tipo, string operacao, string descricao, DateTime dataHora, int idFuncionario)
        {
            Valor = valor;
            Tipo = tipo;
            Operacao = operacao;
            Descricao = descricao;
            DataHora = dataHora;
            IdFuncionario = idFuncionario;
        }

        public OperacaoCaixa()
        {
        }
    }
}
