using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoftCandy.Data;
using SoftCandy.Models;
using SoftCandy.Utils;

namespace SoftCandy.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly SoftCandyContext _context;

        public ProdutoController(SoftCandyContext context)
        {
            _context = context;
        }

        // GET: Produto
        public async Task<IActionResult> Index()
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                var produtos = await _context.Produto
                    .Where(p => p.Ativo)
                    .Include(p => p.Categoria)
                    .Include(p => p.Fornecedor)
                    .Include(p => p.Lotes)
                    .Take(20).ToListAsync();
                produtos.ForEach(p => p.SomarQuantidade());

                ViewData["MostrarAlerta"] = produtos
                   .Any(p => p.EstaEscasso() || p.MostrarNoCardVencido());

                return View(produtos);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        public async Task<IActionResult> EstoqueBaixoVencido()
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                var produtos = await _context.Produto
                    .Include(p => p.Lotes)
                    .Where(p => p.Ativo)
                    .ToListAsync();

                ViewData["ProdEscassos"] = produtos
                    .Where(p => p.EstaEscasso());

                ViewData["LotesVencidos"] = produtos
                    .SelectMany(p => p.Lotes)
                    .Where(lt => lt.MostrarNoCardVencido());

                produtos.ForEach(p => p.SomarQuantidade());
                return View();
            }
            return RedirectToAction("Login", "Funcionario");
        }


        public async Task<IActionResult> Relatorio(string tipo)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                List<Produto> produtos;

                if (tipo == "maisDescartados")
                {
                    produtos = await _context.Produto
                        .Where(c => c.Ativo)
                        .Include(c => c.Lotes)
                        .Include(c => c.Categoria)
                        .OrderByDescending(c => c.QuantidadeDescartada)
                        .ToListAsync();
                }
                else if (tipo == "menosDescartados")
                {
                    produtos = await _context.Produto
                        .Where(c => c.Ativo)
                        .Include(c => c.Lotes)
                        .Include(c => c.Categoria)
                        .OrderBy(c => c.QuantidadeDescartada)
                        .ToListAsync();
                }
                else if (tipo == "maisEstoque")
                {
                    produtos = await _context.Produto
                        .Where(c => c.Ativo)
                        .Include(c => c.Lotes)
                        .Include(c => c.Categoria)
                        .OrderByDescending(c => c.Lotes.Select(l => l.QuantidadeEstoque).Sum())
                        .ToListAsync();
                }
                else if (tipo == "menosEstoque")
                {
                    produtos = await _context.Produto
                        .Where(c => c.Ativo)
                        .Include(c => c.Lotes)
                        .Include(c => c.Categoria)
                        .OrderBy(c => c.Lotes.Select(l => l.QuantidadeEstoque).Sum())
                        .ToListAsync();
                }
                else
                {
                    produtos = await _context.Produto
                        .Include(c => c.Lotes)
                        .Include(c => c.Categoria)
                        .Where(c => c.Ativo).ToListAsync();
                }

                produtos.ForEach(p => p.QuantidadeEstoque = p.Lotes.Select(l=> l.QuantidadeEstoque).Sum());
                ViewData["Selecionado"] = tipo;
                return View(produtos);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Produto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var produto = await _context.Produto
                    .Include(p => p.Categoria)
                    .Include(p => p.Fornecedor)
                    .Include(p => p.Lotes)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (produto == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                produto.SomarQuantidade();
                return View(produto);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Produto/Create
        public IActionResult Create()
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                ViewData["CAT"] = new SelectList(_context.Categoria, "IdCategoria", "NomeCategoria");
                ViewData["FOR"] = new SelectList(_context.Fornecedor, "IdFornecedor", "RazaoSocial");
                return View();
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // POST: Produto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,QuantidadeMinima,IdCategoria,IdFornecedor,Medida")] Produto produto)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                if (ModelState.IsValid)
                {
                    produto.Ativo = true;
                    _context.Add(produto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CAT"] = new SelectList(_context.Categoria, "IdCategoria", "NomeCategoria");
                ViewData["FOR"] = new SelectList(_context.Fornecedor, "IdFornecedor", "RazaoSocial");
                return View(produto);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Produto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var produto = await _context.Produto.FindAsync(id);
                if (produto == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                ViewData["CAT"] = new SelectList(_context.Categoria, "IdCategoria", "NomeCategoria");
                ViewData["FOR"] = new SelectList(_context.Fornecedor, "IdFornecedor", "RazaoSocial");
                return View(produto);

            }
            return RedirectToAction("Login", "Funcionario");
        }

        // POST: Produto/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,QuantidadeMinima,IdCategoria,IdFornecedor, Medida")] Produto produto)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {

                if (id != produto.Id)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        produto.Ativo = true;
                        _context.Update(produto);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProdutoExists(produto.Id))
                        {
                            return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(produto);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Produto/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var produto = await _context.Produto
                    .Include(p => p.Categoria)
                    .Include(p => p.Fornecedor)
                    .Include(p => p.Lotes)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (produto == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                produto.SomarQuantidade();
                return View(produto);

            }
            return RedirectToAction("Login", "Funcionario");
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                var produto = await _context.Produto.FindAsync(id);
                produto.Ativo = false;
                _context.Produto.Update(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Produto/Restore
        public async Task<IActionResult> Restore(int? id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }
                var produto = await _context.Produto
                    .Include(p => p.Categoria)
                    .Include(p => p.Fornecedor)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (produto == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                return View(produto);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // POST: Produto/Restore
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRestore(int id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                var produto = await _context.Produto.FindAsync(id);
                produto.Ativo = true;
                _context.Produto.Update(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Login", "Funcionario");
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.Id == id);
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
