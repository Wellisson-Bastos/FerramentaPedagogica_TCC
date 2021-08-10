using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerramentaPedagogica.Areas.Administrador.Models
{
    public class HomeViewModel
    {
        public int Usuario { get; set; }
        public List<DTOJogoUsuario> Jogos { get; set; }
    }

    public class DTOJogoUsuario
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
