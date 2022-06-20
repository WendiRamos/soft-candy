using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftCandy.Data;
using SoftCandy.Enums;
using SoftCandy.Models;
using SoftCandy.Services;
using SoftCandy.Utils;

namespace SoftCandy.Controllers
{
    public class ComandaController : Controller
    {
        private readonly SoftCandyContext _context;
        private readonly BuscaService _buscaService;

        public ComandaController(SoftCandyContext context, BuscaService BuscaService)
        {
            _context = context;
            _buscaService = BuscaService;
        }

        // GET: Comanda/Abertas
        public async Task<IActionResult> Abertas()
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User) || LoginAtual.IsCaixa(User))
            {
                var softCandyContext = _context.Comanda.Where(c => !c.Recebido)
                    .OrderByDescending(p => p.Id);

                return View(await softCandyContext.Take(20).ToListAsync());

            }
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Comanda/Histórico
        public async Task<IActionResult> Historico()
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User) || LoginAtual.IsCaixa(User))
            {
                var softCandyContext = _context.Comanda.Where(c => c.Recebido)
                    .OrderByDescending(p => p.Id);

                return View(await softCandyContext.Take(20).ToListAsync());

            }
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Comanda/CupomRecebimento
        public async Task<IActionResult> CupomRecebimento(int id)
        {
            if (LoginAtual.IsCaixa(User) || LoginAtual.IsAdministrador(User))
            {
                var comanda = await _context.Comanda
                    .Include(i => i.ItemComanda)
                    .ThenInclude(c => c.Lote)
                    .ThenInclude(c => c.Produto)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (comanda == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                return View(comanda);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Comanda/CupomCriação
        public async Task<IActionResult> CupomCriacao(int id)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var comanda = await _context.Comanda
                .FirstOrDefaultAsync(m => m.Id == id);

                if (comanda == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                return View(comanda);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Comanda/Details
        public async Task<IActionResult> Details(int id)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User) || LoginAtual.IsCaixa(User))
            {
                var comanda = await _context.Comanda
                    .Include(i => i.ItemComanda)
                    .ThenInclude(c => c.Lote)
                    .ThenInclude(c => c.Produto)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (comanda == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(comanda);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // POST: Comanda/Create
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                if (CaixaUtils.IsAberto(_context))
                {
                    Comanda comanda = new Comanda()
                    {
                        DataHoraCriacao = DateTime.Now,
                        IdCaixa = CaixaUtils.IdAberto(_context),
                        Recebido = false
                    };
                    _context.Comanda.Add(comanda);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("CupomCriacao", "Comanda", new { id = comanda.Id });
                }
                else
                {
                    return RedirectToAction("Abertura", "Caixa");
                }
            }
            return RedirectToAction("Login", "Funcionario");
        }

        public async Task<IActionResult> Venda(string procura)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                if (CaixaUtils.IsAberto(_context))
                {
                    if (procura == null)
                    {
                        procura = "";
                    }

                    var lotes = await _context.Lote
                        .Where(lote => lote.DisponivelParaVenda())
                        .Where(lote => lote.Produto.Nome.IndexOf(procura, 0, System.StringComparison.CurrentCultureIgnoreCase) != -1)
                        .Include(lote => lote.Produto)
                        .OrderBy(lote => lote.Produto.Nome)
                        .Take(20)
                        .ToListAsync();

                    return View(lotes);
                }
                else
                {
                    return RedirectToAction("Abertura", "Caixa");
                }
            }
            return RedirectToAction("Login", "Funcionario");
        }

        [HttpPost, ActionName("Venda")]
        public async Task<IActionResult> Venda(int IdComanda, int IdLote, int Quantidade)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var lote = await _context.Lote
                        .FirstOrDefaultAsync(lt => lt.Id == IdLote);

                if (lote == null)
                {
                    return Json("Lote inválido!");
                }

                var comanda = await _context.Comanda
                    .Include(c => c.ItemComanda)
                    .ThenInclude(i => i.Lote)
                    .FirstOrDefaultAsync(c => c.Id == IdComanda);

                if (comanda == null)
                {
                    return Json("Comanda inválida!");
                }

                if (comanda.Recebido == true)
                {
                    return Json("Comanda já recebida!");
                }

                if (!lote.DecrementarVenda(Quantidade))
                {
                    return Json("Quantidade indiponível!");
                }

                ItemComanda itemComanda = new ItemComanda()
                {
                    Lote = lote,
                    Comanda = comanda,
                    Quantidade = Quantidade
                };

                comanda.AdicionarItem(itemComanda);
                _context.Comanda.Update(comanda);
                _context.Lote.Update(lote);
                await _context.SaveChangesAsync();
                return Json("");
            }
            return RedirectToAction("Login", "Funcionario");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoverItem(int IdComanda, int IdItem)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var comanda = await _context.Comanda
                .Include(c => c.ItemComanda)
                .ThenInclude(i => i.Lote)
                .FirstOrDefaultAsync(c => c.Id == IdComanda);

                if (comanda == null)
                {
                    return Json("Comanda inexistente!");
                }

                var itemParaRemover = comanda.ItemComanda.First(i => i.Id == IdItem);

                if (itemParaRemover == null)
                {
                    return Json("Item não existe nessa comanda!");
                }

                itemParaRemover.Lote.DevolverQuantidade(itemParaRemover.Quantidade);
                comanda.RemoverItem(itemParaRemover);
                _context.Comanda.Update(comanda);
                _context.Lote.Update(itemParaRemover.Lote);
                await _context.SaveChangesAsync();

                return Json("");
            }
            return RedirectToAction("Login", "Funcionario");

        }

        //GET:Comanda/Receber
        public async Task<IActionResult> Receber(int id)
        {

            if (LoginAtual.IsCaixa(User) || LoginAtual.IsAdministrador(User))
            {
                var comanda = await _context.Comanda
                    .Include(i => i.ItemComanda)
                    .ThenInclude(it => it.Lote)
                    .ThenInclude(c => c.Produto)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (comanda == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                if (comanda.Recebido)
                {
                    return RedirectToAction("Details", "Comanda", new { id = id });
                }
                return View(comanda);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        [HttpPost, ActionName("Receber")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Receber(int Id, int FormaPagamento)
        {

            if (LoginAtual.IsCaixa(User) || LoginAtual.IsAdministrador(User))
            {
                if (CaixaUtils.IsAberto(_context))
                {
                    Comanda comanda = await _context.Comanda.Where(p => p.Id == Id).FirstAsync();
                    comanda.FormaPagamento = (FormasPagamentoEnum)FormaPagamento;
                    comanda.Recebido = true;
                    comanda.DataHoraRecebimento = DateTime.Now;
                    comanda.IdCaixa = CaixaUtils.IdAberto(_context);
                    Caixa caixaAberto = CaixaUtils.CaixaAberto(_context);
                    caixaAberto.SomarEmValorVendas(comanda);
                    _context.Update(caixaAberto);
                    _context.Comanda.Update(comanda);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Comanda", new { id = Id });
                }
                else
                {
                    return RedirectToAction("Abertura", "Caixa");
                }
            }
            return RedirectToAction("Login", "Funcionario");
        }

        private bool ComandaExists(int id)
        {
            return _context.Comanda.Any(e => e.Id == id);
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

