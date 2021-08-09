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

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Nome")]
        public string Nome_Vendedor { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(11, MinimumLength = 8, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Celular")]
        public string Celular_Vendedor { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(254, MinimumLength = 10, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Endereço")]
        public string Endereco_Vendedor { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido!")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email_Vendedor { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
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
