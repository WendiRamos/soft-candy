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
                return View(produtos);
            }
            return RedirectToAction("User", "Home");
        }

        public async Task<IActionResult> EstoqueBaixo()
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                var produtos = await _context.Produto
                    .Include(p => p.Lotes)
                    .Where(p => p.Ativo
                    && (p.Lotes.Count == 0
                    || p.Lotes.Select(lt => lt.QuantidadeEstoque).Sum() <= p.QuantidadeMinima))
                    .ToListAsync();

                produtos.ForEach(p => p.SomarQuantidade());
                return View(produtos);
            }
            return RedirectToAction("User", "Home");
        }


        public async Task<IActionResult> Relatorio()
        {
            if (User.Identity.IsAuthenticated)
            {
                var softCandyContext = _context.Produto.Where(c => c.Ativo).Include(p => p.Categoria).Include(p => p.Fornecedor);
                return View(await softCandyContext.ToListAsync());
            }
            return RedirectToAction("Index", "Home");
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
            return RedirectToAction("User", "Home");
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
            return RedirectToAction("User", "Home");
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
            return RedirectToAction("User", "Home");
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
            return RedirectToAction("User", "Home");
        }

        // POST: Produto/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,QuantidadeMinima,IdCategoria,IdFornecedor, Medida")] Produto produto)
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
                ViewData["CAT"] = new SelectList(_context.Categoria, "IdCategoria", "NomeCategoria");
                ViewData["FOR"] = new SelectList(_context.Fornecedor, "IdFornecedor", "RazaoSocial");
                return View(produto);
            }
            return RedirectToAction("User", "Home");
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
            return RedirectToAction("User", "Home");
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
                produto.SomarQuantidade();
                _context.Produto.Update(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("User", "Home");
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
                produto.SomarQuantidade();
                return View(produto);
            }
            return RedirectToAction("User", "Home");
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
            return RedirectToAction("User", "Home");
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
