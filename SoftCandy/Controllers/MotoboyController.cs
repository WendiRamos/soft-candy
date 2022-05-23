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
    public class MotoboyController : Controller
    {
        private readonly SoftCandyContext _context;

        public MotoboyController(SoftCandyContext context)
        {
            _context = context;
        }

        // GET: Motoboy
        public async Task<IActionResult> Index()
        {
            return View(await _context.Motoboy.Where( m => m.Ativo).Take(20).ToListAsync());
        }

        // GET: Motoboy/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var motoboy = await _context.Motoboy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motoboy == null)
            {
                return NotFound();
            }

            return View(motoboy);
        }

        // GET: Motoboy/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Celular,Logradouro,Numero,Bairro,Cidade,Estado")] Motoboy motoboy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(motoboy);
                motoboy.Ativo = true;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(motoboy);
        }

        // GET: Motoboy/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var motoboy = await _context.Motoboy.FindAsync(id);
            if (motoboy == null)
            {
                return NotFound();
            }
            return View(motoboy);
        }

        // POST: Motoboy/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Celular,Logradouro,Numero,Bairro,Cidade,Estado")] Motoboy motoboy)
        {
            if (id != motoboy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(motoboy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotoboyExists(motoboy.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(motoboy);
        }

        // GET: Motoboy/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var motoboy = await _context.Motoboy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motoboy == null)
            {
                return NotFound();
            }

            return View(motoboy);
        }

        // POST: Motoboy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var motoboy = await _context.Motoboy.FindAsync(id);
            motoboy.Ativo = false;
            _context.Motoboy.Update(motoboy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Restore(int? id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var motoboy = await _context.Funcionario
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (motoboy == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(motoboy);
            }
            return RedirectToAction("User", "Home");
        }

        // POST: Motoboy/Restore
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreConfirmed(int id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                var motoboy = await _context.Funcionario.FindAsync(id);
                motoboy.Ativo = true;
                _context.Funcionario.Update(motoboy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("User", "Home");
        }

        private bool MotoboyExists(int id)
        {
            return _context.Motoboy.Any(e => e.Id == id);
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
