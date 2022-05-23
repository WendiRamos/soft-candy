using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoftCandy.Data;
using SoftCandy.Models;

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
            return View(await _context.Motoboy.ToListAsync());
        }

        // GET: Motoboy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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

        // POST: Motoboy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Celular,Logradouro,Numero,Bairro,Cidade,Estado,Ativo")] Motoboy motoboy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(motoboy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(motoboy);
        }

        // GET: Motoboy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motoboy = await _context.Motoboy.FindAsync(id);
            if (motoboy == null)
            {
                return NotFound();
            }
            return View(motoboy);
        }

        // POST: Motoboy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Celular,Logradouro,Numero,Bairro,Cidade,Estado,Ativo")] Motoboy motoboy)
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
            _context.Motoboy.Remove(motoboy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MotoboyExists(int id)
        {
            return _context.Motoboy.Any(e => e.Id == id);
        }
    }
}
