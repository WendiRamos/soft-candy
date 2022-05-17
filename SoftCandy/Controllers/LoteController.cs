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
        public IActionResult Create()
        {
            ViewData["IdProduto"] = new SelectList(_context.Produto, "Id", "Nome");
            return View();
        }

        // POST: Lote/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuantidadeEstoque,DataFabricacao,PrecoCompra,PrecoVenda,IdProduto,DiasVencimento")] Lote lote)
        {
            if (ModelState.IsValid)
            {
                lote.Ativo = true;
                lote.DataValidade = lote.DataFabricacao.AddDays(lote.DiasVencimento);
                _context.Add(lote);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Produto", new { id = lote.IdProduto });
            }
            ViewData["IdProduto"] = new SelectList(_context.Produto, "Id", "Nome", lote.IdProduto);
            return View(lote);
        }

        // GET: Lote/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lote = await _context.Lote.FindAsync(id);
            if (lote == null)
            {
                return NotFound();
            }
            ViewData["IdProduto"] = new SelectList(_context.Produto, "Id", "Nome", lote.IdProduto);
            return View(lote);
        }

        // POST: Lote/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuantidadeEstoque,DataFabricacao,DataValidade,PrecoCompra,PrecoVenda,Ativo,IdProduto")] Lote lote)
        {
            if (id != lote.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                //return RedirectToAction(nameof(Index));
            }
            ViewData["IdProduto"] = new SelectList(_context.Produto, "Id", "Nome", lote.IdProduto);
            return View(lote);
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
            lote.Ativo = true;
            _context.Lote.Update(lote);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details));
        }

        private bool LoteExists(int id)
        {
            return _context.Lote.Any(e => e.Id == id);
        }
    }
}
