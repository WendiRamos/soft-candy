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
                var softCandyContext = _context.Produto.Where(p => p.AtivoProduto).Include(p => p.Categoria).Include(p => p.Fornecedor);
                return View(await softCandyContext.ToListAsync());
            }
            return RedirectToAction("User", "Home");
        }

        public async Task<IActionResult> EstoqueBaixo()
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                var softCandyContext = _context.Produto.Where(p => p.AtivoProduto && p.QuantidadeProduto <= p.QuantidadeMinimaProduto).Include(p => p.Fornecedor);
                return View(await softCandyContext.ToListAsync());
            }
            return RedirectToAction("User", "Home");
        }


        public async Task<IActionResult> Relatorio()
        {
            if (User.Identity.IsAuthenticated)
            {
                var softCandyContext = _context.Produto.Where(c => c.AtivoProduto).Include(p => p.Categoria).Include(p => p.Fornecedor);
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
                    .FirstOrDefaultAsync(m => m.IdProduto == id);
                if (produto == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

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
        public async Task<IActionResult> Create([Bind("NomeProduto,PrecoVendaProduto,QuantidadeProduto,QuantidadeMinimaProduto,DescricaoProduto,IdCategoria,IdFornecedor")] Produto produto)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                if (ModelState.IsValid)
                {
                    produto.AtivoProduto = true;
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
        public async Task<IActionResult> Edit(int id, [Bind("IdProduto,NomeProduto,PrecoVendaProduto,QuantidadeProduto,QuantidadeMinimaProduto,DescricaoProduto,IdCategoria,IdFornecedor")] Produto produto)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {

                if (id != produto.IdProduto)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        produto.AtivoProduto = true;
                        _context.Update(produto);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProdutoExists(produto.IdProduto))
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
                    .FirstOrDefaultAsync(m => m.IdProduto == id);
                if (produto == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

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
                produto.AtivoProduto = false;
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
                    .FirstOrDefaultAsync(m => m.IdProduto == id);
                if (produto == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

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
                produto.AtivoProduto = true;
                _context.Produto.Update(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("User", "Home");
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.IdProduto == id);
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
