using FerramentaPedagogica.Areas.Administrador.Models;
using FerramentaPedagogica.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerramentaPedagogica.Areas.Administrador.Services.Home
{
    public class HomeService
    {
        public List<DTOJogoUsuario> ObterListaJogos(int piUsuario)
        {
            using (var db = new studiesContext())
            {
                return (from JOG in db.Jogo
                        where
                            JOG.PkUsuario == piUsuario
                        orderby
                            JOG.HoraCriacao descending
                        select new DTOJogoUsuario
                        {
                            Codigo = JOG.Codigo,
                            Nome = JOG.Titulo,
                            DataCriacao = JOG.HoraCriacao
                        }).ToList();
            }
        }

        public async Task<bool> ExcluirJogo(int piCodigo) 
        {
            try
            {
                using (var db = new studiesContext())
                {
                    var lJogo = db.Jogo.FirstOrDefault(p => p.Codigo == piCodigo);

                    db.Jogo.Remove(lJogo);

                    await db.SaveChangesAsync();

                    return true;
                }
            }
            catch 
            {
                return false;
            }
        }

        public bool PossuiPerguntas(int piJogo) 
        {
            using (var db = new studiesContext())
            {
                return db.Pergunta.Where(p => p.CodigoJogo == piJogo).ToArray().Count() > 0 ? true : false;
                    }
        }
    }
}
