using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Controllers
{
    public class Relatorio : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
