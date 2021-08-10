using FerramentaPedagogica.Areas.Administrador.Models;
using FerramentaPedagogica.Data;
using System.Collections.Generic;
using System.Linq;

namespace FerramentaPedagogica.Areas.Administrador.Services.Explorar
{
    public class ExplorarService
    {
        public List<DTOJogoExplorar> ObterListaJogos(int piUsuario, string psTermoPesquisa = "")
        {
            using (var db = new studiesContext())
            {
                return (from JOG in db.Jogo
                        where
                            JOG.PkUsuarioNavigation.Codigo != piUsuario &&
                            (
                               string.IsNullOrEmpty(psTermoPesquisa) ||
                               (
                                    JOG.Titulo.Contains(psTermoPesquisa) ||
                                    JOG.Descricao.Contains(psTermoPesquisa)
                               )
                            )
                        orderby JOG.HoraCriacao descending
                        select new DTOJogoExplorar()
                        {
                            Codigo = JOG.Codigo,
                            Nome = JOG.Titulo,
                            NomeUsuario = JOG.PkUsuarioNavigation.Usuario1,
                            FotoUsuario = JOG.PkUsuarioNavigation.Foto,
                            CapaJogo = JOG.Capa
                        }).ToList();
            }
        }
    }
}
