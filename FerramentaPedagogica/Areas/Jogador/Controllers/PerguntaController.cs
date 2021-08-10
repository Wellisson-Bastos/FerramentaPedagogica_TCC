using FerramentaPedagogica.Areas.Jogador.Services;
using Microsoft.AspNetCore.Mvc;

namespace FerramentaPedagogica.Areas.Jogador.Controllers
{
    [Area("Jogador")]
    [Route("Pergunta")]
    public class PerguntaController : Controller
    {
        public IActionResult Index(int Jogador, int Sessao)
        {
            var lPerguntaService = new PerguntaService();

            var lModel = lPerguntaService.Buscar(Jogador, Sessao);

            return View(lModel);
        }

        [HttpPost]
        public IActionResult ResponderPergunta(int Jogador, int Sessao, int PerguntaAtual, string Resposta) 
        {
            var lPerguntaService = new PerguntaService();

            if (!string.IsNullOrEmpty(Resposta) || Resposta != "undefined")
            {
                lPerguntaService.AtualizarPontuacao(Jogador, Sessao, PerguntaAtual, Resposta);
            }

            return Json(new { success = true, url = Url.Action("Index", "Carregamento", new { Jogador = Jogador, Sessao = Sessao, PerguntaAtual = PerguntaAtual }) });
        }
    }
}
