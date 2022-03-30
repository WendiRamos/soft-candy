using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Cliente
    {
        [Key()]
        [Display(Name = "Id")]
        public int IdCliente { get; set; }

        [Display(Name = "Nome")]
        public string NomeCliente { get; set; }

        [Display(Name = "Celular")]
        public string CelularCliente { get ; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string EmailCliente { get; set; }

        [Display(Name = "Logradouro")]
        public string LogradouroCliente { get; set; }

        [Display(Name = "Número")]
        public string NumeroCliente { get; set; }

        [Display(Name = "Bairro")]
        public string BairroCliente { get; set; }

        [Display(Name = "Cidade")]
        public string CidadeCliente { get; set; }

        [Display(Name = "Estado")]
        public string EstadoCliente { get; set; }
        public bool AtivoCliente { get; set; }

        
        public Cliente()
        {
        }

        public Cliente(int idCliente, string nomeCliente, string celularCliente, string emailCliente, string logradouroCliente, string numeroCliente, string bairroCliente, string cidadeCliente, string estadoCliente, bool ativoCliente, ICollection<Pedido> pedidos)
        {
            IdCliente = idCliente;
            NomeCliente = nomeCliente;
            CelularCliente = celularCliente;
            EmailCliente = emailCliente;
            LogradouroCliente = logradouroCliente;
            NumeroCliente = numeroCliente;
            BairroCliente = bairroCliente;
            CidadeCliente = cidadeCliente;
            EstadoCliente = estadoCliente;
            AtivoCliente = ativoCliente;
        }
    }
}
