using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoftCandy.Data;
using SoftCandy.Enums;
using SoftCandy.Models;
using SoftCandy.Utils;
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
      
        public async Task<List<Funcionario>> FindByNomeVendedor(String Nome)
        {
            var result = from obj in _context.Funcionario.Where(c => Validacao.IsVendedorAtivo(c)) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                result = result.Where(x => Texto.CaseInsensitiveContains(x.Nome, Nome));
            }
            return await result.ToListAsync();
        }

        public async Task<List<Funcionario>> FindByNomeVendedorApagado(String Nome)
        {
            var result = from obj in _context.Funcionario.Where(c => Validacao.IsVendedorInativo(c)) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                result = result.Where(x => Texto.CaseInsensitiveContains(x.Nome, Nome));
            }
            return await result.ToListAsync();
        }

        public async Task<List<Funcionario>> FindByNomeEstoquista(String Nome)
        {
            var result = from obj in _context.Funcionario.Where(c => Validacao.IsEstoquistaAtivo(c)) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                result = result.Where(x => Texto.CaseInsensitiveContains(x.Nome, Nome));
            }
            return await result.ToListAsync();
        }

        public async Task<List<Funcionario>> FindByNomeEstoquistaApagado(String Nome)
        {
            var result = from obj in _context.Funcionario.Where(c => Validacao.IsEstoquistaInativo(c)) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                result = result.Where(x => Texto.CaseInsensitiveContains(x.Nome, Nome));
            }
            return await result.ToListAsync();
        }

        public async Task<List<Funcionario>> FindByNomeAdministrador(String Nome)
        {
            var result = from obj in _context.Funcionario.Where(c=> Validacao.IsAdministradorAtivo(c)) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                result = result.Where(x => Texto.CaseInsensitiveContains(x.Nome, Nome));
            }
            return await result.ToListAsync();
        }

        public async Task<List<Funcionario>> FindByNomeAdministradorApagado(String Nome)
        {
            var result = from obj in _context.Funcionario.Where(c => Validacao.IsAdministradorInativo(c)) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                result = result.Where(x => Texto.CaseInsensitiveContains(x.Nome, Nome));
            }
            return await result.ToListAsync();
        }

        public async Task<List<Produto>> FindByNome(String Nome)
        {
            var result = from obj in _context.Produto.Where(c => Validacao.IsProdutoAtivo(c)) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                result = result.Where(x => Texto.CaseInsensitiveContains(x.Nome, Nome));
            }
            return await result.ToListAsync();
        }

        public List<Produto> FindByNomeTop5(String Nome)
        {
            var result = from obj in _context.Produto
                         .Where(c => Validacao.IsProdutoAtivo(c) && c.QuantidadeDescartada > 0)
                         select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                result = result.Where(x => Texto.CaseInsensitiveContains(x.Nome, Nome));
            }
            return result.Take(5).ToList();
        }

        public async Task<List<Produto>> FindByNomeApagado(String Nome)
        {
            var result = from obj in _context.Produto.Where(c => Validacao.IsProdutoInativo(c)) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                result = result.Where(x => Texto.CaseInsensitiveContains(x.Nome, Nome));
            }
            return await result.ToListAsync();
        }

        public async Task<List<Categoria>> FindByNomeCategoria(String Nome)
        {
            var result = from obj in _context.Categoria.Where(c => Validacao.IsCategoriaAtivo(c)) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                result = result.Where(x => Texto.CaseInsensitiveContains(x.NomeCategoria, Nome));
            }
            return await result.ToListAsync();
        }

        public async Task<List<Categoria>> FindByNomeCategoriaApagada(String Nome)
        {
            var result = from obj in _context.Categoria.Where(c => Validacao.IsCategoriaInativo(c)) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                result = result.Where(x => Texto.CaseInsensitiveContains(x.NomeCategoria, Nome));
            }
            return await result.ToListAsync();
        }

        public async Task<List<Fornecedor>> FindByNomeFornecedor(String Nome)
        {
            var result = from obj in _context.Fornecedor.Where(c => Validacao.IsFornecedorAtivo(c)) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                result = result.Where(x => Texto.CaseInsensitiveContains(x.RazaoSocial, Nome));
            }
            return await result.ToListAsync();
        }

        public async Task<List<Fornecedor>> FindByNomeFornecedorApagado(String Nome)
        {
            var result = from obj in _context.Fornecedor.Where(c => Validacao.IsFornecedorInativo(c)) select obj;
            if (!string.IsNullOrEmpty(Nome))
            {
                result = result.Where(x => Texto.CaseInsensitiveContains(x.RazaoSocial, Nome));
            }
            return await result.ToListAsync();
        }

        public async Task<List<Comanda>> FindByPedido(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.Comanda  select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.DataHoraRecebimento >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.DataHoraRecebimento <= maxDate.Value);
            }
            return await result
                .OrderByDescending(x => x.DataHoraRecebimento)
                .ToListAsync();
        }

        public async Task<List<Caixa>> FindByCaixa(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.Caixa.Where(c => !c.EstaAberto) select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.DataHoraFechamento >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.DataHoraFechamento <= maxDate.Value);
            }
            return await result
                .OrderByDescending(x => x.DataHoraFechamento)
                .ToListAsync();
        }
    }
}