using System;
using System.Collections.Generic;

namespace FerramentaPedagogica.Areas.Administrador.Models
{
    public class FavoritoViewModel
    {
        public int Usuario { get; set; }
        public List<DTOJogoFavorito> Jogos { get; set; }
    }

    public class DTOJogoFavorito
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
    }
}
