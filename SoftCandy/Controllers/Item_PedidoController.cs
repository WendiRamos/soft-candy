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
    public class Item_PedidoController : Controller
    {
        private readonly SoftCandyContext _context;

        public Item_PedidoController(SoftCandyContext context)
        {
            _context = context;
        }

        // GET: Item_Pedido
        public async Task<IActionResult> Index()
        {
            var softCandyContext = _context.Item_Pedido.Include(i => i.Produto);
            return View(await softCandyContext.ToListAsync());
        }

        // GET: Item_Pedido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item_Pedido = await _context.Item_Pedido
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.Num_Pedido == id);
            if (item_Pedido == null)
            {
                return NotFound();
            }

            return View(item_Pedido);
        }

        // GET: Item_Pedido/Create
        public IActionResult Create()
        {
            ViewData["Cod_Produto"] = new SelectList(_context.Produto, "Cod_Produto", "Nome_Produto");
            return View();
        }

        // POST: Item_Pedido/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Preco_Pago,Quantidade,Cod_Produto,Num_Pedido")] Item_Pedido item_Pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item_Pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cod_Produto"] = new SelectList(_context.Produto, "Cod_Produto", "Nome_Produto", item_Pedido.Cod_Produto);
            return View(item_Pedido);
        }

        // GET: Item_Pedido/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item_Pedido = await _context.Item_Pedido.FindAsync(id);
            if (item_Pedido == null)
            {
                return NotFound();
            }
            ViewData["Cod_Produto"] = new SelectList(_context.Produto, "Cod_Produto", "Nome_Produto", item_Pedido.Cod_Produto);
            return View(item_Pedido);
        }

        // POST: Item_Pedido/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Preco_Pago,Quantidade,Cod_Produto,Num_Pedido")] Item_Pedido item_Pedido)
        {
            if (id != item_Pedido.Num_Pedido)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item_Pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Item_PedidoExists(item_Pedido.Num_Pedido))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cod_Produto"] = new SelectList(_context.Produto, "Cod_Produto", "Nome_Produto", item_Pedido.Cod_Produto);
            return View(item_Pedido);
        }

        // GET: Item_Pedido/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item_Pedido = await _context.Item_Pedido
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.Num_Pedido == id);
            if (item_Pedido == null)
            {
                return NotFound();
            }

            return View(item_Pedido);
        }

        // POST: Item_Pedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item_Pedido = await _context.Item_Pedido.FindAsync(id);
            _context.Item_Pedido.Remove(item_Pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Item_PedidoExists(int id)
        {
            return _context.Item_Pedido.Any(e => e.Num_Pedido == id);
        }
    }
}
