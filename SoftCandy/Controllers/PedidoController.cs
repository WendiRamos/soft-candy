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
            return View(await _context.Pedido.ToListAsync());
        }

        // GET: Pedido/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Número do pedido não foi fornecido!" });
            }

            var pedido = await _context.Pedido
                .FirstOrDefaultAsync(m => m.Num_Pedido == id);
            if (pedido == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Número do pedido não foi existe!" });
            }

            return View(pedido);
        }

        // GET: Pedido/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Num_Pedido,Valor_Total,Desconto,Data_Pedido")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        // GET: Pedido/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Número do pedido não foi fornecido!" });
            }

            var pedido = await _context.Pedido
                .FirstOrDefaultAsync(m=>m.Num_Pedido==id);
            if (pedido == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Número do pedido não existe!" });
            }
            return View(pedido);
        }

        // POST: Pedido/Edit
         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Num_Pedido,Valor_Total,Desconto,Data_Pedido")] Pedido pedido)
        {
            if (id != pedido.Num_Pedido)
            {
                return RedirectToAction(nameof(Error), new { message = "Número do pedido não corresponde!" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!PedidoExists(pedido.Num_Pedido))
                    {
                        return RedirectToAction(nameof(Error), new { message = e.Message });
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        // GET: Pedido/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Número do pedido não foi fornecido!" });
            }

            var pedido = await _context.Pedido
                .FirstOrDefaultAsync(m => m.Num_Pedido == id);
            if (pedido == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Número do pedido não existe!" });
            }

            return View(pedido);
        }

        // POST: Pedido/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);
            _context.Pedido.Remove(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedido.Any(e => e.Num_Pedido == id);
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
