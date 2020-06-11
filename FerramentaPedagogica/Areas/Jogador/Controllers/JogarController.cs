using FerramentaPedagogica.Areas.Jogador.Models;
using Microsoft.AspNetCore.Mvc;

namespace FerramentaPedagogica.Areas.Jogador.Controllers
{
    [Area("Jogador")]
    [Route("Jogar")]
    public class JogarController : Controller
    {
        [HttpGet("/Jogar")]
        public IActionResult Index()
        {
            return View();
        }

        public void IniciarJogo(JogadorViewModel pModel)
        {
            if (ModelState.IsValid)
            {

            }
        }
    }
}
