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
        public int Id_Cliente { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(11, MinimumLength = 8, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        public string Celular { get ; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(254, MinimumLength = 10, ErrorMessage = "O tamanho do {0} deve estar entre {2} e {1}.")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }


        
        public Cliente()
        {
        }

        public Cliente(int id_Cliente, string nome, string celular, string endereco, ICollection<Pedido> pedidos)
        {
            Id_Cliente = id_Cliente;
            Nome = nome;
            Celular = celular;
            Endereco = endereco;
            Pedidos = pedidos;
        }
    }

}
