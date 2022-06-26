using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftCandy.Data;
using SoftCandy.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Controllers
{
    public class HomeController : Controller
    {
        private readonly SoftCandyContext _context;
        public HomeController(SoftCandyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var produtos = await _context.Produto
                    .Include(p => p.Lotes)
                    .Where(p => p.Ativo)
                    .ToListAsync();

                ViewData["MostrarAlerta"] = produtos
                    .Any(p => p.EstaEscasso() || p.MostrarNoCardVencido());

                return View();
            }
            return RedirectToAction("Login", "Funcionario");
        }


        public IActionResult Footer()
        {
            return View();
        }
    }
}
