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
using SoftCandy.Enums;
using SoftCandy.Models;
using SoftCandy.Utils;

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
            if (LogadoComo.Administrador(User))
            {
                return View(await _context.Vendedor.Where(c => c.AtivoVendedor).ToListAsync());
            }
            return RedirectToAction("User", "Home");
        }

        public async Task<IActionResult> Relatorio()
        {
            if (LogadoComo.Administrador(User))
            {
                return View(await _context.Vendedor.Where(c => c.AtivoVendedor).ToListAsync());
            }
            return RedirectToAction("User", "Home");
        }

        // GET: Vendedor/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (LogadoComo.Administrador(User))
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
            return RedirectToAction("User", "Home");
        }

        // GET: Vendedor/Create
        public IActionResult Create()
        {
            if (LogadoComo.Administrador(User))
            {
                return View();
            }
            return RedirectToAction("User", "Home");
        }

        // POST: Vendedor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomeVendedor,CelularVendedor,LogradouroVendedor,NumeroVendedor,BairroVendedor,CidadeVendedor,EstadoVendedor,EmailVendedor,SenhaVendedor")] Vendedor vendedor)
        {
            if (LogadoComo.Administrador(User))
            {
                if (ModelState.IsValid)
                {
                    vendedor.AtivoVendedor = true;
                    _context.Add(vendedor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(vendedor);
            }
            return RedirectToAction("User", "Home");
        }

        // GET: Vendedor/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (LogadoComo.Administrador(User))
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
            return RedirectToAction("User", "Home");
        }

        // POST: Vendedor/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVendedor,NomeVendedor,CelularVendedor,LogradouroVendedor,NumeroVendedor,BairroVendedor,CidadeVendedor,EstadoVendedor,EmailVendedor,SenhaVendedor")] Vendedor vendedor)
        {
            if (LogadoComo.Administrador(User))
            {
                if (id != vendedor.IdVendedor)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não corresponde!" });
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        vendedor.AtivoVendedor = true;
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
            return RedirectToAction("User", "Home");
        }

        // GET: Vendedor/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (LogadoComo.Administrador(User))
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
            return RedirectToAction("User", "Home");
        }

        // POST: Vendedor/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (LogadoComo.Administrador(User))
            {
                var vendedor = await _context.Vendedor.FindAsync(id);
                vendedor.AtivoVendedor = false;
                _context.Vendedor.Update(vendedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("User", "Home");
        }

        // GET: Estoquista/Restore
        public async Task<IActionResult> Restore(int? id)
        {
            if (LogadoComo.Administrador(User))
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
            return RedirectToAction("User", "Home");
        }

        // POST: Vendedor/Restore
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRestore(int id)
        {
            if (LogadoComo.Administrador(User))
            {
                var vendedor = await _context.Vendedor.FindAsync(id);
                vendedor.AtivoVendedor = true;
                _context.Vendedor.Update(vendedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("User", "Home");
        }
        public IActionResult Login()
        {
            if (LogadoComo.Administrador(User))
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
                int Id = reader.GetInt32(0);
                string Nome = reader.GetString(1);
                string Tipo = Atores.VENDEDOR.ToString();

                List<Claim> direitosdeAcesso = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                    new Claim(ClaimTypes.Name, Nome),
                    new Claim(ClaimTypes.Actor, Tipo)
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
            if (LogadoComo.Vendedor(User))
            {
                await HttpContext.SignOutAsync();
            }
            return RedirectToAction("User", "Home");
        }

        private bool VendedorExists(int id)
        {
            return _context.Vendedor.Any(e => e.IdVendedor == id);
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
