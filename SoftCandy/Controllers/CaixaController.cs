﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoftCandy.Data;
using SoftCandy.Enums;
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
            return View(await _context.Caixa
                .Where(c => !c.EstaAberto)
                .Include(f => f.FuncionarioAbertura)
                .Include(f => f.FuncionarioFechamento)
                .ToListAsync());
        }


        // GET: Caixa
        public IActionResult Caixa()
        {
            if (CaixaUtils.IsAberto(_context))
            {
                return View();
            }
            else
            {
                return RedirectToAction(nameof(Abertura));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Caixa([Bind("ValorAbertura")] Caixa caixa)
        {
            return View(caixa);
        }

        public IActionResult Abertura()
        {
            if (CaixaUtils.IsFechado(_context))
            {
                return View();
            }
            else
            {
                return RedirectToAction(nameof(Caixa));
            }
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

        // GET: Caixa/Fechamento
        public async Task<IActionResult> Fechamento()
        {
            if (CaixaUtils.IsAberto(_context))
            {
                return View();
            }
            else
            {
                return RedirectToAction(nameof(Historico));
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FechamentoCaixa()
        {
            Caixa caixa = CaixaUtils.CaixaAberto(_context);
            caixa.EstaAberto = false;
            caixa.FuncionarioFechamentoId = LoginAtual.Id(User);
            caixa.AtualizaValorFechamento();
            caixa.DataHoraFechamento = DateTime.Now;
            _context.Update(caixa);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Historico));
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
