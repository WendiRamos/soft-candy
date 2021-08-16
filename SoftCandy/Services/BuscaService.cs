using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoftCandy.Data;
using SoftCandy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftCandy.Services
{
    public class BuscaService
    {
        private readonly SoftCandyContext _context;


        public BuscaService(SoftCandyContext context)
        {
            _context = context;

        }
        public async Task<List<Cliente>> FindByCliente(String Nome)
        {
            var result = from obj in _context.Cliente select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                byte[] tmp = Encoding.GetEncoding("ISO-8859-8").GetBytes(Nome);
                string pesquisa = System.Text.Encoding.UTF8.GetString(tmp);
                result = result.Where(x => x.Nome.Contains(pesquisa));
            }
            return await result
                  .ToListAsync();
        }

        public async Task<List<Vendedor>> FindByVendedor(String Nome)
        {
            var result = from obj in _context.Vendedor select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                byte[] tmp = Encoding.GetEncoding("ISO-8859-8").GetBytes(Nome);
                string pesquisa = Encoding.UTF8.GetString(tmp);
                result = result.Where(x => x.Nome_Vendedor.Contains(pesquisa));
            }
            return await result
                  .ToListAsync();
        }

        public async Task<List<Produto>> FindByProduto(String Nome)
        {
            var result = from obj in _context.Produto select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                byte[] tmp = Encoding.GetEncoding("ISO-8859-8").GetBytes(Nome);
                string pesquisa = Encoding.UTF8.GetString(tmp);
                result = result.Where(x => x.Nome_Produto.Contains(pesquisa));
            }
            return await result
                  .ToListAsync();
        }

        public async Task<List<Categoria>> FindByCategoria(String Nome)
        {
            var result = from obj in _context.Categoria select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                byte[] tmp = Encoding.GetEncoding("ISO-8859-8").GetBytes(Nome);
                string pesquisa = Encoding.UTF8.GetString(tmp);
                result = result.Where(x => x.Nome_Categoria.Contains(pesquisa));
            }
            return await result
                  .ToListAsync();
        }


    }
}