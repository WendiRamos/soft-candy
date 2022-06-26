using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Funcionario
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Celular")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O tamanho do {0} deve ser 11 caracteres.")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 4, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(6, MinimumLength = 1, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 4, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 4, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(29, MinimumLength = 2, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [EmailAddress(ErrorMessage = "Entre um e-mail válido!")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email{ get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [NotMapped]
        [Display(Name = "Confirmar Senha")]
        [Required(ErrorMessage = "{0} obrigatório")]
        [Compare("Senha",ErrorMessage = "Senhas não coincidem!")]
        [DataType(DataType.Password)]
        public string ConfirmarSenha { get; set; }

        public int Cargo { get; set; }

        public bool Ativo { get; set; }

        public Funcionario()
        {
        }

        public Funcionario(string nome, string celular, string logradouro, string numero, string bairro, string cidade, string estado, string email, string senha, int cargo, bool ativo)
        {
            Nome = nome;
            Celular = celular;
            Logradouro = logradouro;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Email = email;
            Senha = senha;
            Cargo = cargo;
            Ativo = ativo;
        }
    }
}
