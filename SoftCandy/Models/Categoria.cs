using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Categoria
    {
        [Key]
        [Display(Name = "Id")]
        public int Id_Categoria { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        public string Nome_Categoria { get; set; }
        public ICollection<Produto> Produtos { get; set; }

        public Categoria(int id_Categoria, string nome_Categoria, ICollection<Produto> produtos)
        {
            Id_Categoria = id_Categoria;
            Nome_Categoria = nome_Categoria;
            Produtos = produtos;
        }

        public Categoria()
        {
        }
    }
}
