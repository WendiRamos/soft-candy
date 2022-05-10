
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
                var result = await _buscaService.FindByNomeCliente(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> BuscaClienteApagado(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByNomeClienteApagado(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaVendedor(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByNomeVendedor(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaVendedorApagado(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByNomeVendedorApagado(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaEstoquista(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByNomeEstoquista(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaEstoquistaApagado(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByNomeEstoquistaApagado(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

         public async Task<IActionResult> BuscaAdministrador(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByNomeAdministrador(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaAdministradorApagado(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByNomeAdministradorApagado(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaProduto(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByNome(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaProdutoApagado(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByNomeApagado(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaCategoria(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByNomeCategoria(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaCategoriaApagada(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByNomeCategoriaApagada(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaFornecedor(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByNomeFornecedor(Nome);
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuscaFornecedorApagado(string Nome)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _buscaService.FindByNomeFornecedorApagado(Nome);
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

        public async Task<IActionResult> BuscaCaixa(DateTime? minDate, DateTime? maxDate)
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
            var result = await _buscaService.FindByCaixa(minDate, maxDate);
            return View(result);
        }
    }
}