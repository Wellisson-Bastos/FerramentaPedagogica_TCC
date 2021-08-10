using FerramentaPedagogica.Areas.SessaoJogo.Models.Ranking;
using FerramentaPedagogica.Data;
using System.Linq;

namespace FerramentaPedagogica.Areas.SessaoJogo.Services.Ranking
{
    public class RankingService
    {
        public DTOJogador[] ObterRanking(int piCodigo)
        {
            using (var db = new studiesContext())
            {
                return db.Jogador
                     .Where(p => p.CodigoSessao == piCodigo)
                     .ToList()
                     .OrderByDescending(p => p.Pontuacao)
                     .Select((n, i) => new DTOJogador
                     {
                         Jogador = n.Jogador,
                         Pontuacao = n.Pontuacao,
                         Colocacao = i+1
                     }).ToArray();
            }
        }
    }
}
