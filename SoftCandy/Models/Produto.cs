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
        [Display(Name = "Código Produto")]
        public int Cod_Produto { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Nome Produto")]
        public string Nome_Produto { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Preço Venda")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal Preco_Venda { get; set; }

        [StringLength(60, MinimumLength = 0, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        public string Descricao { get; set; }

        public ICollection<Item_Pedido> Itens_Pedidos { get; set; }

        public Produto(int cod_Produto, string nome_Produto, int quantidade, decimal preco_Venda, string descricao, ICollection<Item_Pedido> itens_Pedidos)
        {
            Cod_Produto = cod_Produto;
            Nome_Produto = nome_Produto;
            Quantidade = quantidade;
            Preco_Venda = preco_Venda;
            Descricao = descricao;
            Itens_Pedidos = itens_Pedidos;
        }

        public Produto()
        {
        }
    }

}
