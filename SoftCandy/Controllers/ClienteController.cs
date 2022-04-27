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
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                return View(await _context.Cliente.Where(c => c.AtivoCliente).ToListAsync());
            }
            return RedirectToAction("User", "Home");
        }

        public async Task<IActionResult> Relatorio()
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                return View(await _context.Cliente.Where(c => c.AtivoCliente).ToListAsync());
            }
            return RedirectToAction("User", "Home");
        }

        // GET: Cliente/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
                }

                var cliente = await _context.Cliente
                    .Where(c => c.AtivoCliente)
                    .FirstOrDefaultAsync(m => m.IdCliente == id);
                if (cliente == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(cliente);
            }
            return RedirectToAction("User", "Home");
        }

        // GET: Cliente/Create
        public IActionResult Create()
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                return View();
            }
            return RedirectToAction("User", "Home");
        }

        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomeCliente,CelularCliente,EmailCliente,LogradouroCliente,NumeroCliente,BairroCliente,CidadeCliente,EstadoCliente")] Cliente cliente)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                if (ModelState.IsValid)
                {
                    cliente.AtivoCliente = true;
                    _context.Add(cliente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(cliente);
            }
            return RedirectToAction("User", "Home");
        }

        // GET: Cliente/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var cliente = await _context.Cliente
                    .Where(c => c.AtivoCliente)
                    .FirstOrDefaultAsync(c => c.IdCliente == id);
                if (cliente == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                return View(cliente);
            }
            return RedirectToAction("User", "Home");
        }

        // POST: Cliente/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,NomeCliente,CelularCliente,EmailCliente,LogradouroCliente,NumeroCliente,BairroCliente,CidadeCliente,EstadoCliente")] Cliente cliente)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                if (id != cliente.IdCliente)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não corresponde!" });
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        cliente.AtivoCliente = true;
                        _context.Update(cliente);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        if (!ClienteExists(cliente.IdCliente))
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
            return RedirectToAction("User", "Home");
        }

        // GET: Cliente/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
                }

                var cliente = await _context.Cliente
                    .Where(c => c.AtivoCliente)
                    .FirstOrDefaultAsync(m => m.IdCliente == id);
                if (cliente == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(cliente);
            }
            return RedirectToAction("User", "Home");
        }

        // POST: Cliente/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var cliente = await _context.Cliente.FindAsync(id);
                cliente.AtivoCliente = false;
                _context.Cliente.Update(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("User", "Home");
        }

        // GET: Cliente/Restore
        public async Task<IActionResult> Restore(int? id)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var cliente = await _context.Cliente
                    .FirstOrDefaultAsync(m => m.IdCliente == id);
                if (cliente == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(cliente);
            }
            return RedirectToAction("User", "Home");
        }

        // POST: Cliente/Restore
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRestore(int id)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var cliente = await _context.Cliente.FindAsync(id);
                cliente.AtivoCliente = true;
                _context.Cliente.Update(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("User", "Home");
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.IdCliente == id);
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
