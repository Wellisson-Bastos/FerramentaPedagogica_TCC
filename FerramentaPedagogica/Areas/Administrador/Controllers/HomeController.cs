using FerramentaPedagogica.Areas.Administrador.Models;
using FerramentaPedagogica.Areas.Administrador.Services.Home;
using Microsoft.AspNetCore.Mvc;

namespace FerramentaPedagogica.Controllers
{
    [Area("Administrador")]
    [Route("Home")]
    public class HomeController : Controller
    {
        [Route("Index")]
        public IActionResult Index(int Usuario)
        {
            var lModel = new HomeViewModel();

            var lHomeService = new HomeService();

            lModel.Usuario = Usuario;
            lModel.Jogos = lHomeService.ObterListaJogos(Usuario);

            return View(lModel);
        }

        [HttpPost]
        public IActionResult ExcluirJogo(int Jogo) 
        {
            var lHomeService = new HomeService();

            var lbExcluidoComSucesso = lHomeService.ExcluirJogo(Jogo);

            if (lbExcluidoComSucesso.Result)
            {
                return Json(new { success = true, responseText = "Exclusão realizada com sucesso!" });
            }
            else 
            {
                return Json(new { success = false, responseText = "Houve um erro na solicitação, tente novamente." });
            }
        }

        [HttpGet]
        public IActionResult IniciarJogo(int Jogo)
        {
            var lHomeService = new HomeService();

            var lbPossuiPerguntas = lHomeService.PossuiPerguntas(Jogo);

            if (lbPossuiPerguntas)
            {
                return Json(new { success = true, url = Url.Action("Index", "Inicio", new { Area = "SessaoJogo", Jogo = Jogo }) });
            }
            else
            {
                return Json(new { success = false, responseText = "Crie perguntas para jogar." });
            }
        }
    }
}
