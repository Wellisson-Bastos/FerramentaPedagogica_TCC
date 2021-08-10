using FerramentaPedagogica.Areas.SessaoJogo.Models.Pergunta;
using FerramentaPedagogica.Data;
using System.Linq;

namespace FerramentaPedagogica.Areas.SessaoJogo.Services.Pergunta
{
    public class PerguntaSessaoService
    {
        public PerguntaSessaoViewModel Buscar(int Pergunta, int Sessao)
        {
            using (var db = new studiesContext())
            {
                int lJogo = db.Sessao.FirstOrDefault(p => p.Codigo == Sessao).CodigoJogo;

                PerguntaSessaoViewModel[] lPerguntas = db.Pergunta
                    .Where(p => p.CodigoJogo == lJogo)
                    .OrderBy(p => p.Ordem)
                    .Select(p => new PerguntaSessaoViewModel
                    {
                        Tempo = p.Tempo,
                        Descricao = p.Texto,
                        Codigo = p.Codigo,
                        CodigoSessao = Sessao,
                        PerguntaAtual = Pergunta,
                        Imagem = p.Imagem,
                        URLYoutube = p.LinkVideo,
                        RespostaA = db.Resposta
                        .FirstOrDefault(r => 
                            r.CodigoPergunta == p.Codigo &&
                            r.Opcao == "A")
                        .Texto,
                        RespostaB = db.Resposta
                        .FirstOrDefault(r =>
                            r.CodigoPergunta == p.Codigo &&
                            r.Opcao == "B")
                        .Texto,
                        RespostaC = db.Resposta
                        .FirstOrDefault(r =>
                            r.CodigoPergunta == p.Codigo &&
                            r.Opcao == "C")
                        .Texto,
                        RespostaD = db.Resposta
                        .FirstOrDefault(r =>
                            r.CodigoPergunta == p.Codigo &&
                            r.Opcao == "D")
                        .Texto,
                    })
                    .ToArray();

                var lSessao = db.Sessao.FirstOrDefault(p => p.Codigo == Sessao);

                lSessao.PerguntaAtual = Pergunta;

                db.SaveChanges();

                if (lPerguntas.Length > Pergunta)
                {
                    return lPerguntas[Pergunta];
                }

                return null;
            }
        }

        public void AtualizarSessao(int piSessao) 
        {
            using (var db = new studiesContext())
            {
                var lSessao = db.Sessao.FirstOrDefault(p => p.Codigo == piSessao);

                lSessao.Finalizada = true;

                db.SaveChanges();
            }
        }
    }
}
