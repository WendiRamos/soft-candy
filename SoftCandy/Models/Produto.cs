using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Produto
    {
        [Key]
        public int Cod_Produto { get; set; }
        public string Nome_Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco_Venda { get; set; }
        public string Descricao { get; set; }
        
       

        public Produto(int cod_Produto, string nome_Produto, int quantidade, decimal preco_Venda, string descricao)
        {
            Cod_Produto = cod_Produto;
            Nome_Produto = nome_Produto;
            Quantidade = quantidade;
            Preco_Venda = preco_Venda;
            Descricao = descricao;
        }

        public Produto()
        {
        }
    }

}
