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
    public class OperacaoCaixaController : Controller
    {
        private readonly SoftCandyContext _context;

        public OperacaoCaixaController(SoftCandyContext context)
        {
            _context = context;
        }

 
        // GET: OperacaoCaixa/Details/
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operacaoCaixa = await _context.OperacaoCaixa
                .Include(o => o.Funcionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operacaoCaixa == null)
            {
                return NotFound();
            }

            return View(operacaoCaixa);
        }

        // GET: OperacaoCaixa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OperacaoCaixa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Valor,Tipo,Operacao,Descricao")] OperacaoCaixa operacaoCaixa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operacaoCaixa);
                operacaoCaixa.DataHora = DateTime.Now;
                operacaoCaixa.IdFuncionario = LoginAtual.Id(User);
                await _context.SaveChangesAsync();
                return RedirectToAction("Caixa", "Caixa");
            }
            return View(operacaoCaixa);
        }

        private bool OperacaoCaixaExists(int id)
        {
            return _context.OperacaoCaixa.Any(e => e.Id == id);
        }
    }
}
