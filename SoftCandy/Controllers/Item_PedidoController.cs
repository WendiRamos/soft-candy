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
            if (User.Identity.IsAuthenticated)
            {
                var softCandyContext = _context.Item_Pedido.Include(i => i.Produto);
                return View(await softCandyContext.ToListAsync());
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Item_Pedido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var item_Pedido = await _context.Item_Pedido
                    .Include(i => i.Produto)
                    .FirstOrDefaultAsync(m => m.Num_Pedido == id);
                if (item_Pedido == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(item_Pedido);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Item_Pedido/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["Cod_Produto"] = new SelectList(_context.Produto, "Cod_Produto", "Nome_Produto");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Item_Pedido/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Preco_Pago,Quantidade,Cod_Produto,Num_Pedido")] Item_Pedido item_Pedido)
        {
            if (User.Identity.IsAuthenticated)
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
            return RedirectToAction("Index", "Home");
        }

        // GET: Item_Pedido/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var item_Pedido = await _context.Item_Pedido.FindAsync(id);
                if (item_Pedido == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                ViewData["Cod_Produto"] = new SelectList(_context.Produto, "Cod_Produto", "Nome_Produto", item_Pedido.Cod_Produto);
                return View(item_Pedido);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Item_Pedido/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Preco_Pago,Quantidade,Cod_Produto,Num_Pedido")] Item_Pedido item_Pedido)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != item_Pedido.Num_Pedido)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
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
                            return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
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
            return RedirectToAction("Index", "Home");
        }

        // GET: Item_Pedido/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var item_Pedido = await _context.Item_Pedido
                    .Include(i => i.Produto)
                    .FirstOrDefaultAsync(m => m.Num_Pedido == id);
                if (item_Pedido == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(item_Pedido);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Item_Pedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var item_Pedido = await _context.Item_Pedido.FindAsync(id);
                _context.Item_Pedido.Remove(item_Pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Home");
        }

        private bool Item_PedidoExists(int id)
        {
            return _context.Item_Pedido.Any(e => e.Num_Pedido == id);
        }

        public IActionResult Error(string message)
        {
            if (User.Identity.IsAuthenticated)
            {
                var viewModel = new ErrorViewModel
                {
                    Message = message,
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return View(viewModel);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
