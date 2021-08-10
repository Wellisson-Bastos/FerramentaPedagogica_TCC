namespace FerramentaPedagogica.Areas.SessaoJogo.Models.Pergunta
{
    public class PerguntaSessaoViewModel
    {
        public int CodigoSessao { get; set; }

        public int Codigo { get; set; }

        public int PerguntaAtual { get; set; }

        public string Descricao { get; set; }

        public int Tempo { get; set; }

        public byte[] Imagem { get; set; }

        public string URLYoutube { get; set; }

        public string RespostaA { get; set; }

        public string RespostaB { get; set; }

        public string RespostaC { get; set; }

        public string RespostaD { get; set; }
    }
}
