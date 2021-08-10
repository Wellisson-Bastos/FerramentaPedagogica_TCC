using FerramentaPedagogica.Areas.Administrador.Models;
using FerramentaPedagogica.Areas.Administrador.Services;
using Microsoft.AspNetCore.Mvc;

namespace FerramentaPedagogica.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    [Route("Favorito")]
    public class FavoritoController : Controller
    {
        [Route("Index")]
        public IActionResult Index(int Usuario)
        {
            var lModel = new FavoritoViewModel();

            var lFavoritoService = new FavoritoService();

            lModel.Usuario = Usuario;
            lModel.Jogos = lFavoritoService.ObterListaJogos(Usuario);

            return View(lModel);
        }

        [HttpGet]
        public IActionResult IniciarJogo(int Jogo)
        {
            var lFavoritoService = new FavoritoService();

            var lbPossuiPerguntas = lFavoritoService.PossuiPerguntas(Jogo);

            if (lbPossuiPerguntas)
            {
                return Json(new { success = true, url = Url.Action("Index", "Inicio", new { Area = "SessaoJogo", Jogo = Jogo }) });
            }
            else
            {
                return Json(new { success = false, responseText = "Este jogo não possui perguntas. Não é possível jogá-lo." });
            }
        }
    }
}
