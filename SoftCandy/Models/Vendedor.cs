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

        [Display(Name = "Endereço")]
        public string EnderecoVendedor { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string EmailVendedor { get; set; }

        [Display(Name = "Senha")]
        public string SenhaVendedor { get; set; }

        public bool AtivoVendedor { get; set; }

        public Vendedor(int idVendedor, string nomeVendedor, string celularVendedor, string enderecoVendedor, string emailVendedor, string senhaVendedor)
        {
            idVendedor = IdVendedor;
            nomeVendedor = NomeVendedor;
            celularVendedor = CelularVendedor;
            enderecoVendedor = EnderecoVendedor;
            emailVendedor = EmailVendedor;
            senhaVendedor = SenhaVendedor;
        }

        public Vendedor()
        {
        }

    }
  
}
