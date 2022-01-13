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
            var result = from obj in _context.Cliente.Where(c => c.AtivoCliente && c.IdCliente != 1) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                byte[] tmp = Encoding.GetEncoding("ISO-8859-8").GetBytes(Nome);
                string pesquisa = System.Text.Encoding.UTF8.GetString(tmp);
                result = result.Where(x => x.NomeCliente.Contains(pesquisa));
            }
            return await result
                  .ToListAsync();
        }

        public async Task<List<Cliente>> FindByClienteApagado(String Nome)
        {
            var result = from obj in _context.Cliente.Where(c => c.AtivoCliente == false) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                byte[] tmp = Encoding.GetEncoding("ISO-8859-8").GetBytes(Nome);
                string pesquisa = System.Text.Encoding.UTF8.GetString(tmp);
                result = result.Where(x => x.NomeCliente.Contains(pesquisa));
            }
            return await result
                  .ToListAsync();
        }

        public async Task<List<Vendedor>> FindByVendedor(String Nome)
        {
            var result = from obj in _context.Vendedor.Where(c => c.AtivoVendedor) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                byte[] tmp = Encoding.GetEncoding("ISO-8859-8").GetBytes(Nome);
                string pesquisa = Encoding.UTF8.GetString(tmp);
                result = result.Where(x => x.NomeVendedor.Contains(pesquisa));
            }
            return await result
                  .ToListAsync();
        }

        public async Task<List<Vendedor>> FindByVendedorApagado(String Nome)
        {
            var result = from obj in _context.Vendedor.Where(c => c.AtivoVendedor == false) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                byte[] tmp = Encoding.GetEncoding("ISO-8859-8").GetBytes(Nome);
                string pesquisa = Encoding.UTF8.GetString(tmp);
                result = result.Where(x => x.NomeVendedor.Contains(pesquisa));
            }
            return await result
                  .ToListAsync();
        }

        public async Task<List<Estoquista>> FindByEstoquista(String Nome)
        {
            var result = from obj in _context.Estoquista.Where(c => c.AtivoEstoquista) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                byte[] tmp = Encoding.GetEncoding("ISO-8859-8").GetBytes(Nome);
                string pesquisa = Encoding.UTF8.GetString(tmp);
                result = result.Where(x => x.NomeEstoquista.Contains(pesquisa));
            }
            return await result
                  .ToListAsync();
        }

        public async Task<List<Estoquista>> FindByEstoquistaApagado(String Nome)
        {
            var result = from obj in _context.Estoquista.Where(c => c.AtivoEstoquista == false) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                byte[] tmp = Encoding.GetEncoding("ISO-8859-8").GetBytes(Nome);
                string pesquisa = Encoding.UTF8.GetString(tmp);
                result = result.Where(x => x.NomeEstoquista.Contains(pesquisa));
            }
            return await result
                  .ToListAsync();
        }

         public async Task<List<Administrador>> FindByAdministrador(String Nome)
        {
            var result = from obj in _context.Administrador.Where(c => c.AtivoAdministrador) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                byte[] tmp = Encoding.GetEncoding("ISO-8859-8").GetBytes(Nome);
                string pesquisa = Encoding.UTF8.GetString(tmp);
                result = result.Where(x => x.NomeAdministrador.Contains(pesquisa));
            }
            return await result
                  .ToListAsync();
        }

        public async Task<List<Administrador>> FindByAdministradorApagado(String Nome)
        {
            var result = from obj in _context.Administrador.Where(c => c.AtivoAdministrador == false) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                byte[] tmp = Encoding.GetEncoding("ISO-8859-8").GetBytes(Nome);
                string pesquisa = Encoding.UTF8.GetString(tmp);
                result = result.Where(x => x.NomeAdministrador.Contains(pesquisa));
            }
            return await result
                  .ToListAsync();
        }

        public async Task<List<Produto>> FindByProduto(String Nome)
        {
            var result = from obj in _context.Produto.Where(c => c.AtivoProduto) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                byte[] tmp = Encoding.GetEncoding("ISO-8859-8").GetBytes(Nome);
                string pesquisa = Encoding.UTF8.GetString(tmp);
                result = result.Where(x => x.NomeProduto.Contains(pesquisa));
            }
            return await result
                  .ToListAsync();
        }

        public List<Produto> FindByProdutoTop5(String Nome)
        {
            var result = from obj in _context.Produto
                         .Where(c => c.AtivoProduto && c.QuantidadeProduto > 0) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                byte[] tmp = Encoding.GetEncoding("ISO-8859-8").GetBytes(Nome);
                string pesquisa = Encoding.UTF8.GetString(tmp);
                result = result.Where(x => x.NomeProduto.Contains(pesquisa));
            }
            return  result.Take(5).ToList();
        }

        public async Task<List<Produto>> FindByProdutoApagado(String Nome)
        {
            var result = from obj in _context.Produto.Where(c => c.AtivoProduto == false) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                byte[] tmp = Encoding.GetEncoding("ISO-8859-8").GetBytes(Nome);
                string pesquisa = Encoding.UTF8.GetString(tmp);
                result = result.Where(x => x.NomeProduto.Contains(pesquisa));
            }
            return await result
                  .ToListAsync();
        }

        public async Task<List<Categoria>> FindByCategoria(String Nome)
        {
            var result = from obj in _context.Categoria.Where(c => c.AtivoCategoria) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                byte[] tmp = Encoding.GetEncoding("ISO-8859-8").GetBytes(Nome);
                string pesquisa = Encoding.UTF8.GetString(tmp);
                result = result.Where(x => x.NomeCategoria.Contains(pesquisa));
            }
            return await result
                  .ToListAsync();
        }

        public async Task<List<Categoria>> FindByCategoriaApagada(String Nome)
        {
            var result = from obj in _context.Categoria.Where(c => c.AtivoCategoria == false) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                byte[] tmp = Encoding.GetEncoding("ISO-8859-8").GetBytes(Nome);
                string pesquisa = Encoding.UTF8.GetString(tmp);
                result = result.Where(x => x.NomeCategoria.Contains(pesquisa));
            }
            return await result
                  .ToListAsync();
        }

        public async Task<List<Fornecedor>> FindByFornecedor(String Nome)
        {
            var result = from obj in _context.Fornecedor.Where(c => c.AtivoFornecedor) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                byte[] tmp = Encoding.GetEncoding("ISO-8859-8").GetBytes(Nome);
                string pesquisa = Encoding.UTF8.GetString(tmp);
                result = result.Where(x => x.RazaoSocial.Contains(pesquisa));
            }
            return await result
                  .ToListAsync();
        }

        public async Task<List<Fornecedor>> FindByFornecedorApagado(String Nome)
        {
            var result = from obj in _context.Fornecedor.Where(c => c.AtivoFornecedor == false) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                byte[] tmp = Encoding.GetEncoding("ISO-8859-8").GetBytes(Nome);
                string pesquisa = Encoding.UTF8.GetString(tmp);
                result = result.Where(x => x.RazaoSocial.Contains(pesquisa));
            }
            return await result
                  .ToListAsync();
        }

        public async Task<List<Pedido>> FindByPedido(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.Pedido.Where(c => c.AtivoPedido) select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.DataPedido >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.DataPedido <= maxDate.Value);
            }
            return await result
                .Include(x => x.Cliente)
                .OrderByDescending(x => x.DataPedido)
                .ToListAsync();
        }
    }
}