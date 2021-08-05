using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Models
{
    public class Cliente
    {
        [Key]
        public int Id_Cliente { get; set; }
        public string Nome { get; set; }
        public string Celular { get ; set; }
        public string Endereco { get; set; }
        
        public Cliente()
        {
        }

        public Cliente(int id, string nome, string celular, string endereco)
        {
            Id_Cliente = id;
            Nome = nome;
            Celular = celular;
            Endereco = endereco;
        }

    }

}
