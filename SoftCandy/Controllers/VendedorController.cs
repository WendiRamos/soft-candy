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
    public class VendedorController : Controller
    {
        private readonly SoftCandyContext _context;

        public VendedorController(SoftCandyContext context)
        {
            _context = context;
        }

        // GET: Vendedor
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vendedor.ToListAsync());
        }

        // GET: Vendedor/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
            }

            var vendedor = await _context.Vendedor
                .FirstOrDefaultAsync(m => m.Id_Vendedor == id);
            if (vendedor == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
            }

            return View(vendedor);
        }

        // GET: Vendedor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vendedor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Vendedor,Nome_Vendedor,Celular_Vendedor,Endereco_Vendedor,Email_Vendedor,Senha_Vendedor")] Vendedor vendedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendedor);
        }

        // GET: Vendedor/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
            }

            var vendedor = await _context.Vendedor.FindAsync(id);
            if (vendedor == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
            }
            return View(vendedor);
        }

        // POST: Vendedor/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Vendedor,Nome_Vendedor,Celular_Vendedor,Endereco_Vendedor,Email_Vendedor,Senha_Vendedor")] Vendedor vendedor)
        {
            if (id != vendedor.Id_Vendedor)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não corresponde!" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!VendedorExists(vendedor.Id_Vendedor))
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
            return View(vendedor);
        }

        // GET: Vendedor/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
            }

            var vendedor = await _context.Vendedor
                .FirstOrDefaultAsync(m => m.Id_Vendedor == id);
            if (vendedor == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
            }

            return View(vendedor);
        }

        // POST: Vendedor/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendedor = await _context.Vendedor.FindAsync(id);
            _context.Vendedor.Remove(vendedor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendedorExists(int id)
        {
            return _context.Vendedor.Any(e => e.Id_Vendedor == id);
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
