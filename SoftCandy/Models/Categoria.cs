using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftCandy.Models
{
    public class Categoria
    {
        [Key]
        [Display(Name = "Id")]
        public int IdCategoria { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        public string NomeCategoria { get; set; }

        public bool AtivoCategoria { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}
