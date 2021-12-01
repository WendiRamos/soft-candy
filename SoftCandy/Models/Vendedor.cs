using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Vendedor
    {
        [Key]
        [Display(Name = "Id")]
        public int IdVendedor { get; set; }

        [Display(Name = "Nome")]
        public string NomeVendedor { get; set; }

        [Display(Name = "Celular")]
        public string CelularVendedor { get; set; }

        [Display(Name = "Logradouro")]
        public string LogradouroVendedor { get; set; }

        [Display(Name = "Número")]
        public string NumeroVendedor { get; set; }

        [Display(Name = "Bairro")]
        public string BairroVendedor { get; set; }

        [Display(Name = "Cidade")]
        public string CidadeVendedor { get; set; }

        [Display(Name = "Estado")]
        public string EstadoVendedor { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string EmailVendedor { get; set; }

        [Display(Name = "Senha")]
        public string SenhaVendedor { get; set; }

        public bool AtivoVendedor { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }

        public Vendedor(string nomeVendedor, string celularVendedor, string logradouroVendedor, string numeroVendedor, string bairroVendedor, string cidadeVendedor, string estadoVendedor, string emailVendedor, string senhaVendedor, bool ativoVendedor)
        {
            NomeVendedor = nomeVendedor;
            CelularVendedor = celularVendedor;
            LogradouroVendedor = logradouroVendedor;
            NumeroVendedor = numeroVendedor;
            BairroVendedor = bairroVendedor;
            CidadeVendedor = cidadeVendedor;
            EstadoVendedor = estadoVendedor;
            EmailVendedor = emailVendedor;
            SenhaVendedor = senhaVendedor;
            AtivoVendedor = ativoVendedor;
        }

        public Vendedor()
        {
        }

    }
  
}
