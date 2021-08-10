using FerramentaPedagogica.Areas.Jogador.Models.Pergunta;
using FerramentaPedagogica.Data;
using System.Linq;

namespace FerramentaPedagogica.Areas.Jogador.Services
{
    public class PerguntaService
    {
        public PerguntaViewModel Buscar(int piJogador, int piSessao)
        {
            using (var db = new studiesContext())
            {
                var lSessao = db.Sessao.Where(p => p.Codigo == piSessao).Select(p => new { p.PerguntaAtual, p.CodigoJogo }).FirstOrDefault();

                PerguntaViewModel[] lPerguntas = db.Pergunta
                    .Where(p => p.CodigoJogo == lSessao.CodigoJogo)
                    .OrderBy(p => p.Ordem)
                    .Select(p => new PerguntaViewModel
                    {
                        Tempo = p.Tempo,
                        Descricao = p.Texto,
                        Codigo = p.Codigo,
                        CodigoJogador = piJogador,
                        CodigoSessao = piSessao,
                        PerguntaAtual = lSessao.PerguntaAtual
                    })
                    .ToArray();

                if (lPerguntas.Length > lSessao.PerguntaAtual)
                {
                    PerguntaViewModel PerguntaAtual = lPerguntas[lSessao.PerguntaAtual];

                    PerguntaAtual.NrRespostas = db.Resposta.Where(p => p.CodigoPergunta == PerguntaAtual.Codigo).Count();

                    return PerguntaAtual;
                }

                return null;
            }
        }

        public void AtualizarPontuacao(int Jogador, int Sessao, int PerguntaAtual, string Resposta)
        {
            using (var db = new studiesContext())
            {
                var lJogador = db.Jogador.FirstOrDefault(p => p.Codigo == Jogador && p.CodigoSessao == Sessao);

                var lSessao = db.Sessao.FirstOrDefault(p => p.Codigo == Sessao);

                var lPerguntas = db.Pergunta
                    .Where(p => p.CodigoJogo == lSessao.CodigoJogo)
                    .OrderBy(p => p.Ordem)
                    .Select(p => new { p.Pontuacao, p.Codigo }).ToArray();

                var lPerguntaAtual = lPerguntas[PerguntaAtual];

                if (!string.IsNullOrEmpty(Resposta) && !Resposta.Equals("undefined"))
                {
                    bool RespostaCorreta = db.Resposta.FirstOrDefault(p => p.CodigoPergunta == lPerguntaAtual.Codigo && p.Opcao == Resposta).RespostaCorreta;

                    if (RespostaCorreta)
                    {
                        lJogador.Pontuacao += lPerguntaAtual.Pontuacao.Value;

                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
