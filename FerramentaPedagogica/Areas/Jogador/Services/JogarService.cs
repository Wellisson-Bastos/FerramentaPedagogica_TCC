using FerramentaPedagogica.Areas.Jogador.Models;
using FerramentaPedagogica.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FerramentaPedagogica.Areas.Jogador.Services
{
    public class JogarService
    {
        public async Task<bool> Buscar(int Sessao)
        {
            using (var db = new studiesContext())
            {
                return await db.Sessao.FirstOrDefaultAsync(p => p.Codigo == Sessao) != null ? true : false;
            }
        }

        public async Task<JogadorModel> InserirJogador(JogarViewModel pModel) 
        {
            JogadorModel lJogador = new JogadorModel()
            {
                Jogador = pModel.Jogador.Trim(),
                CodigoSessao = pModel.CodigoSessao.Value,
                Pontuacao = 0
            };

            using (var db = new studiesContext())
            {
                db.Jogador.Add(lJogador);
                await db.SaveChangesAsync();

                lJogador.Codigo = lJogador.Codigo;
                return lJogador;
            }
        }
    }
}
