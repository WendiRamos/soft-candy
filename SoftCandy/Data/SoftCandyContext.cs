using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftCandy.Models;

namespace SoftCandy.Data
{
    public class SoftCandyContext : DbContext
    {
        public SoftCandyContext (DbContextOptions<SoftCandyContext> options)
            : base(options)
        {
        }

        public DbSet<SoftCandy.Models.Cliente> Cliente { get; set; }

        public DbSet<SoftCandy.Models.Produto> Produto { get; set; }

        public DbSet<SoftCandy.Models.Pedido> Pedido { get; set; }

        public DbSet<SoftCandy.Models.Vendedor> Vendedor { get; set; }

        public DbSet<SoftCandy.Models.Item_Pedido> Item_Pedido { get; set; }

        public DbSet<SoftCandy.Models.Categoria> Categoria { get; set; }
    }
}
