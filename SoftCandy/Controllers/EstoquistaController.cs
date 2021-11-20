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
    public class EstoquistaController : Controller
    {
        private readonly SoftCandyContext _context;
        private readonly IConfiguration Configuration;

        public EstoquistaController(SoftCandyContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        // GET: Estoquista
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.Estoquista.ToListAsync());
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Estoquista/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
                }

                var estoquista = await _context.Estoquista
                    .FirstOrDefaultAsync(m => m.IdEstoquista == id);
                if (estoquista == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(estoquista);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Estoquista/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Estoquista/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstoquista,NomeEstoquista,CelularEstoquista,LogradouroEstoquista,NumeroEstoquista,BairroEstoquista,CidadeEstoquista,EstadoEstoquista,EmailEstoquista,SenhaEstoquista")] Estoquista estoquista)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    estoquista.AtivoEstoquista = true;
                    _context.Add(estoquista);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(estoquista);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Estoquista/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var estoquista = await _context.Estoquista.FindAsync(id);
                if (estoquista == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                return View(estoquista);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Estoquista/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstoquista,NomeEstoquista,CelularEstoquista,LogradouroEstoquista,NumeroEstoquista,BairroEstoquista,CidadeEstoquista,EstadoEstoquista,EmailEstoquista,SenhaEstoquista,AtivoEstoquista")] Estoquista estoquista)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != estoquista.IdEstoquista)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não corresponde!" });
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(estoquista);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        if (!EstoquistaExists(estoquista.IdEstoquista))
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
            return View(estoquista);
        }

        // GET: Estoquista/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
                }

                var estoquista = await _context.Estoquista
                    .FirstOrDefaultAsync(m => m.IdEstoquista == id);
                if (estoquista == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(estoquista);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Estoquista/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var estoquista = await _context.Estoquista.FindAsync(id);
                estoquista.AtivoEstoquista = false;
                _context.Estoquista.Update(estoquista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Estoquista/Restore
        public async Task<IActionResult> Restore(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var estoquista = await _context.Estoquista
                    .FirstOrDefaultAsync(m => m.IdEstoquista == id);
                if (estoquista == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(estoquista);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Estoquista/Restore
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRestore(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var estoquista = await _context.Estoquista.FindAsync(id);
                estoquista.AtivoEstoquista = true;
                _context.Estoquista.Update(estoquista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Home");
        }
        //GET: Estoquista/Login
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return null;
            }
            return View();
        }
        //Post: Estoquista/Login
        [HttpPost]
        public async Task<IActionResult> Login(string EmailEstoquista, string SenhaEstoquista)
        {

            string strConexao = Configuration.GetConnectionString("SoftCandyContext");
            MySqlConnection SoftCandyContext = new MySqlConnection(strConexao);
            await SoftCandyContext.OpenAsync();
            MySqlCommand mySqlCommand = SoftCandyContext.CreateCommand();
            mySqlCommand.CommandText = $"SELECT * FROM Estoquista WHERE EmailEstoquista = '{EmailEstoquista}' AND SenhaEstoquista='{SenhaEstoquista}'";

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

        private bool EstoquistaExists(int id)
        {
            return _context.Estoquista.Any(e => e.IdEstoquista == id);
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
