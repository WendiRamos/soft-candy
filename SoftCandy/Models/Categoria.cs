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
        public int IdCategoria { get; set; }

        [Display(Name = "Nome")]
        public string NomeCategoria { get; set; }

        public bool AtivoCategoria { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}
