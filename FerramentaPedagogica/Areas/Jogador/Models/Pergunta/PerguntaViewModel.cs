namespace FerramentaPedagogica.Areas.Jogador.Models.Pergunta
{
    public class PerguntaViewModel
    {
        public int Codigo { get; set; }
        public int Tempo { get; set; }
        public string Descricao { get; set; }
        public int NrRespostas { get; set; }
        public int CodigoJogador { get; set; }
        public int CodigoSessao { get; set; }
        public int PerguntaAtual { get; set; }
    }
}
