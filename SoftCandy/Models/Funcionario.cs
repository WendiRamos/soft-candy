using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Funcionario
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }

        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email{ get; set; }

        [Display(Name = "Senha")]
        public string Senha { get; set; }

        public int Cargo { get; set; }

        public bool Ativo { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }

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
