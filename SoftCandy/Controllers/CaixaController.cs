using System;
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
            return View(await _context.Caixa
                .Where(c => !c.EstaAberto)
                .Include(f => f.FuncionarioAbertura)
                .Include(f => f.FuncionarioFechamento)
                .OrderByDescending(p => p.IdCaixa)
                .Take(20)
                .ToListAsync());
        }


        // GET: Caixa
        public async Task<IActionResult> Caixa()
        {
            if (CaixaUtils.IsAberto(_context))
            {
                return View( await _context.Caixa
                .Where(c => c.EstaAberto)
                .Include(c => c.Operacoes)
                .ThenInclude(c => c.Funcionario)
                .Include(p => p.Pedidos)
                .ThenInclude(c => c.Cliente)
                .FirstAsync());
            }
            else
            {
                return RedirectToAction(nameof(Abertura));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Caixa([Bind("ValorAbertura")] Caixa caixa)
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
        public async Task<IActionResult> Abertura([Bind("ValorDinheiroAbertura")] Caixa caixa)
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

        // GET: Caixa/Fechamento
        public IActionResult Fechamento()
        {
            if (CaixaUtils.IsAberto(_context))
            {
                Caixa caixa = CaixaUtils.CaixaAberto(_context);
                return View(caixa);
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
            _context.Entry(caixa).Collection(c => c.Pedidos).Load();
            if (caixa.ExistePedidoSemReceber())
            {
                return RedirectToAction(nameof(Error), new { message = "Existe pedido sem receber!" });
            }
            caixa.EstaAberto = false;
            caixa.FuncionarioFechamentoId = LoginAtual.Id(User);
            caixa.DataHoraFechamento = DateTime.Now;
            _context.Update(caixa);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Historico));
        }

        // GET: Caixa/Details/
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caixa = await _context.Caixa
                .Include(c => c.Pedidos)
                .Include(c => c.Operacoes)
                .Include(c => c.FuncionarioAbertura)
                .Include(c => c.FuncionarioFechamento)
                .FirstOrDefaultAsync(m => m.IdCaixa == id);
            if (caixa == null)
            {
                return NotFound();
            }

            return View(caixa);
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
