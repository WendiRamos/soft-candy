﻿using System;
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
            if (LoginAtual.IsAdministrador(User))
            {
                return View(await _context.Motoboy.Where(m => m.Ativo).Take(20).ToListAsync());
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Motoboy/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                var motoboy = await _context.Motoboy
                .FirstOrDefaultAsync(m => m.Id == id);
                if (motoboy == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(motoboy);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Motoboy/Create
        public IActionResult Create()
        {
            if (LoginAtual.IsAdministrador(User))
            {
                return View();
            }
            return RedirectToAction("Login", "Funcionario");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Celular,Logradouro,Numero,Bairro,Cidade,Estado")] Motoboy motoboy)
        {
            if (LoginAtual.IsAdministrador(User))
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
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Motoboy/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                var motoboy = await _context.Motoboy.FindAsync(id);
                if (motoboy == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                return View(motoboy);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // POST: Motoboy/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Celular,Logradouro,Numero,Bairro,Cidade,Estado")] Motoboy motoboy)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (id != motoboy.Id)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        motoboy.Ativo = true;
                        _context.Update(motoboy);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MotoboyExists(motoboy.Id))
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
                return View(motoboy);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Motoboy/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                var motoboy = await _context.Motoboy
                .FirstOrDefaultAsync(m => m.Id == id);
                if (motoboy == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(motoboy);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // POST: Motoboy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                var motoboy = await _context.Motoboy.FindAsync(id);
                motoboy.Ativo = false;
                _context.Motoboy.Update(motoboy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Login", "Funcionario");
        }

        public async Task<IActionResult> Restore(int? id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (LoginAtual.IsAdministrador(User))
                {
                    if (id == null)
                    {
                        return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                    }

                    var motoboy = await _context.Motoboy
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (motoboy == null)
                    {
                        return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                    }

                    return View(motoboy);
                }
                return RedirectToAction("Login", "Funcionario");
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // POST: Motoboy/Restore
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreConfirmed(int id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (LoginAtual.IsAdministrador(User))
                {
                    var motoboy = await _context.Motoboy.FindAsync(id);
                    motoboy.Ativo = true;
                    _context.Motoboy.Update(motoboy);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction("Login", "Funcionario");
            }
            return RedirectToAction("Login", "Funcionario");
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
