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
    public class PedidoController : Controller
    {
        private readonly SoftCandyContext _context;
        private readonly BuscaService _buscaService;

        public PedidoController(SoftCandyContext context, BuscaService BuscaService)
        {
            _context = context;
            _buscaService = BuscaService;
        }

        // GET: Pedido
        public async Task<IActionResult> Index()
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var softCandyContext = _context.Pedido
                    .Where(c => c.AtivoPedido)
                    .OrderByDescending(p => p.IdPedido)
                    .Include(f => f.Funcionario);

                return View(await softCandyContext.ToListAsync());

            }
            return RedirectToAction("User", "Home");
        }

        public async Task<IActionResult> Relatorio()
        {
            if (User.Identity.IsAuthenticated)
            {
                var softCandyContext = _context.Pedido.Where(c => c.AtivoPedido);

                return View(await softCandyContext.ToListAsync());
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Pedido/Details
        public async Task<IActionResult> Details(int id)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var pedido = await _context.Pedido
                    .Include(f => f.Funcionario)
                    .Include(i => i.ItensPedidos)
                    .ThenInclude(it => it.Produto)
                    .FirstOrDefaultAsync(m => m.IdPedido == id);

                if (pedido.IdCliente != 0)
                {
                    var cliente = await _context.Cliente.FirstOrDefaultAsync(c => c.IdCliente == pedido.IdCliente);
                    pedido.Cliente = cliente;
                }

                if (pedido == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(pedido);
            }
            return RedirectToAction("User", "Home");
        }


        // GET: Pedido/Cupom
        public async Task<IActionResult> Cupom(int id)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var pedido = await _context.Pedido
                    .Include(f => f.Funcionario)
                    .Include(i => i.ItensPedidos)
                    .ThenInclude(it => it.Produto)
                    .FirstOrDefaultAsync(m => m.IdPedido == id);

                if (pedido.IdCliente != 0)
                {
                    var cliente = await _context.Cliente.FirstOrDefaultAsync(c => c.IdCliente == pedido.IdCliente);
                    pedido.Cliente = cliente;
                }

                if (pedido == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(pedido);
            }
            return RedirectToAction("User", "Home");
        }

        [HttpPost]
        public IActionResult GerarCupom(int IdPedido)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                return RedirectToAction("Cupom", "Pedido", new { id = IdPedido });
            }
            return RedirectToAction("User", "Home");
        }
        public List<Produto> BuscarProdutoPorNomeTop5(string TermoProcurado)
        {
            return _buscaService.FindByNomeProdutoTop5(TermoProcurado);
        }

        // GET: Pedido/Create
        public IActionResult Create()
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                if (CaixaUtils.IsAberto(_context))
                {
                    var model = new RealizarPedido();
                    ViewData["IdCliente"] = new SelectList(_context.Cliente.Where(c => c.AtivoCliente), "IdCliente", "NomeCliente");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Abertura", "Caixa");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Pedido/Create
        [HttpPost]
        public async Task<int> Create(List<ItemPedido> Itens, int? IdCliente)
        {
            foreach (ItemPedido item in Itens)
            {
                var produto = await _context.Produto.FirstOrDefaultAsync(p => p.IdProduto == item.IdProduto);
                item.PrecoPago = produto.PrecoVendaProduto;
                try
                {
                    if (produto.ProblemaAoSubtrair(item.Quantidade))
                    {
                        throw new Exception();
                    }
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            Pedido pedido = new Pedido()
            {
                AtivoPedido = true,
                DataPedido = DateTime.Now,
                IdFuncionario = LoginAtual.Id(User),
                IdCliente = IdCliente,
                IdCaixa = CaixaUtils.IdAberto(_context),
                ItensPedidos = Itens,
                Recebido = false
            };
            pedido.CalcularValorPedido();
            _context.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido.IdPedido;
        }



        // GET: Pedido/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var pedido = await _context.Pedido
                    .Include(f => f.Funcionario)
                    .Include(i => i.ItensPedidos)
                    .ThenInclude(it => it.Produto)
                    .FirstOrDefaultAsync(m => m.IdPedido == id);
                if (pedido == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(pedido);
            }
            return RedirectToAction("User", "Home");
        }

        // POST: Pedido/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {

                Pedido pedido = await _context.Pedido.Include(p => p.ItensPedidos).FirstOrDefaultAsync(p => p.IdPedido == id);

                Produto produto;

                foreach (ItemPedido item in pedido.ItensPedidos)
                {
                    produto = await _context.Produto.FirstOrDefaultAsync(p => p.IdProduto == item.IdProduto);

                    try
                    {
                        produto.devolver(item.Quantidade);
                        _context.Update(produto);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        return RedirectToAction(nameof(Error), new { message = "Ocorre um erro :(" });
                    }
                }

                pedido.AtivoPedido = false;
                _context.Pedido.Update(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("User", "Home");
        }
        public async Task<IActionResult> Receber(int id)
        {
            if (LoginAtual.IsVendedor(User) || LoginAtual.IsAdministrador(User))
            {
                var pedido = await _context.Pedido
                    .Include(c => c.Cliente)
                    .Include(f => f.Funcionario)
                    .Include(i => i.ItensPedidos)
                    .ThenInclude(it => it.Produto)
                    .FirstOrDefaultAsync(m => m.IdPedido == id);

                if (pedido == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                return View(pedido);
            }
            return RedirectToAction("User", "Home");
        }

        [HttpPost, ActionName("Receber")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Receber(int IdPedido, int FormaPagamento)
        {
            Pedido pedido = await _context.Pedido.Where(p => p.IdPedido == IdPedido).FirstAsync();
            pedido.FormaPagamento = (FormasPagamentoEnum)FormaPagamento;
            pedido.Recebido = true;
            Caixa caixaAberto = CaixaUtils.CaixaAberto(_context);
            caixaAberto.SomarEmValorVendas(pedido);
            _context.Update(caixaAberto);
            _context.Pedido.Update(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Pedido", new { id = IdPedido });
        }
        private bool PedidoExists(int id)
        {
            return _context.Pedido.Any(e => e.IdPedido == id);
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

