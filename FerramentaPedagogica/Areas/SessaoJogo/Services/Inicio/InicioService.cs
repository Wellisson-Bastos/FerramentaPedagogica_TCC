using FerramentaPedagogica.Areas.SessaoJogo.Models.Inicio;
using FerramentaPedagogica.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FerramentaPedagogica.Areas.SessaoJogo.Services.Inicio
{
    public class InicioService
    {
        public InicioViewModel CriarSessao(int piJogo)
        {
            int liNumero = 0;
            bool lbNumeroPossivel = false;
            Sessao lSessao = new Sessao();

            using (var db = new studiesContext())
            {
                do
                {
                    Random rnd = new Random();
                    liNumero = rnd.Next(111111, 999999);

                    lSessao = db.Sessao.FirstOrDefault(p => p.Codigo == liNumero);

                    if (lSessao == null)
                    {
                        lbNumeroPossivel = true;
                    }
                }
                while (!lbNumeroPossivel);

                lSessao = new Sessao()
                {
                    Codigo = liNumero,
                    CodigoJogo = piJogo,
                    PerguntaAtual = -1,
                    Finalizada = false
                };

                db.Sessao.Add(lSessao);
                db.SaveChanges();

                var lTitulo = db.Jogo.FirstOrDefault(p => p.Codigo == piJogo).Titulo;

                return new InicioViewModel
                {
                    CodigoJogo = piJogo,
                    CodigoSessao = lSessao.Codigo,
                    TituloJogo = lTitulo
                };
            }
        }

        public string[] BuscarJogadores(int Sessao)
        {
            using (var db = new studiesContext())
            {
                return (from JOG in db.Jogador
                        where
                            JOG.CodigoSessao == Sessao
                        select
                            JOG.Jogador
                       ).ToArray();
            }
        }
    }
}
