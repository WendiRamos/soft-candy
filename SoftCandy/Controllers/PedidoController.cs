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
                var softCandyContext = _context.Pedido.OrderByDescending(p => p.IdPedido).Include(c => c.Cliente);

                return View(await softCandyContext.ToListAsync());
                
            }
            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Relatorio()
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
                    .FirstOrDefaultAsync(m => m.IdPedido == id);
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
                var model = new RealizarPedido();
                model.Produtos = _context.Produto.ToList();
                ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "NomeCliente");
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Pedido/Create
        [HttpPost]
        public int Create(List<ItemPedido> Itens, int IdCliente)
        {
            decimal total = 0;
            foreach(ItemPedido item in Itens)
                total += item.PrecoPago * item.QuantidadeProduto;

            Pedido pedido = new Pedido(total, IdCliente, Itens);

            _context.Add(pedido);
            _context.SaveChanges();

            return pedido.IdPedido;
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
                ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "NomeCliente");
                return View(pedido);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Pedido/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPedido,ValorTotalPedido,Data_Pedido,IdCliente")] Pedido pedido)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != pedido.IdPedido)
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
                        if (!PedidoExists(pedido.IdPedido))
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
                ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "NomeCliente");
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
                    .FirstOrDefaultAsync(m => m.IdPedido == id);
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
            return _context.Pedido.Any(e => e.IdPedido == id);
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
