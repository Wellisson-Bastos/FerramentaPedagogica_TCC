using FerramentaPedagogica.Areas.Administrador.Models;
using FerramentaPedagogica.Data;
using System.Collections.Generic;
using System.Linq;

namespace FerramentaPedagogica.Areas.Administrador.Services
{
    public class FavoritoService
    {
        public List<DTOJogoFavorito> ObterListaJogos(int piUsuario)
        {
            using (var db = new studiesContext())
            {
                return (from FOV in db.Favorito
                        join JOG in db.Jogo
                        on FOV.CodigoJogo equals JOG.Codigo
                        where
                            FOV.CodigoUsuario == piUsuario
                        orderby
                            JOG.HoraCriacao descending
                        select new DTOJogoFavorito
                        {
                            Codigo = JOG.Codigo,
                            Nome = JOG.Titulo,
                            NomeUsuario = JOG.PkUsuarioNavigation.Usuario1
                        }).ToList();
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
