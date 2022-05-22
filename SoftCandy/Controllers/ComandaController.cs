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
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var softCandyContext = _context.Comanda.Where(c => !c.Recebido)
                    .OrderByDescending(p => p.Id);

                return View(await softCandyContext.ToListAsync());

            }
            return RedirectToAction("User", "Home");
        }

        // GET: Comanda/CupomRecebimento
        public async Task<IActionResult> CupomRecebimento(int id)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var comanda = await _context.Comanda
                    .Include(i => i.ItensPedidos)
                    .ThenInclude(c => c.Lote)
                    .ThenInclude(c => c.Produto)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (comanda == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                return View(comanda);
            }
            return RedirectToAction("User", "Home");
        }

        // GET: Comanda/CupomCriação
        public async Task<IActionResult> CupomCriacao(int id)
        {
            var comanda = await _context.Comanda
                .FirstOrDefaultAsync(m => m.Id == id);

            if (comanda == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
            }
            return View(comanda);
        }

        // GET: Comanda/Details
        public async Task<IActionResult> Details(int id)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var comanda = await _context.Comanda
                    .Include(i => i.ItensPedidos)
                    .ThenInclude(c => c.Lote)
                    .ThenInclude(c => c.Produto)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (comanda == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(comanda);
            }
            return RedirectToAction("User", "Home");
        }

        // GET: Comanda/Top5
        public List<Produto> BuscarProdutoPorNomeTop5(string TermoProcurado)
        {
            return _buscaService.FindByNomeTop5(TermoProcurado);
        }

        // POST: Comanda/Create
        [HttpPost]
        public async Task<IActionResult> Create()
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

        //GET:Comanda/Venda
        public async Task<IActionResult> Venda()
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                if (CaixaUtils.IsAberto(_context))
                {
                    var lotes = await _context.Lote
                        .Where(lote => lote.DisponivelParaVenda())
                        .Include(lote => lote.Produto)
                        .OrderBy(lote => lote.Produto.Nome)
                        .ToListAsync();

                    return View(lotes);
                }
                else
                {
                    return RedirectToAction("Abertura", "Caixa");
                }
            }
            return RedirectToAction("User", "Home");
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
                    .Include(c => c.ItensPedidos)
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
            return RedirectToAction("User", "Home");
        }

        [HttpDelete/*, ActionName("RemoverItem")*/]
        public async Task<IActionResult> RemoverItem(int IdComanda, int IdItem)
        {
            var comanda = await _context.Comanda
                .Include(c => c.ItensPedidos)
                .ThenInclude(i => i.Lote)
                .FirstOrDefaultAsync(c => c.Id == IdComanda);

            if (comanda == null)
            {
                return Json("Comanda inexistente!");
            }

            var itemParaRemover = comanda.ItensPedidos.First(i => i.Id == IdItem);

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

        //GET:Comanda/Receber
        public async Task<IActionResult> Receber(int id)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var comanda = await _context.Comanda
                    .Include(i => i.ItensPedidos)
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
            return RedirectToAction("User", "Home");
        }

        [HttpPost, ActionName("Receber")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Receber(int Id, int FormaPagamento)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                Comanda comanda = await _context.Comanda.Where(p => p.Id == Id).FirstAsync();
                comanda.FormaPagamento = (FormasPagamentoEnum)FormaPagamento;
                comanda.Recebido = true;
                comanda.DataHoraRecebimento = DateTime.Now;
                Caixa caixaAberto = CaixaUtils.CaixaAberto(_context);
                caixaAberto.SomarEmValorVendas(comanda);
                _context.Update(caixaAberto);
                _context.Comanda.Update(comanda);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Comanda", new { id = Id });

            }
            return RedirectToAction("User", "Home");
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

