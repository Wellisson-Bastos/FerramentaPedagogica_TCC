using FerramentaPedagogica.Areas.Jogador.Services;
using Microsoft.AspNetCore.Mvc;

namespace FerramentaPedagogica.Areas.Jogador.Controllers
{
    [Area("Jogador")]
    [Route("Carregamento")]
    public class CarregamentoController : Controller
    {
        public IActionResult Index(int Jogador, int Sessao, int PerguntaAtual)
        {
            ViewData["Jogador"] = Jogador;
            ViewData["Sessao"] = Sessao;
            ViewData["PerguntaAtual"] = PerguntaAtual;

            return View();
        }

        [HttpPost]
        public IActionResult VerificarJogo(int Jogador, int Sessao, int PerguntaAtual)
        {
            var lCarregamentoService = new CarregamentoService();

            var lSessao = lCarregamentoService.BuscarSessao(Sessao);

            if (lSessao.Result == null)
            {
                return Json(new { success = false, sessionClosed = true, responseText = "A sessão foi encerrada. Redirecionando para a tela inicial...", url = Url.Action("Index", "Jogar")});
            }

            if (lSessao.Result.Finalizada.Value)
            {
                return Json(new { success = true, sessionClosed = false, url = Url.Action("Index", "Finalizacao", new { Area = "Jogador" }) });
            }

            if (lSessao.Result.PerguntaAtual != PerguntaAtual && lSessao.Result.PerguntaAtual != -1)
            {
                return Json(new { success = true, sessionClosed = false, url = Url.Action("Index", "Pergunta", new { Area = "Jogador", Jogador = Jogador, Sessao = Sessao, PerguntaAtual = PerguntaAtual }) });
            }

            return Json(new { success = false, sessionClosed = false, responseText= "Ocorreu um erro ao realizar a solicitação" });
        }
    }
}
