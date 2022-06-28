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
    public class LoteController : Controller
    {
        private readonly SoftCandyContext _context;

        public LoteController(SoftCandyContext context)
        {
            _context = context;
        }

        // GET: Lote/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
                }

                var lote = await _context.Lote
                    .Include(l => l.Produto)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (lote == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Lote não foi fornecido!" });
                }

                return View(lote);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Lote/Create
        public async Task<IActionResult> Create(int? id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    ViewData["ListaProdutos"] = new SelectList(_context.Produto.Where(p => p.Ativo), "Id", "Nome");
                    return View();
                }

                var produto = await _context.Produto.FirstOrDefaultAsync(p => p.Id == id);

                if (produto == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Produto não foi fornecido!" });
                }

                ViewData["NomeProduto"] = produto.Nome;
                ViewData["IdProduto"] = id;
                return View();
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // POST: Lote/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuantidadeEstoque,DataHoraFabricacao,PrecoCompra,PrecoVenda,IdProduto,DiasVencimento")] Lote lote)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                if (ModelState.IsValid)
                {
                    lote.Ativo = true;
                    lote.DataHoraValidade = lote.DataHoraFabricacao.AddDays(lote.DiasVencimento);
                    _context.Add(lote);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Produto", new { id = lote.IdProduto });
                }
                ViewData["IdProduto"] = new SelectList(_context.Produto, "Id", "Nome", lote.IdProduto);
                return View(lote);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Lote/Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                var lote = await _context.Lote
                .Include(lt => lt.Produto)
                .FirstOrDefaultAsync(lt => lt.Id == id);

                if (lote == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Lote não foi fornecido!" });
                }

                lote.DiasVencimento = (lote.DataHoraValidade - lote.DataHoraFabricacao).Days;

                ViewData["IdProduto"] = lote.Produto.Id;
                ViewData["NomeProduto"] = lote.Produto.Nome;
                return View(lote);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // POST: Lote/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuantidadeEstoque,DataHoraFabricacao,DiasVencimento,PrecoCompra,PrecoVenda,IdProduto")] Lote lote)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                if (id != lote.Id)
                {
                    return RedirectToAction(nameof(Error), new { message = "Lote não foi fornecido!" });
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        lote.Ativo = true;
                        lote.DataHoraValidade = lote.DataHoraFabricacao.AddDays(lote.DiasVencimento);
                        _context.Update(lote);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!LoteExists(lote.Id))
                        {
                            return RedirectToAction(nameof(Error), new { message = "Lote não foi fornecido!" });
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return RedirectToAction("Details", "Produto", new { id = lote.IdProduto });
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Lote/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
                }

                var lote = await _context.Lote
                    .Include(l => l.Produto)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (lote == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Lote não foi fornecido!" });
                }

                return View(lote);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // POST: Lote/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                var lote = await _context.Lote.FindAsync(id);
                lote.Ativo = false;
                _context.Lote.Update(lote);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Produto");
            }
            return RedirectToAction("Login", "Funcionario");
        }
        
        // POST: Lote/Descartar
        [HttpPost]
        public async Task<IActionResult> Descartar(int id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                var lote = await _context.Lote
                    .Include(lt => lt.Produto)
                    .FirstAsync(lt => lt.Id == id);

                lote.Descartar();
                lote.Ativo = false;

                _context.Lote.Update(lote);
                _context.Produto.Update(lote.Produto);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Produto");
            }
            return RedirectToAction("Login", "Funcionario");
        }

        private bool LoteExists(int id)
        {
            return _context.Lote.Any(e => e.Id == id);
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
