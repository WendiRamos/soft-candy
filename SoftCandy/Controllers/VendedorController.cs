using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SoftCandy.Data;
using SoftCandy.Models;

namespace SoftCandy.Controllers
{
    public class VendedorController : Controller

    {
        private readonly SoftCandyContext _context;
        private readonly IConfiguration Configuration;

        public VendedorController(SoftCandyContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        // GET: Vendedor
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.Vendedor.ToListAsync());
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Relatorio()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.Vendedor.ToListAsync());
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Vendedor/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var vendedor = await _context.Vendedor
                    .FirstOrDefaultAsync(m => m.IdVendedor == id);
                if (vendedor == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(vendedor);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Vendedor/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Vendedor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVendedor,NomeVendedor,CelularVendedor,EnderecoVendedor,EmailVendedor,SenhaVendedor")] Vendedor vendedor)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(vendedor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(vendedor);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Vendedor/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var vendedor = await _context.Vendedor.FindAsync(id);
                if (vendedor == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                return View(vendedor);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Vendedor/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVendedor,NomeVendedor,CelularVendedor,EnderecoVendedor,EmailVendedor,SenhaVendedor")] Vendedor vendedor)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != vendedor.IdVendedor)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não corresponde!" });
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(vendedor);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        if (!VendedorExists(vendedor.IdVendedor))
                        {
                            return RedirectToAction(nameof(Error), new { message = e.Message });
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(vendedor);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Vendedor/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var vendedor = await _context.Vendedor
                    .FirstOrDefaultAsync(m => m.IdVendedor == id);
                if (vendedor == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(vendedor);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Vendedor/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var vendedor = await _context.Vendedor.FindAsync(id);
                _context.Vendedor.Remove(vendedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return null;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string EmailVendedor, string SenhaVendedor)
        {

            string strConexao = Configuration.GetConnectionString("SoftCandyContext");
            MySqlConnection SoftCandyContext = new MySqlConnection(strConexao);
            await SoftCandyContext.OpenAsync();
            MySqlCommand mySqlCommand = SoftCandyContext.CreateCommand();
            mySqlCommand.CommandText = $"SELECT * FROM vendedor WHERE EmailVendedor = '{EmailVendedor}' AND SenhaVendedor='{SenhaVendedor}'";

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            if (await reader.ReadAsync())
            {
                int Idlogin = reader.GetInt32(0);
                string nome = reader.GetString(1);

                List<Claim> direitosdeAcesso = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,Idlogin.ToString()),
                    new Claim(ClaimTypes.Name,nome)

                };

                var identity = new ClaimsIdentity(direitosdeAcesso, "Identity.Login");
                var userPrincipal = new ClaimsPrincipal(new[] { identity });

                await HttpContext.SignInAsync(userPrincipal,
                    new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTime.Now.AddHours(10)
                    });

                return RedirectToAction("Index", "Home");

            }
            return RedirectToAction(nameof(Error), new { message = "Usuário não encontrado! Verifique suas credenciais!" });



        }
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }

        private bool VendedorExists(int id)
        {
            return _context.Vendedor.Any(e => e.IdVendedor == id);
        }
        public IActionResult Error(string message)
        {
            if (User.Identity.IsAuthenticated)
            {
                var viewModel = new ErrorViewModel
                {
                    Message = message,
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return View(viewModel);
            }
            return RedirectToAction("Index", "Home");

        }
    }
}
