using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SoftCandy.Data;
using SoftCandy.Enums;
using SoftCandy.Models;
using SoftCandy.Utils;

namespace SoftCandy.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly SoftCandyContext _context;
        private readonly IConfiguration Configuration;

        public FuncionarioController(SoftCandyContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        // GET: Funcionario
        public async Task<IActionResult> IndexAdministrador()
        {
            if (LoginAtual.IsAdministrador(User))
            {
                return View(await _context.Funcionario.Where(c => c.Ativo && c.Cargo == ((int)CargosEnum.ADMINISTRADOR)).ToListAsync());
            }
            return RedirectToAction("User", "Home");
        }

        public async Task<IActionResult> IndexEstoquista()
        {
            if (LoginAtual.IsAdministrador(User))
            {
                return View(await _context.Funcionario.Where(c => c.Ativo && c.Cargo == ((int)CargosEnum.ESTOQUISTA)).ToListAsync());
            }
            return RedirectToAction("User", "Home");
        }

        public async Task<IActionResult> IndexVendedor()
        {
            if (LoginAtual.IsAdministrador(User))
            {
                return View(await _context.Funcionario.Where(c => c.Ativo && c.Cargo == ((int)CargosEnum.VENDEDOR)).ToListAsync());
            }
            return RedirectToAction("User", "Home");
        }
        // GET: Funcionario/Details
        public async Task<IActionResult> DetailsAdministrador(int? id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
                }

                var funcionario = await _context.Funcionario
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (funcionario == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(funcionario);
            }
            return RedirectToAction("User", "Home");
        }


        public async Task<IActionResult> DetailsEstoquista(int? id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
                }

                var funcionario = await _context.Funcionario
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (funcionario == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(funcionario);
            }
            return RedirectToAction("User", "Home");
        }

        public async Task<IActionResult> DetailsVendedor(int? id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
                }

                var funcionario = await _context.Funcionario
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (funcionario == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(funcionario);
            }
            return RedirectToAction("User", "Home");
        }
        // GET: Funcionario/Create
        public IActionResult CreateAdministrador()
        {
            if (LoginAtual.IsAdministrador(User))
            {
                return View();
            }
            return RedirectToAction("User", "Home");
        }

        public IActionResult CreateEstoquista()
        {
            if (LoginAtual.IsAdministrador(User))
            {
                return View();
            }
            return RedirectToAction("User", "Home");
        }

        public IActionResult CreateVendedor()
        {
            if (LoginAtual.IsAdministrador(User))
            {
                return View();
            }
            return RedirectToAction("User", "Home");
        }
        // POST: Funcionario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdministrador([Bind("Nome,Celular,Logradouro,Numero,Bairro,Cidade,Estado,Email,Senha,Cargo")] Funcionario funcionario)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (ModelState.IsValid)
                {
                    funcionario.Ativo = true;
                    funcionario.Cargo = 1;
                    _context.Add(funcionario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(IndexAdministrador));
                }
                return View(funcionario);
            }
            return RedirectToAction("User", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEstoquista([Bind("Nome,Celular,Logradouro,Numero,Bairro,Cidade,Estado,Email,Senha,Cargo")] Funcionario funcionario)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (ModelState.IsValid)
                {
                    funcionario.Ativo = true;
                    funcionario.Cargo = 2;
                    _context.Add(funcionario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(IndexEstoquista));
                }
                return View(funcionario);
            }
            return RedirectToAction("User", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVendedor([Bind("Nome,Celular,Logradouro,Numero,Bairro,Cidade,Estado,Email,Senha,Cargo")] Funcionario funcionario)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (ModelState.IsValid)
                {
                    funcionario.Ativo = true;
                    funcionario.Cargo = 3;
                    _context.Add(funcionario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(IndexVendedor));
                }
                return View(funcionario);
            }
            return RedirectToAction("User", "Home");
        }
        // GET: Funcionario/Edit
        public async Task<IActionResult> EditAdministrador(int? id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var funcionario = await _context.Funcionario.FindAsync(id);
                if (funcionario == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                return View(funcionario);
            }
            return RedirectToAction("User", "Home");
        }

        public async Task<IActionResult> EditEstoquista(int? id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var funcionario = await _context.Funcionario.FindAsync(id);
                if (funcionario == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                return View(funcionario);
            }
            return RedirectToAction("User", "Home");
        }

        public async Task<IActionResult> EditVendedor(int? id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var funcionario = await _context.Funcionario.FindAsync(id);
                if (funcionario == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }
                return View(funcionario);
            }
            return RedirectToAction("User", "Home");
        }
        // POST: Funcionario/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdministrador(int id, [Bind("Id,Nome,Celular,Logradouro,Numero,Bairro,Cidade,Estado,Email,Senha,Cargo")] Funcionario funcionario)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (id != funcionario.Id)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não corresponde!" });
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        funcionario.Ativo = true;
                        funcionario.Cargo = 1;
                        _context.Update(funcionario);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        if (!FuncionarioExists(funcionario.Id))
                        {
                            return RedirectToAction(nameof(Error), new { message = e.Message });
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(IndexAdministrador));
                }
                return RedirectToAction("User", "Home");
            }
            return View(funcionario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEstoquista(int id, [Bind("Id,Nome,Celular,Logradouro,Numero,Bairro,Cidade,Estado,Email,Senha,Cargo")] Funcionario funcionario)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (id != funcionario.Id)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não corresponde!" });
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        funcionario.Ativo = true;
                        funcionario.Cargo = 2;
                        _context.Update(funcionario);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        if (!FuncionarioExists(funcionario.Id))
                        {
                            return RedirectToAction(nameof(Error), new { message = e.Message });
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(IndexEstoquista));
                }
                return RedirectToAction("User", "Home");
            }
            return View(funcionario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVendedor(int id, [Bind("Id,Nome,Celular,Logradouro,Numero,Bairro,Cidade,Estado,Email,Senha,Cargo")] Funcionario funcionario)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (id != funcionario.Id)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não corresponde!" });
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        funcionario.Ativo = true;
                        funcionario.Cargo = 3;
                        _context.Update(funcionario);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        if (!FuncionarioExists(funcionario.Id))
                        {
                            return RedirectToAction(nameof(Error), new { message = e.Message });
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(IndexVendedor));
                }
                return RedirectToAction("User", "Home");
            }
            return View(funcionario);
        }
        // GET: Funcionario/Delete
        public async Task<IActionResult> DeleteAdministrador(int? id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
                }

                var funcionario = await _context.Funcionario
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (funcionario == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(funcionario);
            }
            return RedirectToAction("User", "Home");
        }

        public async Task<IActionResult> DeleteEstoquista(int? id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
                }

                var funcionario = await _context.Funcionario
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (funcionario == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(funcionario);
            }
            return RedirectToAction("User", "Home");
        }

        public async Task<IActionResult> DeleteVendedor(int? id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
                }

                var funcionario = await _context.Funcionario
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (funcionario == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(funcionario);
            }
            return RedirectToAction("User", "Home");
        }
        // POST: Funcionario/Delete
        [HttpPost, ActionName("DeleteAdministrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAdministrador(int id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                var funcionario = await _context.Funcionario.FindAsync(id);
                funcionario.Ativo = false;
                funcionario.Cargo = 1;
                _context.Funcionario.Update(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexAdministrador));
            }
            return RedirectToAction("User", "Home");
        }

        [HttpPost, ActionName("DeleteEstoquista")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedEstoquista(int id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                var funcionario = await _context.Funcionario.FindAsync(id);
                funcionario.Ativo = false;
                funcionario.Cargo = 2;
                _context.Funcionario.Update(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexEstoquista));
            }
            return RedirectToAction("User", "Home");
        }

        [HttpPost, ActionName("DeleteVendedor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedVendedor(int id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                var funcionario = await _context.Funcionario.FindAsync(id);
                funcionario.Ativo = false;
                funcionario.Cargo = 3;
                _context.Funcionario.Update(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexVendedor));
            }
            return RedirectToAction("User", "Home");
        }
        // GET: Funcionario/Restore
        public async Task<IActionResult> RestoreAdministrador(int? id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var funcionario = await _context.Funcionario
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (funcionario == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(funcionario);
            }
            return RedirectToAction("User", "Home");
        }

        public async Task<IActionResult> RestoreEstoquista(int? id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var funcionario = await _context.Funcionario
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (funcionario == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(funcionario);
            }
            return RedirectToAction("User", "Home");
        }

        public async Task<IActionResult> RestoreVendedor(int? id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
                }

                var funcionario = await _context.Funcionario
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (funcionario == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não existe!" });
                }

                return View(funcionario);
            }
            return RedirectToAction("User", "Home");
        }

        // POST: Funcionario/Restore
        [HttpPost, ActionName("RestoreAdministrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreConfirmedAdministrador(int id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                var funcionario = await _context.Funcionario.FindAsync(id);
                funcionario.Ativo = true;
                funcionario.Cargo = 1;
                _context.Funcionario.Update(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexAdministrador));
            }
            return RedirectToAction("User", "Home");
        }

        [HttpPost, ActionName("RestoreEstoquista")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreConfirmedEstoquista(int id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                var funcionario = await _context.Funcionario.FindAsync(id);
                funcionario.Ativo = true;
                funcionario.Cargo = 2;
                _context.Funcionario.Update(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexEstoquista));
            }
            return RedirectToAction("User", "Home");
        }

        [HttpPost, ActionName("RestoreVendedor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreConfirmedVendedor(int id)
        {
            if (LoginAtual.IsAdministrador(User))
            {
                var funcionario = await _context.Funcionario.FindAsync(id);
                funcionario.Ativo = true;
                funcionario.Cargo = 3;
                _context.Funcionario.Update(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexVendedor));
            }
            return RedirectToAction("User", "Home");
        }

        //GET: Funcionario/Login
        public IActionResult Login()
        {
            return View();
        }
        //Post: Funcionario/Login
        [HttpPost]
        public async Task<IActionResult> Login(string Email, string Senha)
        {

            string strConexao = Configuration.GetConnectionString("SoftCandyContext");
            MySqlConnection SoftCandyContext = new MySqlConnection(strConexao);
            await SoftCandyContext.OpenAsync();
            MySqlCommand mySqlCommand = SoftCandyContext.CreateCommand();
            mySqlCommand.CommandText = $"SELECT * FROM Funcionario WHERE Email = '{Email}' AND Senha='{Senha}'";

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            if (await reader.ReadAsync())
            {
                int Id = reader.GetInt32("Id");
                string Nome = reader.GetString("Nome");
                int Cargo = reader.GetInt32("Cargo");

                List<Claim> direitosdeAcesso = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                    new Claim(ClaimTypes.Name, Nome),
                    new Claim(ClaimTypes.Actor, Cargo.ToString())
                };

                var identity = new ClaimsIdentity(direitosdeAcesso, "Identity.Login");
                var userPrincipal = new ClaimsPrincipal(new[] { identity });

                Thread.CurrentPrincipal = userPrincipal;

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
            return RedirectToAction("Login", "Funcionario");
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionario .Any(e => e.Id == id);
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
