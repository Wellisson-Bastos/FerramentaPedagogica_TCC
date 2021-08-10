namespace FerramentaPedagogica.Areas.SessaoJogo.Models.Ranking
{
    public class RankingViewModel
    {
        public int CodigoSessao { get; set; }
        public DTOJogador[] Jogadores { get; set; }
    }

    public class DTOJogador 
    {
        public int Colocacao { get; set; }
        public string Jogador { get; set; }
        public int Pontuacao { get; set; }
    }
}
