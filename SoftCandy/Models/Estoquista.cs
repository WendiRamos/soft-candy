using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Estoquista
    {
        [Key]
        [Display(Name = "Id")]
        public int IdEstoquista { get; set; }

        [Display(Name = "Nome")]
        public string NomeEstoquista { get; set; }

        [Display(Name = "Celular")]
        public string CelularEstoquista { get; set; }

        [Display(Name = "Logradouro")]
        public string LogradouroEstoquista { get; set; }

        [Display(Name = "Número")]
        public string NumeroEstoquista { get; set; }

        [Display(Name = "Bairro")]
        public string BairroEstoquista { get; set; }

        [Display(Name = "Cidade")]
        public string CidadeEstoquista { get; set; }

        [Display(Name = "Estado")]
        public string EstadoEstoquista { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string EmailEstoquista { get; set; }

        [Display(Name = "Senha")]
        public string SenhaEstoquista { get; set; }

        public bool AtivoEstoquista { get; set; }

        public Estoquista(string nomeEstoquista, string celularEstoquista, string logradouroEstoquista, string numeroEstoquista, string bairroEstoquista, string cidadeEstoquista, string estadoEstoquista, string emailEstoquista, string senhaEstoquista, bool ativoEstoquista)
        {
            NomeEstoquista = nomeEstoquista;
            CelularEstoquista = celularEstoquista;
            LogradouroEstoquista = logradouroEstoquista;
            NumeroEstoquista = numeroEstoquista;
            BairroEstoquista = bairroEstoquista;
            CidadeEstoquista = cidadeEstoquista;
            EstadoEstoquista = estadoEstoquista;
            EmailEstoquista = emailEstoquista;
            SenhaEstoquista = senhaEstoquista;
            AtivoEstoquista = ativoEstoquista;
        }

        public Estoquista()
        {
        }

    }
  
}
