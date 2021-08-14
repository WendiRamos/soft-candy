﻿
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
                var result = await _buscaService.FindByCliente(Nome);
                return View(result);
        }

        public async Task<IActionResult> BuscaVendedor(string Nome)
        {
            var result = await _buscaService.FindByVendedor(Nome);
            return View(result);
        }

        public async Task<IActionResult> BuscaProduto(string Nome)
        {
            var result = await _buscaService.FindByProduto(Nome);
            return View(result);
        }

    }
}