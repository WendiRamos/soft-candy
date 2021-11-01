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
    public class FornecedorController : Controller
    {
        private readonly SoftCandyContext _context;

        public FornecedorController(SoftCandyContext context)
        {
            _context = context;
        }

        // GET: Fornecedor
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.Fornecedor.Where(c => c.AtivoFornecedor).ToListAsync());
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> FornecedoresApagadas()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.Fornecedor.Where(c => c.AtivoFornecedor == false).ToListAsync());
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Relatorio()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.Fornecedor.Where(c => c.AtivoFornecedor).ToListAsync());
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Fornecedor/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
                }

                var fornecedor = await _context.Fornecedor
                    .FirstOrDefaultAsync(m => m.IdFornecedor == id);
                if (fornecedor == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(fornecedor);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Fornecedore/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Fornecedor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFornecedor,Cnpj,RazaoSocial,NomeFantasia,CelularFornecedor,EmailFornecedor,LogradouroFornecedor,NumeroFornecedor,BairroFornecedor,CidadeFornecedor,EstadoFornecedor")] Fornecedor fornecedor)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    fornecedor.AtivoFornecedor = true;
                    _context.Add(fornecedor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(fornecedor);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Fornecedor/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var fornecedor = await _context.Fornecedor.FindAsync(id);
                if (fornecedor == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                return View(fornecedor);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Fornecedor/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFornecedor,Cnpj,RazaoSocial,NomeFantasia,CelularFornecedor,EmailFornecedor,LogradouroFornecedor,NumeroFornecedor,BairroFornecedor,CidadeFornecedor,EstadoFornecedor")] Fornecedor fornecedor)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != fornecedor.IdFornecedor)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não corresponde!" });
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(fornecedor);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        if (!FornecedorExists(fornecedor.IdFornecedor))
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
                return View(fornecedor);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Fornecedor/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
                }

                var fornecedor = await _context.Fornecedor
                    .FirstOrDefaultAsync(m => m.IdFornecedor == id);
                if (fornecedor == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(fornecedor);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST:Fornecedor/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var fornecedor = await _context.Fornecedor.FindAsync(id);
                fornecedor.AtivoFornecedor = false;
                _context.Fornecedor.Update(fornecedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Fornecedor/Restore
        public async Task<IActionResult> Restore(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var fornecedor = await _context.Fornecedor
                    .FirstOrDefaultAsync(m => m.IdFornecedor == id);
                if (fornecedor == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(fornecedor);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Fornecedor/Restore
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRestore(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var fornecedor = await _context.Fornecedor.FindAsync(id);
                fornecedor.AtivoFornecedor = true;
                _context.Fornecedor.Update(fornecedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Home");
        }

        private bool FornecedorExists(int id)
        {
            return _context.Fornecedor.Any(e => e.IdFornecedor == id);
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