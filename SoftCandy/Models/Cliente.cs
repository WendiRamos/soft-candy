using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Cliente
    {
        [Key()]
        [Display(Name = "Id")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Nome")]
        public string NomeCliente { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(11, MinimumLength = 8, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Celular")]
        public string CelularCliente { get ; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(254, MinimumLength = 10, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Endereço")]
        public string EnderecoCliente { get; set; }

        public bool AtivoCliente { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }
        
        public Cliente()
        {
        }

        public Cliente(string nome, string celular, string endereco)
        {
            
            NomeCliente = nome;
            CelularCliente = celular;
            EnderecoCliente = endereco;
            Pedidos = null;
        }
    }
}
