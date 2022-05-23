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
    public class DeliveryController : Controller
    {
        private readonly SoftCandyContext _context;

        public DeliveryController(SoftCandyContext context)
        {
            _context = context;
        }

        // GET: Delivery
        public async Task<IActionResult> Index()
        {
            var softCandyContext = _context.Delivery.Include(d => d.Caixa).Include(d => d.Motoboy);
            return View(await softCandyContext.ToListAsync());
        }

        // GET: Delivery/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Delivery
                .Include(d => d.Caixa)
                .Include(d => d.Motoboy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // GET: Delivery/Create
        public IActionResult Create()
        {
            ViewData["IdCaixa"] = new SelectList(_context.Caixa, "IdCaixa", "IdCaixa");
            ViewData["IdMotoboy"] = new SelectList(_context.Motoboy, "Id", "Id");
            return View();
        }

        // POST: Delivery/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ValorTotal,DataHoraCriacao,DataHoraRecebimento,Recebido,IdCaixa,IdMotoboy,FormaPagamento")] Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(delivery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCaixa"] = new SelectList(_context.Caixa, "IdCaixa", "IdCaixa", delivery.IdCaixa);
            ViewData["IdMotoboy"] = new SelectList(_context.Motoboy, "Id", "Id", delivery.IdMotoboy);
            return View(delivery);
        }

        // GET: Delivery/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Delivery.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }
            ViewData["IdCaixa"] = new SelectList(_context.Caixa, "IdCaixa", "IdCaixa", delivery.IdCaixa);
            ViewData["IdMotoboy"] = new SelectList(_context.Motoboy, "Id", "Id", delivery.IdMotoboy);
            return View(delivery);
        }

        // POST: Delivery/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ValorTotal,DataHoraCriacao,DataHoraRecebimento,Recebido,IdCaixa,IdMotoboy,FormaPagamento")] Delivery delivery)
        {
            if (id != delivery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(delivery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryExists(delivery.Id))
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
            ViewData["IdCaixa"] = new SelectList(_context.Caixa, "IdCaixa", "IdCaixa", delivery.IdCaixa);
            ViewData["IdMotoboy"] = new SelectList(_context.Motoboy, "Id", "Id", delivery.IdMotoboy);
            return View(delivery);
        }

        // GET: Delivery/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Delivery
                .Include(d => d.Caixa)
                .Include(d => d.Motoboy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // POST: Delivery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delivery = await _context.Delivery.FindAsync(id);
            _context.Delivery.Remove(delivery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryExists(int id)
        {
            return _context.Delivery.Any(e => e.Id == id);
        }
    }
}
