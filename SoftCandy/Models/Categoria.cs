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
        [Display(Name = "Id Categoria")]
        public int IdCategoria { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        public string NomeCategoria { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}
