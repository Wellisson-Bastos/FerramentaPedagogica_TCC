using FerramentaPedagogica.Areas.SessaoJogo.Services.Pergunta;
using Microsoft.AspNetCore.Mvc;

namespace FerramentaPedagogica.Areas.SessaoJogo.Controllers
{
    [Area("SessaoJogo")]
    [Route("PerguntaSessao")]
    public class PerguntaSessaoController : Controller
    {
        public IActionResult Index(int Sessao, int Pergunta)
        {
            var lPerguntaService = new PerguntaSessaoService();

            var lModel = lPerguntaService.Buscar(++Pergunta, Sessao);

            if (lModel == null)
            {
                lPerguntaService.AtualizarSessao(Sessao);

                return RedirectToAction("Index", "Ranking", new { Sessao = Sessao });
            }

            return View(lModel);
        }
    }
}
