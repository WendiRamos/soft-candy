using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Motoboy
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 10, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(4, MinimumLength = 2, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 10, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        public bool Ativo { get; set; }

        public virtual ICollection<Delivery> Deliveries { get; set; }

        public Motoboy()
        {
        }
    }
}
