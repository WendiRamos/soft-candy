
using SoftCandy.Services;
using Microsoft.AspNetCore.Mvc;
using SoftCandy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Controllers
{
    public class BuscaController : Controller
    {
        private readonly BuscaService _buscaService;

        public BuscaController(BuscaService BuscaService)
        {
            _buscaService = BuscaService;
        }

        public async Task<IActionResult> BuscaCliente(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByCliente(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> BuscaClienteApagado(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByClienteApagado(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaVendedor(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByVendedor(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaVendedorApagado(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByVendedorApagado(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaEstoquista(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByEstoquista(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaEstoquistaApagado(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByEstoquistaApagado(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaProduto(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByProduto(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaProdutoApagado(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByProdutoApagado(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaCategoria(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByCategoria(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaCategoriaApagada(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByCategoriaApagada(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaFornecedor(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByFornecedor(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaFornecedorApagado(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByFornecedorApagado(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> BuscaPedido(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _buscaService.FindByPedido(minDate, maxDate);
            return View(result);
        }
    }
}