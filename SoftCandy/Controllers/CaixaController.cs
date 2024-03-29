﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if (LoginAtual.IsCaixa(User) || LoginAtual.IsAdministrador(User))
            {
                return View(await _context.Caixa
                .Where(c => !c.EstaAberto)
                .Include(f => f.FuncionarioAbertura)
                .Include(f => f.FuncionarioFechamento)
                .OrderByDescending(p => p.IdCaixa)
                .Take(20)
                .ToListAsync());
            }
            return RedirectToAction("Login", "Funcionario");
        }


        // GET: Caixa
        public async Task<IActionResult> Caixa()
        {
            if (LoginAtual.IsCaixa(User) || LoginAtual.IsAdministrador(User))
            {
                if (CaixaUtils.IsAberto(_context))
                {
                    return View(await _context.Caixa
                    .Where(c => c.EstaAberto)
                    .Include(c => c.Operacoes)
                    .ThenInclude(o => o.Funcionario)
                    .Include(c => c.Comandas)
                    .Include(c => c.Deliveries)
                    .ThenInclude(d => d.Motoboy)
                    .FirstAsync());
                }
                else
                {
                    return RedirectToAction(nameof(Abertura));
                }
            }
            return RedirectToAction("Login", "Funcionario");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Caixa([Bind("ValorAbertura")] Caixa caixa)
        {
            if (LoginAtual.IsCaixa(User) || LoginAtual.IsAdministrador(User))
            {
                return View(caixa);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        public IActionResult Abertura()
        {
            if (LoginAtual.IsCaixa(User) || LoginAtual.IsAdministrador(User))
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
            return RedirectToAction("Login", "Funcionario");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Abertura([Bind("ValorDinheiroAbertura")] Caixa caixa)
        {
            if (LoginAtual.IsCaixa(User) || LoginAtual.IsAdministrador(User))
            {
                if (ModelState.IsValid)
                {
                    caixa.ValorTotalFechamentoDinheiro = caixa.ValorDinheiroAbertura;
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
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Caixa/Fechamento
        public IActionResult Fechamento()
        {
            if (LoginAtual.IsCaixa(User) || LoginAtual.IsAdministrador(User))
            {
                if (CaixaUtils.IsAberto(_context))
                {
                    Caixa caixa = CaixaUtils.CaixaAberto(_context);
                    _context.Entry(caixa).Collection(c => c.Comandas).Load();
                    _context.Entry(caixa).Collection(c => c.Deliveries).Load();

                    ViewData["ComandaDinheiro"] = caixa.Comandas.Where(c => c.FormaPagamentoIsDinheiro()).Select(c => c.ValorTotal).Sum();
                    ViewData["ComandaCredito"] = caixa.Comandas.Where(c => c.FormaPagamentoIsCredito()).Select(c => c.ValorTotal).Sum();
                    ViewData["ComandaDebito"] = caixa.Comandas.Where(c => c.FormaPagamentoIsDebito()).Select(c => c.ValorTotal).Sum();
                    ViewData["ComandaPix"] = caixa.Comandas.Where(c => c.FormaPagamentoIsPix()).Select(c => c.ValorTotal).Sum();
                    ViewData["ComandaTotal"] = caixa.Comandas.Select(c => c.ValorTotal).Sum();

                    ViewData["DeliveryDinheiro"] = caixa.Deliveries.Where(c => c.FormaPagamentoIsDinheiro()).Select(c => c.ValorTotal).Sum();
                    ViewData["DeliveryCredito"] = caixa.Deliveries.Where(c => c.FormaPagamentoIsCredito()).Select(c => c.ValorTotal).Sum();
                    ViewData["DeliveryDebito"] = caixa.Deliveries.Where(c => c.FormaPagamentoIsDebito()).Select(c => c.ValorTotal).Sum();
                    ViewData["DeliveryPix"] = caixa.Deliveries.Where(c => c.FormaPagamentoIsPix()).Select(c => c.ValorTotal).Sum();
                    ViewData["DeliveryTotal"] = caixa.Deliveries.Select(c => c.ValorTotal).Sum();

                    return View(caixa);
                }
                else
                {
                    return RedirectToAction(nameof(Historico));
                }
            }
            return RedirectToAction("Login", "Funcionario");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FechamentoCaixa()
        {
            if (LoginAtual.IsCaixa(User) || LoginAtual.IsAdministrador(User))
            {
                Caixa caixa = CaixaUtils.CaixaAberto(_context);
                _context.Entry(caixa).Collection(c => c.Comandas).Load();
                _context.Entry(caixa).Collection(c => c.Deliveries).Load();

                caixa.EstaAberto = false;
                caixa.FuncionarioFechamentoId = LoginAtual.Id(User);
                caixa.DataHoraFechamento = DateTime.Now;
                _context.Update(caixa);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Historico));
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Caixa/Details/
        public async Task<IActionResult> Details(int id)
        {
            if (LoginAtual.IsCaixa(User) || LoginAtual.IsAdministrador(User))
            {
                var caixa = await _context.Caixa
                .Include(c => c.Comandas)
                .Include(c => c.Operacoes)
                .Include(c => c.FuncionarioAbertura)
                .Include(c => c.FuncionarioFechamento)
                .Include(c => c.Deliveries)
                .ThenInclude(d => d.Motoboy)
                .FirstOrDefaultAsync(m => m.IdCaixa == id);
                if (caixa == null)
                {
                    return NotFound();
                }
                _context.Entry(caixa).Collection(c => c.Comandas).Load();
                _context.Entry(caixa).Collection(c => c.Deliveries).Load();

                ViewData["ComandaDinheiro"] = caixa.Comandas.Where(c => c.FormaPagamentoIsDinheiro()).Select(c => c.ValorTotal).Sum();
                ViewData["ComandaCredito"] = caixa.Comandas.Where(c => c.FormaPagamentoIsCredito()).Select(c => c.ValorTotal).Sum();
                ViewData["ComandaDebito"] = caixa.Comandas.Where(c => c.FormaPagamentoIsDebito()).Select(c => c.ValorTotal).Sum();
                ViewData["ComandaPix"] = caixa.Comandas.Where(c => c.FormaPagamentoIsPix()).Select(c => c.ValorTotal).Sum();
                ViewData["ComandaTotal"] = caixa.Comandas.Select(c => c.ValorTotal).Sum();

                ViewData["DeliveryDinheiro"] = caixa.Deliveries.Where(c => c.FormaPagamentoIsDinheiro()).Select(c => c.ValorTotal).Sum();
                ViewData["DeliveryCredito"] = caixa.Deliveries.Where(c => c.FormaPagamentoIsCredito()).Select(c => c.ValorTotal).Sum();
                ViewData["DeliveryDebito"] = caixa.Deliveries.Where(c => c.FormaPagamentoIsDebito()).Select(c => c.ValorTotal).Sum();
                ViewData["DeliveryPix"] = caixa.Deliveries.Where(c => c.FormaPagamentoIsPix()).Select(c => c.ValorTotal).Sum();
                ViewData["DeliveryTotal"] = caixa.Deliveries.Select(c => c.ValorTotal).Sum();
                return View(caixa);
            }
            return RedirectToAction("Login", "Funcionario");
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
