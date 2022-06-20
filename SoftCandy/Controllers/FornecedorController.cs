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
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                return View(await _context.Fornecedor.Where(c => c.AtivoFornecedor).Take(20).ToListAsync());
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Fornecedor/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
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
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Fornecedore/Create
        public IActionResult Create()
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                return View();
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // POST: Fornecedor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cnpj,RazaoSocial,NomeFantasia,CelularFornecedor,EmailFornecedor,LogradouroFornecedor,NumeroFornecedor,BairroFornecedor,CidadeFornecedor,EstadoFornecedor")] Fornecedor fornecedor)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
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
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Fornecedor/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
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
            return RedirectToAction("Login", "Funcionario");
        }

        // POST: Fornecedor/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFornecedor,Cnpj,RazaoSocial,NomeFantasia,CelularFornecedor,EmailFornecedor,LogradouroFornecedor,NumeroFornecedor,BairroFornecedor,CidadeFornecedor,EstadoFornecedor")] Fornecedor fornecedor)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                if (id != fornecedor.IdFornecedor)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não corresponde!" });
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        fornecedor.AtivoFornecedor = true;
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
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Fornecedor/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
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
            return RedirectToAction("Login", "Funcionario");
        }

        // POST:Fornecedor/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                var fornecedor = await _context.Fornecedor.FindAsync(id);
                fornecedor.AtivoFornecedor = false;
                _context.Fornecedor.Update(fornecedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Fornecedor/Restore
        public async Task<IActionResult> Restore(int? id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
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
            return RedirectToAction("Login", "Funcionario");
        }

        // POST: Fornecedor/Restore
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRestore(int id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                var fornecedor = await _context.Fornecedor.FindAsync(id);
                fornecedor.AtivoFornecedor = true;
                _context.Fornecedor.Update(fornecedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Login", "Funcionario");
        }

        private bool FornecedorExists(int id)
        {
            return _context.Fornecedor.Any(e => e.IdFornecedor == id);
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
