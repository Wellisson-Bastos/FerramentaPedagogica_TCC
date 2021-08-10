using FerramentaPedagogica.Areas.SessaoJogo.Models.Ranking;
using FerramentaPedagogica.Areas.SessaoJogo.Services.Ranking;
using Microsoft.AspNetCore.Mvc;

namespace FerramentaPedagogica.Areas.SessaoJogo.Controllers
{
    [Area("SessaoJogo")]
    [Route("Ranking")]
    public class RankingController : Controller
    {
        public IActionResult Index(int Sessao)
        {
            var lRankingService = new RankingService();
            var lModel = new RankingViewModel();

            lModel.CodigoSessao = Sessao;
            lModel.Jogadores = lRankingService.ObterRanking(Sessao);

            return View(lModel);
        }
    }
}
