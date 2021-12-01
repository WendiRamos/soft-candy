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
    public class AdministradorController : Controller
    {
        private readonly SoftCandyContext _context;
        private readonly IConfiguration Configuration;

        public AdministradorController(SoftCandyContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        // GET: Administrador
        public async Task<IActionResult> Index()
        {
            if (LogadoComo.Administrador(User))
            {
                return View(await _context.Administrador.Where(c => c.AtivoAdministrador).ToListAsync());
            }
            return RedirectToAction("User", "Home");
        }

        // GET: Administrador/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (LogadoComo.Administrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
                }

                var administrador = await _context.Administrador
                    .FirstOrDefaultAsync(m => m.IdAdministrador == id);
                if (administrador == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(administrador);
            }
            return RedirectToAction("User", "Home");
        }

        // GET: Administrador/Create
        public IActionResult Create()
        {
            if (LogadoComo.Administrador(User))
            {
                return View();
            }
            return RedirectToAction("User", "Home");
        }

        // POST: Administrador/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomeAdministrador,CelularAdministrador,LogradouroAdministrador,NumeroAdministrador,BairroAdministrador,CidadeAdministrador,EstadoAdministrador,EmailAdministrador,SenhaAdministrador")] Administrador administrador)
        {
            if (LogadoComo.Administrador(User))
            {
                if (ModelState.IsValid)
                {
                    administrador.AtivoAdministrador = true;
                    _context.Add(administrador);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(administrador);
            }
            return RedirectToAction("User", "Home");
        }

        // GET: Administrador/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (LogadoComo.Administrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var administrador = await _context.Administrador.FindAsync(id);
                if (administrador == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                return View(administrador);
            }
            return RedirectToAction("User", "Home");
        }

        // POST: Administrador/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAdministrador,NomeAdministrador,CelularAdministrador,LogradouroAdministrador,NumeroAdministrador,BairroAdministrador,CidadeAdministrador,EstadoAdministrador,EmailAdministrador,SenhaAdministrador")] Administrador administrador)
        {
            if (LogadoComo.Administrador(User))
            {
                if (id != administrador.IdAdministrador)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não corresponde!" });
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        administrador.AtivoAdministrador = true;
                        _context.Update(administrador);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        if (!AdministradorExists(administrador.IdAdministrador))
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
                return RedirectToAction("User", "Home");
            }
            return View(administrador);
        }

        // GET: Administrador/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (LogadoComo.Administrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
                }

                var administrador = await _context.Administrador
                    .FirstOrDefaultAsync(m => m.IdAdministrador == id);
                if (administrador == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(administrador);
            }
            return RedirectToAction("User", "Home");
        }

        // POST: Administrador/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (LogadoComo.Administrador(User))
            {
                var administrador = await _context.Administrador.FindAsync(id);
                administrador.AtivoAdministrador = false;
                _context.Administrador.Update(administrador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("User", "Home");
        }

        // GET: Administrador/Restore
        public async Task<IActionResult> Restore(int? id)
        {
            if (LogadoComo.Administrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var administrador = await _context.Administrador
                    .FirstOrDefaultAsync(m => m.IdAdministrador == id);
                if (administrador == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(administrador);
            }
            return RedirectToAction("User", "Home");
        }

        // POST: Administrador/Restore
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRestore(int id)
        {
            if (LogadoComo.Administrador(User))
            {
                var administrador = await _context.Administrador.FindAsync(id);
                administrador.AtivoAdministrador = true;
                _context.Administrador.Update(administrador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("User", "Home");
        }
        //GET: Administrador/Login
        public IActionResult Login()
        {
            if (LogadoComo.Administrador(User))
            {
                return null;
            }
            return View();
        }
        //Post: Administrador/Login
        [HttpPost]
        public async Task<IActionResult> Login(string EmailAdministrador, string SenhaAdministrador)
        {

            string strConexao = Configuration.GetConnectionString("SoftCandyContext");
            MySqlConnection SoftCandyContext = new MySqlConnection(strConexao);
            await SoftCandyContext.OpenAsync();
            MySqlCommand mySqlCommand = SoftCandyContext.CreateCommand();
            mySqlCommand.CommandText = $"SELECT * FROM Administrador WHERE EmailAdministrador = '{EmailAdministrador}' AND SenhaAdministrador='{SenhaAdministrador}'";

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            if (await reader.ReadAsync())
            {
                int Id = reader.GetInt32(0);
                string Nome = reader.GetString(1);
                string Tipo = Atores.ADMINISTRADOR.ToString();

                List<Claim> direitosdeAcesso = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,Id.ToString()),
                    new Claim(ClaimTypes.Name,Nome),
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
            if (LogadoComo.Administrador(User))
            {
                await HttpContext.SignOutAsync();
            }
            return RedirectToAction("User", "Home");
        }

        private bool AdministradorExists(int id)
        {
            return _context.Administrador.Any(e => e.IdAdministrador == id);
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
