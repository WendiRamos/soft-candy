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
    public class CategoriaController : Controller
    {
        private readonly SoftCandyContext _context;

        public CategoriaController(SoftCandyContext context)
        {
            _context = context;
        }

        // GET: Categoria
        public async Task<IActionResult> Index()
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {   
                return View(await _context.Categoria.Where(c => c.AtivoCategoria).ToListAsync());
            }
            return RedirectToAction("User", "Home");
        }

        public async Task<IActionResult> Relatorio()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.Categoria.Where(c => c.AtivoCategoria).ToListAsync());
            }
            return RedirectToAction("Index", "Home");
        }
        // GET: Categoria/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido" });
                }

                var categoria = await _context.Categoria
                    .FirstOrDefaultAsync(m => m.IdCategoria == id);
                if (categoria == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(categoria);
            }
            return RedirectToAction("User", "Home");
        }

        // GET: Categoria/Create
        public IActionResult Create()
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                return View();
            }
            return RedirectToAction("User", "Home");
        }

        // POST: Categoria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCategoria,NomeCategoria")] Categoria categoria)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                if (ModelState.IsValid)
                {
                    categoria.AtivoCategoria = true;
                    _context.Add(categoria);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(categoria);
            }
            return RedirectToAction("User", "Home");
        }

        // GET: Categoria/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido" });
                }

                var categoria = await _context.Categoria.FindAsync(id);
                if (categoria == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                return View(categoria);
            }
            return RedirectToAction("User", "Home");
        }

        // POST: Categoria/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCategoria,NomeCategoria")] Categoria categoria)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                if (id != categoria.IdCategoria)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        categoria.AtivoCategoria = true;
                        _context.Update(categoria);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CategoriaExists(categoria.IdCategoria))
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
                return View(categoria);
            }
            return RedirectToAction("User", "Home");
        }

        // GET: Categoria/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var categoria = await _context.Categoria
                    .FirstOrDefaultAsync(m => m.IdCategoria == id);
                if (categoria == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(categoria);
            }
            return RedirectToAction("User", "Home");
        }

        // POST: Categoria/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                var categoria = await _context.Categoria.FindAsync(id);
                categoria.AtivoCategoria = false;
                _context.Categoria.Update(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("User", "Home");
        }

        // GET: Categoria/Restore
        public async Task<IActionResult> Restore(int? id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var categoria = await _context.Categoria
                    .FirstOrDefaultAsync(m => m.IdCategoria == id);
                if (categoria == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(categoria);
            }
            return RedirectToAction("User", "Home");
        }

        // POST: Categoria/Restore
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRestore(int id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                var categoria = await _context.Categoria.FindAsync(id);
                categoria.AtivoCategoria = true;
                _context.Categoria.Update(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("User", "Home");
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.IdCategoria == id);

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
