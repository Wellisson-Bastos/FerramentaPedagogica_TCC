using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerramentaPedagogica.Areas.Administrador.Models
{
    public class ExplorarViewModel
    {
        public int CodigoUsuario { get; set; }
        public List<DTOJogoExplorar> Jogos { get; set; }
    }

    public class DTOJogoExplorar
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public byte[] FotoUsuario { get; set; }
        public byte[] CapaJogo { get; set; }
    }
}
