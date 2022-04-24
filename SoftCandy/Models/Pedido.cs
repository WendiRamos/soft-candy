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
        public int IdPedido { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Valor Total")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal ValorTotalPedido { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Data Pedido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime DataPedido { get; set; }

        public bool AtivoPedido { get; set; }


        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

        public virtual Cliente Cliente { get; set; }


        [ForeignKey("Funcionario")]
        public int IdFuncionario { get; set; }

        public virtual Funcionario Funcionario { get; set; }


        [ForeignKey("Caixa")]
        public int IdCaixa { get; set; }

        public virtual Caixa Caixa { get; set; }


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
                    soma += item.PrecoPago * item.Quantidade;
                }
            }
            ValorTotalPedido = soma;
        }

    }
}
