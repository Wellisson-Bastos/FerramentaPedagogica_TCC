using FerramentaPedagogica.Areas.Jogador.Models;
using FerramentaPedagogica.Areas.Jogador.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FerramentaPedagogica.Controllers
{
    [Area("Jogador")]
    [Route("Jogar")]
    public class JogarController : Controller
    {
        public IActionResult Index()
        {
            var lModel = new JogarViewModel();

            return View(lModel);
        }

        [HttpPost]
        public JsonResult IniciarJogo(JogarViewModel pModel)
        {
            var lJogarService = new JogarService();

            if (ModelState.IsValid)
            {
                var lExisteSessao = lJogarService.Buscar(pModel.CodigoSessao.Value);

                if (!lExisteSessao.Result)
                {
                    return Json(new { success = false, responseText = "A sessão inserida não foi localizada. Verifique o código." });
                }

                var lJogador = lJogarService.InserirJogador(pModel);

                return Json(new { success = true, url = Url.Action("Index", "Carregamento", new { Area = "Jogador", Pergunta = 0, Jogador = lJogador.Result.Codigo, Sessao = pModel.CodigoSessao.Value, PerguntaAtual = -1}) });
            }

            return Json(new { success = false, responseText = "Houve um erro na solicitação, tente novamente." });
        }
    }
}
