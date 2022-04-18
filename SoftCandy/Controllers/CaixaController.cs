using System;
using System.Collections.Generic;
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
    public class CaixaController : Controller
    {
        private readonly SoftCandyContext _context;

        public CaixaController(SoftCandyContext context)
        {
            _context = context;
        }

        // GET: Histórico
        public async Task<IActionResult> Historico()
        {
            return View(await _context.Caixa.ToListAsync());
        }


        // GET: Caixa
        public IActionResult Caixa()
        {
            ViewData["CaixaIdAberto"] = CaixaUtils.IdAberto(_context);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Caixa([Bind("ValorAbertura")] Caixa caixa)
        {
            if (ModelState.IsValid)
            {
                caixa.EstaAberto = true;
                caixa.DataHoraAbertura = DateTime.Now;
                caixa.FuncionarioAberturaId = LoginAtual.Id(User);
                caixa.FuncionarioFechamentoId = LoginAtual.Id(User);
                _context.Add(caixa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Caixa));
            }
            return View(caixa);
        }

        public IActionResult Abertura()
        {
            ViewData["CaixaIdAberto"] = CaixaUtils.IdAberto(_context);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Abertura([Bind("ValorAbertura")] Caixa caixa)
        {
            if (ModelState.IsValid)
            {
                caixa.EstaAberto = true;
                caixa.DataHoraAbertura = DateTime.Now;
                caixa.FuncionarioAberturaId = LoginAtual.Id(User);
                caixa.FuncionarioFechamentoId = LoginAtual.Id(User);
                _context.Add(caixa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Caixa));
            }
            return View(caixa);
        }

        // GET: Caixa/Edit/5
        public async Task<IActionResult> Fechamento(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caixa = await _context.Caixa.FindAsync(id);
            if (caixa == null)
            {
                return NotFound();
            }
            return View(caixa);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Fechamento(int id, [Bind("IdCaixa,DataHoraAbertura,DataHoraFechamento,ValorAbertura,ValorFechamento,EstaAberto,FuncionarioAberturaId,FuncionarioFechamentoId")] Caixa caixa)
        {
            if (id != caixa.IdCaixa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caixa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaixaExists(caixa.IdCaixa))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Historico));
            }
            return View(caixa);
        }

        // GET: Caixa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caixa = await _context.Caixa
                .FirstOrDefaultAsync(m => m.IdCaixa == id);
            if (caixa == null)
            {
                return NotFound();
            }

            return View(caixa);
        }
        private bool CaixaExists(int id)
        {
            return _context.Caixa.Any(e => e.IdCaixa == id);
        }
    }
}
