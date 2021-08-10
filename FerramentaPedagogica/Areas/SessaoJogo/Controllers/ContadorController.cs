using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FerramentaPedagogica.Areas.SessaoJogo.Controllers
{
    [Area("SessaoJogo")]
    [Route("Contador")]
    public class ContadorController : Controller
    {
        public IActionResult Index(int Sessao, int Pergunta)
        {
            ViewData["Sessao"] = Sessao;
            ViewData["Pergunta"] = Pergunta;

            return View();
        }
    }
}
