using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // GET: Comanda/Index
        public async Task<IActionResult> Index()
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var softCandyContext = _context.Comanda
                    .OrderByDescending(p => p.Id);

                return View(await softCandyContext.ToListAsync());

            }
            return RedirectToAction("User", "Home");
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
                var pedido = await _context.Comanda
                    .Include(i => i.ItensPedidos)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (pedido == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
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

        //GET:Comanda/Venda
        public async Task<IActionResult> Venda()
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var lotes = await _context.Lote
                    .Where(lote => lote.DisponivelParaVenda())
                    .Include(lote => lote.Produto)
                    .OrderBy(lote => lote.Produto.Nome)
                    .ToListAsync();

                return View(lotes);
            }
            return RedirectToAction("User", "Home");
        }

        [HttpPost, ActionName("Venda")]
        public async Task<IActionResult> Venda(int IdComanda, int IdLote, int Quantidade)
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

        //GET:Comanda/Receber
        public async Task<IActionResult> Receber(int id)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var pedido = await _context.Comanda
                    .Include(i => i.ItensPedidos)
                    //.ThenInclude(it => it.Produto)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (pedido == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                if (pedido.Recebido)
                {
                    return RedirectToAction("Details", "Pedido", new { id = id });
                }
                return View(pedido);
            }
            return RedirectToAction("User", "Home");
        }

        [HttpPost, ActionName("Receber")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Receber(int Id, int FormaPagamento)
        {
            Comanda pedido = await _context.Comanda.Where(p => p.Id == Id).FirstAsync();
            pedido.FormaPagamento = (FormasPagamentoEnum)FormaPagamento;
            pedido.Recebido = true;
            Caixa caixaAberto = CaixaUtils.CaixaAberto(_context);
            caixaAberto.SomarEmValorVendas(pedido);
            _context.Update(caixaAberto);
            _context.Comanda.Update(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Pedido", new { id = Id });
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

