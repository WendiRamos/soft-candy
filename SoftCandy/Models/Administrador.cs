using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Administrador
    {
        [Key]
        [Display(Name = "Id")]
        public int IdAdministrador { get; set; }

        [Display(Name = "Nome")]
        public string NomeAdministrador { get; set; }

        [Display(Name = "Celular")]
        public string CelularAdministrador { get; set; }

        [Display(Name = "Logradouro")]
        public string LogradouroAdministrador { get; set; }

        [Display(Name = "Número")]
        public string NumeroAdministrador { get; set; }

        [Display(Name = "Bairro")]
        public string BairroAdministrador { get; set; }

        [Display(Name = "Cidade")]
        public string CidadeAdministrador { get; set; }

        [Display(Name = "Estado")]
        public string EstadoAdministrador { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string EmailAdministrador { get; set; }

        [Display(Name = "Senha")]
        public string SenhaAdministrador { get; set; }

        public bool AtivoAdministrador { get; set; }

        public Administrador(string nomeAdministrador, string celularAdministrador, string logradouroAdministrador, string numeroAdministrador, string bairroAdministrador, string cidadeAdministrador, string estadoAdministrador, string emailAdministrador, string senhaAdministrador, bool ativoAdministrador)
        {
            NomeAdministrador = nomeAdministrador;
            CelularAdministrador = celularAdministrador;
            LogradouroAdministrador = logradouroAdministrador;
            NumeroAdministrador = numeroAdministrador;
            BairroAdministrador = bairroAdministrador;
            CidadeAdministrador = cidadeAdministrador;
            EstadoAdministrador = estadoAdministrador;
            EmailAdministrador = emailAdministrador;
            SenhaAdministrador = senhaAdministrador;
            AtivoAdministrador = ativoAdministrador;
        }

        public Administrador()
        {
        }

    }
  
}
