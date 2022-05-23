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
        public async Task<IActionResult> Index()
        {
            var softCandyContext = _context.Delivery.Include(d => d.Caixa).Include(d => d.Motoboy);
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
            return RedirectToAction("User", "Home");
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

                ItemDelivery itemDelivery = new ItemDelivery()
                {
                    Id = CarrinhoDelivery.Instancia.Id,
                    Lote = lote,
                    Quantidade = Quantidade
                };

                carrinho.AdicionarItem(itemDelivery);

                _context.Lote.Update(lote);
                await _context.SaveChangesAsync();
                return Json("");
            }
            return RedirectToAction("User", "Home");
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

            itemParaRemover.Lote.DevolverQuantidade(itemParaRemover.Quantidade);

            carrinho.RemoverItem(itemParaRemover);
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
                if (CaixaUtils.IsAberto(_context))
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
                else
                {
                    return RedirectToAction("Abertura", "Caixa");
                }
            }
            return RedirectToAction("User", "Home");
        }
        // GET: Delivery/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var delivery = await _context.Delivery
                .Include(d => d.Caixa)
                .Include(d => d.Motoboy)
                .Include(d => d.ItensDelivery)
                .ThenInclude(i => i.Lote)
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
        public async Task<IActionResult> Create([Bind("Id,ValorTotal,DataHoraCriacao,DataHoraRecebimento,EnderecoEntrega,Recebido,IdCaixa,IdMotoboy,FormaPagamento")] Delivery delivery)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,ValorTotal,DataHoraCriacao,DataHoraRecebimento,EnderecoEntrega,Recebido,IdCaixa,IdMotoboy,FormaPagamento")] Delivery delivery)
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
                .Include(d => d.ItensDelivery)
                .ThenInclude(i => i.Lote)
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
