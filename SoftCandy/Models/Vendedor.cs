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
        [Display(Name = "Id Vendedor")]
        public int Id_Vendedor { get; set; }

        [Display(Name = "Nome")]
        public string Nome_Vendedor { get; set; }

        [Display(Name = "Celular")]
        public string Celular_Vendedor { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco_Vendedor { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email_Vendedor { get; set; }

        [Display(Name = "Senha")]
        public string Senha_Vendedor { get; set; }

        public Vendedor(int id_Vendedor, string nome_Vendedor, string celular_Vendedor, string endereco_Vendedor, string email_Vendedor, string senha_Vendedor)
        {
            Id_Vendedor = id_Vendedor;
            Nome_Vendedor = nome_Vendedor;
            Celular_Vendedor = celular_Vendedor;
            Endereco_Vendedor = endereco_Vendedor;
            Email_Vendedor = email_Vendedor;
            Senha_Vendedor = senha_Vendedor;
        }

        public Vendedor()
        {
        }

    }
  
}
