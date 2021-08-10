using FerramentaPedagogica.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FerramentaPedagogica.Areas.Jogador.Services
{
    public class CarregamentoService
    {
        public async Task<Sessao> BuscarSessao(int piSessao)
        {
            using (var db = new studiesContext())
            {
                return await db.Sessao.FirstOrDefaultAsync(p => p.Codigo == piSessao);
            }
        }
    }
}
