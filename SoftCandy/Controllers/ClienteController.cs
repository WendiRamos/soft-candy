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
    public class ClienteController : Controller
    {
        private readonly SoftCandyContext _context;

        public ClienteController(SoftCandyContext context)
        {
            _context = context;
        }

        // GET: Cliente
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cliente.ToListAsync());
        }

        // GET: Cliente/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.ID_Cliente == id);
            if (cliente == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
            }

            return View(cliente);
        }

        // GET: Cliente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Cliente,Nome,Celular,Endereco")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Cliente/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
            }
            return View(cliente);
        }

        // POST: Cliente/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Cliente,Nome,Celular,Endereco")] Cliente cliente)
        {
            if (id != cliente.ID_Cliente)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não corresponde!" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!ClienteExists(cliente.ID_Cliente))
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
            return View(cliente);
        }

        // GET: Cliente/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.ID_Cliente == id);
            if (cliente == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
            }

            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.ID_Cliente == id);
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
