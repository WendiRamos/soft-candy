using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoftCandy.Data;
using SoftCandy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Controllers
{
    public class RealizarPedidoController : Controller
    {
        private readonly SoftCandyContext _context;

        public RealizarPedidoController(SoftCandyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = new RealizarPedido();
                model.Produtos = _context.Produto.ToList();
                ViewData["ID_CLIENTE"] = new SelectList(_context.Cliente, "IdCliente", "NomeCliente");
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
