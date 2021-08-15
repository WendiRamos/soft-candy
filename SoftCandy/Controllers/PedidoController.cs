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
    public class PedidoController : Controller
    {
        private readonly SoftCandyContext _context;

        public PedidoController(SoftCandyContext context)
        {
            _context = context;
        }

        // GET: Pedido
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var softCandyContext = _context.Pedido.Include(c => c.Cliente);

                return View(await softCandyContext.ToListAsync());
            }
            return RedirectToAction("Index", "Home");

        }

        // GET: Pedido/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var pedido = await _context.Pedido
                    .Include(p => p.Cliente)
                    .FirstOrDefaultAsync(m => m.Num_Pedido == id);
                if (pedido == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(pedido);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Pedido/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["ID_CLIENTE"] = new SelectList(_context.Cliente, "Id_Cliente", "Nome");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Pedido/Create
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<Item_Pedido> itensL)
        {
            return View(itensL);
        }

        // GET: Pedido/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var pedido = await _context.Pedido.FindAsync(id);
                if (pedido == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                ViewData["ID_CLIENTE"] = new SelectList(_context.Cliente, "Id_Cliente", "Celular", pedido.ID_CLIENTE);
                return View(pedido);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Pedido/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Num_Pedido,Valor_Total,Desconto,Data_Pedido,ID_CLIENTE")] Pedido pedido)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != pedido.Num_Pedido)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(pedido);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PedidoExists(pedido.Num_Pedido))
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
                ViewData["ID_CLIENTE"] = new SelectList(_context.Cliente, "Id_Cliente", "Celular", pedido.ID_CLIENTE);
                return View(pedido);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Pedido/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var pedido = await _context.Pedido
                    .Include(p => p.Cliente)
                    .FirstOrDefaultAsync(m => m.Num_Pedido == id);
                if (pedido == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(pedido);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Pedido/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var pedido = await _context.Pedido.FindAsync(id);
                _context.Pedido.Remove(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Home");
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedido.Any(e => e.Num_Pedido == id);
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
