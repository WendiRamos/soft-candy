﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Fornecedor
    {
        [Key()]
        public int IdFornecedor { get; set; }

        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }

        [Display(Name = "Empresa Social")]
        public string EmpresaSocial { get; set; }

        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [Display(Name = "Celular")]
        public string CelularFornecedor { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string EmailFornecedor { get; set; }

        [Display(Name = "Logradouro")]
        public string LogradouroFornecedor { get; set; }

        [Display(Name = "Número")]
        public string NumeroFornecedor { get; set; }

        [Display(Name = "Bairro")]
        public string BairroFornecedor { get; set; }

        [Display(Name = "Cidade")]
        public string CidadeFornecedor { get; set; }

        [Display(Name = "Estado")]
        public string EstadoFornecedor { get; set; }

        public bool AtivoFornecedor { get; set; }

        public ICollection<Produto> Produtos { get; set; }

        public Fornecedor()
        {
        }

        public Fornecedor(int idFornecedor, string cnpj, string empresaSocial, string nomeFantasia, string celularFornecedor, string emailFornecedor, string logradouroFornecedor, string numeroFornecedor, string bairroFornecedor, string cidadeFornecedor, string estadoFornecedor, bool ativoFornecedor, ICollection<Produto> produtos)
        {
            IdFornecedor = idFornecedor;
            Cnpj = cnpj;
            EmpresaSocial = empresaSocial;
            NomeFantasia = nomeFantasia;
            CelularFornecedor = celularFornecedor;
            EmailFornecedor = emailFornecedor;
            LogradouroFornecedor = logradouroFornecedor;
            NumeroFornecedor = numeroFornecedor;
            BairroFornecedor = bairroFornecedor;
            CidadeFornecedor = cidadeFornecedor;
            EstadoFornecedor = estadoFornecedor;
            AtivoFornecedor = ativoFornecedor;
            Produtos = produtos;
        }
    }

}