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

        public async Task<IActionResult> Index()
        {
            var model = new RealizarPedido();
            model.Produtos = _context.Produto.ToList();
            ViewData["ID_CLIENTE"] = new SelectList(_context.Cliente, "Id_Cliente", "Nome");
            return View(model);

        }
        
    }
}
