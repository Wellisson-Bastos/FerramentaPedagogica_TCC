using FerramentaPedagogica.Areas.SessaoJogo.Services.Inicio;
using Microsoft.AspNetCore.Mvc;

namespace FerramentaPedagogica.Areas.SessaoJogo.Controllers
{
    [Area("SessaoJogo")]
    [Route("Inicio")]
    public class InicioController : Controller
    {
        [Route("Index")]
        public IActionResult Index(int Jogo)
        {
            var lInicioService = new InicioService();

            var lModel = lInicioService.CriarSessao(Jogo);

            return View(lModel);
        }

        [HttpGet]
        public PartialViewResult _ListagemJogadores(int Sessao) 
        {
            var lInicioService = new InicioService();

            var lModel = lInicioService.BuscarJogadores(Sessao);

            return PartialView(lModel);
        }
    }
}
