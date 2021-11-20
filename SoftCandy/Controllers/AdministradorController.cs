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
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.Administrador.ToListAsync());
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Administrador/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
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
            return RedirectToAction("Index", "Home");
        }

        // GET: Administrador/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Administrador/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAdministrador,NomeAdministrador,CelularAdministrador,LogradouroAdministrador,NumeroAdministrador,BairroAdministrador,CidadeAdministrador,EstadoAdministrador,EmailAdministrador,SenhaAdministrador")] Administrador administrador)
        {
            if (User.Identity.IsAuthenticated)
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
            return RedirectToAction("Index", "Home");
        }

        // GET: Administrador/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
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
            return RedirectToAction("Index", "Home");
        }

        // POST: Administrador/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAdministrador,NomeAdministrador,CelularAdministrador,LogradouroAdministrador,NumeroAdministrador,BairroAdministrador,CidadeAdministrador,EstadoEstoquista,EmailEstoquista,SenhaEstoquista,AtivoAdministrador")] Administrador administrador)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != administrador.IdAdministrador)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não corresponde!" });
                }

                if (ModelState.IsValid)
                {
                    try
                    {
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
                return RedirectToAction("Index", "Home");
            }
            return View(administrador);
        }

        // GET: Administrador/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
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
            return RedirectToAction("Index", "Home");
        }

        // POST: Administrador/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var administrador = await _context.Administrador.FindAsync(id);
                administrador.AtivoAdministrador = false;
                _context.Administrador.Update(administrador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Administrador/Restore
        public async Task<IActionResult> Restore(int? id)
        {
            if (User.Identity.IsAuthenticated)
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
            return RedirectToAction("Index", "Home");
        }

        // POST: Administrador/Restore
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRestore(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var administrador = await _context.Administrador.FindAsync(id);
                administrador.AtivoAdministrador = true;
                _context.Administrador.Update(administrador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Home");
        }
        //GET: Administrador/Login
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
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

        private bool AdministradorExists(int id)
        {
            return _context.Administrador.Any(e => e.IdAdministrador == id);
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
