using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoftCandy.Data;
using SoftCandy.Models;

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
            if (id == null)
            {
                return NotFound();
            }

            var lote = await _context.Lote
                .Include(l => l.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lote == null)
            {
                return NotFound();
            }

            return View(lote);
        }

        // GET: Lote/Create
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                ViewData["ListaProdutos"] = new SelectList(_context.Produto.Where(p => p.Ativo), "Id", "Nome");
                return View();
            }

            var produto = await _context.Produto.FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            ViewData["NomeProduto"] = produto.Nome;
            ViewData["IdProduto"] = id;
            return View();
        }

        // POST: Lote/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuantidadeEstoque,DataHoraFabricacao,PrecoCompra,PrecoVenda,IdProduto,DiasVencimento")] Lote lote)
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

        // GET: Lote/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var lote = await _context.Lote
                .Include(lt => lt.Produto)
                .FirstOrDefaultAsync(lt => lt.Id == id);

            if (lote == null)
            {
                return NotFound();
            }

            lote.DiasVencimento = (lote.DataHoraValidade - lote.DataHoraFabricacao).Days;

            ViewData["IdProduto"] = id;
            ViewData["NomeProduto"] = lote.Produto.Nome;
            return View(lote);
        }

        // POST: Lote/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuantidadeEstoque,DataHoraFabricacao,DiasVencimento,PrecoCompra,PrecoVenda,IdProduto")] Lote lote)
        {
            if (id != lote.Id)
            {
                return NotFound();
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
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction("Details", "Produto", new { id = lote.IdProduto });
        }

        // GET: Lote/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lote = await _context.Lote
                .Include(l => l.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lote == null)
            {
                return NotFound();
            }

            return View(lote);
        }

        // POST: Lote/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lote = await _context.Lote.FindAsync(id);
            lote.Ativo = false;
            _context.Lote.Update(lote);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Produto");
        }

        private bool LoteExists(int id)
        {
            return _context.Lote.Any(e => e.Id == id);
        }
    }
}
