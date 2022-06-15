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
    public class DeliveryController : Controller
    {
        private readonly SoftCandyContext _context;

        public DeliveryController(SoftCandyContext context)
        {
            _context = context;
        }

        // GET: Delivery
        public async Task<IActionResult> Historico()
        {
            var softCandyContext = _context.Delivery.Where(d => d.Recebido && d.Ativo).Include(d => d.Caixa).Include(d => d.Motoboy);
            return View(await softCandyContext.Take(20).ToListAsync());
        }

        // GET: Delivery
        public async Task<IActionResult> Pendentes()
        {
            var softCandyContext = _context.Delivery.Where(d => !d.Recebido && d.Ativo).Include(d => d.Caixa).Include(d => d.Motoboy);
            return View(await softCandyContext.Take(20).ToListAsync());
        }

        public async Task<IActionResult> EscolherItens(string procura)
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

                    ViewData["Carrinho"] = CarrinhoDelivery.Instancia;

                    return View(lotes);
                }
                else
                {
                    return RedirectToAction("Abertura", "Caixa");
                }
            }
            return RedirectToAction("Login", "Funcionario");
        }

        [HttpPost]
        public async Task<IActionResult> EscolherItens(int IdLote, int Quantidade)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var lote = await _context.Lote
                    .Include(lt => lt.Produto)
                    .FirstOrDefaultAsync(lt => lt.Id == IdLote);

                if (lote == null)
                {
                    return Json("Lote inválido!");
                }

                var carrinho = CarrinhoDelivery.Instancia;

                if (!lote.DecrementarVenda(Quantidade))
                {
                    return Json("Quantidade indiponível!");
                }

                if (carrinho.LoteIdJaEstaNoCarrinho(IdLote))
                {
                    var item = carrinho.ItensDelivery.First(i => i.Lote.Id == IdLote);
                    item.Quantidade += Quantidade;
                    carrinho.CalcularTotal();
                }
                else
                {
                    ItemDelivery itemDelivery = new ItemDelivery()
                    {
                        Id = CarrinhoDelivery.Instancia.Id,
                        Lote = lote,
                        Quantidade = Quantidade
                    };

                    carrinho.AdicionarItem(itemDelivery);
                }

                _context.Lote.Update(lote);
                await _context.SaveChangesAsync();
                return Json("");
            }
            return RedirectToAction("Login", "Funcionario");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoverItem(int IdItem)
        {
            var carrinho = CarrinhoDelivery.Instancia;
            var itemParaRemover = carrinho.ItensDelivery.First(i => i.Id == IdItem);

            if (itemParaRemover == null)
            {
                return Json("Item não existe no carrinho!");
            }

            var lote = await _context.Lote.FirstAsync(lt => lt.Id == itemParaRemover.Lote.Id);
            lote.DevolverQuantidade(itemParaRemover.Quantidade);

            carrinho.RemoverItem(itemParaRemover);
            _context.Lote.Update(lote);
            await _context.SaveChangesAsync();

            return Json("");

        }

        //GET:Comanda/Receber
        public async Task<IActionResult> Receber(int id)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var delivery = await _context.Delivery
                    .Include(i => i.ItensDelivery)
                    .ThenInclude(it => it.Lote)
                    .ThenInclude(c => c.Produto)
                    .Include(d => d.Motoboy)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (delivery == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                if (delivery.Recebido)
                {
                    return RedirectToAction("Details", "Delivery", new { id });
                }
                return View(delivery);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReceberDelivery(int Id)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                if (CaixaUtils.IsAberto(_context))
                {
                    Delivery delivery = await _context.Delivery.Where(p => p.Id == Id).FirstAsync();
                    delivery.Recebido = true;
                    delivery.DataHoraRecebimento = DateTime.Now;
                    delivery.IdCaixa = CaixaUtils.IdAberto(_context);
                    Caixa caixaAberto = CaixaUtils.CaixaAberto(_context);
                    caixaAberto.SomarEmValorDelivery(delivery);
                    _context.Update(caixaAberto);
                    _context.Delivery.Update(delivery);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Delivery", new { id = Id });
                }
                else
                {
                    return RedirectToAction("Abertura", "Caixa");
                }
            }
            return RedirectToAction("Login", "Funcionario");
        }

        [HttpPost]
        public async Task<IActionResult> LimparCarrinho()
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var carrinho = CarrinhoDelivery.Instancia;
                foreach (ItemDelivery i in carrinho.ItensDelivery)
                {
                    var lote = await _context.Lote.FirstAsync(lt => lt.Id == i.Lote.Id);
                    lote.DevolverQuantidade(i.Quantidade);
                    _context.Lote.Update(lote);
                }

                carrinho.LimparItens();
                await _context.SaveChangesAsync();

                return RedirectToAction("EscolherItens", "Delivery");
            }
            return RedirectToAction("Login", "Funcionario");
        }
        // GET: Delivery/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var delivery = await _context.Delivery
                .Include(d => d.Caixa)
                .Include(d => d.Motoboy)
                .Include(d => d.ItensDelivery)
                .ThenInclude(i => i.Lote)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // GET: Delivery/CupomRecebimento
        public async Task<IActionResult> CupomRecebimento(int id)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var delivery = await _context.Delivery
                    .Include(i => i.ItensDelivery)
                    .ThenInclude(c => c.Lote)
                    .ThenInclude(c => c.Produto)
                    .Include(d => d.Motoboy)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (delivery == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                return View(delivery);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Comanda/CupomCriação
        public async Task<IActionResult> CupomCriacao(int id)
        {

            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var delivery = await _context.Delivery
                    .Include(i => i.ItensDelivery)
                    .ThenInclude(c => c.Lote)
                    .ThenInclude(c => c.Produto)
                    .Include(d => d.Motoboy)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (delivery == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                return View(delivery);
            }
            return RedirectToAction("Login", "Funcionario");
        }

        // GET: Delivery/Create
        public IActionResult Create()
        {
            ViewData["Carrinho"] = CarrinhoDelivery.Instancia;
            ViewData["IdMotoboy"] = new SelectList(_context.Motoboy.Where(m => m.Ativo), "Id", "Nome");
            return View();
        }

        // POST: Delivery/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ValorFrete,EnderecoEntrega,IdMotoboy,NomeCliente,FormaPagamento")] Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                delivery.DataHoraCriacao = DateTime.Now;
                delivery.Recebido = false;
                delivery.Ativo = true;
                delivery.IdCaixa = CaixaUtils.IdAberto(_context);

                delivery.ItensDelivery = CarrinhoDelivery.Instancia.ItensDelivery;
                delivery.CalcularValor();

                delivery.ItensDelivery = CarrinhoDelivery.Instancia.ItensDelivery.Select(i => new ItemDelivery()
                {
                    Quantidade = i.Quantidade,
                    IdLote = i.Lote.Id,
                    Lote = null
                }).ToList();

                _context.Add(delivery);
                await _context.SaveChangesAsync();

                CarrinhoDelivery.Instancia.LimparItens();
                return RedirectToAction("Pendentes", "Delivery", new { id = delivery.Id });
            }
            return View(delivery);
        }

        // GET: Delivery/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var delivery = await _context.Delivery
                    .Include(i => i.ItensDelivery)
                    .ThenInclude(c => c.Lote)
                    .ThenInclude(c => c.Produto)
                    .Include(d => d.Motoboy)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (delivery == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(delivery);
            }
            return RedirectToAction("User", "Home");
        }

        // POST: Delivery/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (LoginAtual.IsEstoquista(User) || LoginAtual.IsAdministrador(User))
            {
                var delivery = await _context.Delivery.FindAsync(id);
                delivery.Ativo = false;
                _context.Delivery.Update(delivery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Pendentes));
            }
            return RedirectToAction("User", "Home");
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

        private bool DeliveryExists(int id)
        {
            return _context.Delivery.Any(e => e.Id == id);
        }
    }
}
