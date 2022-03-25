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
            if (LogadoComo.Vendedor(User) || LogadoComo.Administrador(User))
            {
                var softCandyContext = _context.Pedido
                    .Where(c => c.AtivoPedido)
                    .OrderByDescending(p => p.IdPedido)
                    .Include(c => c.Cliente)
                    .Include(f => f.Funcionario);

                return View(await softCandyContext.ToListAsync());

            }
            return RedirectToAction("User", "Home");

        }

        public async Task<IActionResult> Relatorio()
        {
            if (User.Identity.IsAuthenticated)
            {
                var softCandyContext = _context.Pedido.Where(c => c.AtivoPedido).Include(c => c.Cliente);

                return View(await softCandyContext.ToListAsync());
            }
            return RedirectToAction("Index", "Home");

        }
        // GET: Pedido/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (LogadoComo.Vendedor(User) || LogadoComo.Administrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var pedido = await _context.Pedido
                    .Include(p => p.Cliente)
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
        public List<Produto> BuscarProdutoPorNomeTop5(string TermoProcurado)
        {
            return _buscaService.FindByNomeProdutoTop5(TermoProcurado);
        }
        // GET: Pedido/Create
        public IActionResult Create()
        {
            if (LogadoComo.Vendedor(User) || LogadoComo.Administrador(User))
            {
                var model = new RealizarPedido();
                ViewData["IdCliente"] = new SelectList(_context.Cliente.Where(c => c.AtivoCliente), "IdCliente", "NomeCliente");
                return View(model);
            }
            return RedirectToAction("User", "Home");
        }

        // POST: Pedido/Create
        [HttpPost]
        public async Task<int> Create(List<ItemPedido> Itens, int IdCliente)
        {
            decimal total = 0;

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

                total += item.PrecoPago * item.Quantidade;
            }
            int idVendedor = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Pedido pedido = new Pedido(total, IdCliente, idVendedor, Itens)
            {
                AtivoPedido = true
            };
            _context.Add(pedido);

            try
            {
                _context.SaveChanges();
            }
            catch(Exception ) { }

            return pedido.IdPedido;
        }



        // GET: Pedido/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (LogadoComo.Vendedor(User) || LogadoComo.Administrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var pedido = await _context.Pedido
                    .Include(p => p.Cliente)
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
            if (LogadoComo.Vendedor(User) || LogadoComo.Administrador(User))
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

