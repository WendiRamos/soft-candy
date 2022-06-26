using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Fornecedor
    {
        [Key()]
        [Display(Name = "Id")]
        public int IdFornecedor { get; set; }

        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(90, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Celular")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O tamanho do {0} deve ser 11 caracteres.")]
        public string CelularFornecedor { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [EmailAddress(ErrorMessage = "Entre um e-mail válido!")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string EmailFornecedor { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 4, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Logradouro")]
        public string LogradouroFornecedor { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(6, MinimumLength = 1, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Número")]
        public string NumeroFornecedor { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 4, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Bairro")]
        public string BairroFornecedor { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 4, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Cidade")]
        public string CidadeFornecedor { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(29, MinimumLength = 2, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Estado")]
        public string EstadoFornecedor { get; set; }

        public bool AtivoFornecedor { get; set; }

        public ICollection<Produto> Produtos { get; set; }

        public Fornecedor()
        {
        }

        public Fornecedor(string cnpj, string razaoSocial, string nomeFantasia, string celularFornecedor, string emailFornecedor, string logradouroFornecedor, string numeroFornecedor, string bairroFornecedor, string cidadeFornecedor, string estadoFornecedor)
        {
            Cnpj = cnpj;
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            CelularFornecedor = celularFornecedor;
            EmailFornecedor = emailFornecedor;
            LogradouroFornecedor = logradouroFornecedor;
            NumeroFornecedor = numeroFornecedor;
            BairroFornecedor = bairroFornecedor;
            CidadeFornecedor = cidadeFornecedor;
            EstadoFornecedor = estadoFornecedor;
        }
    }

}
