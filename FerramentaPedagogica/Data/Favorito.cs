using System;
using System.Collections.Generic;

namespace FerramentaPedagogica.Data
{
    public partial class Favorito
    {
        public int Codigo { get; set; }
        public int CodigoJogo { get; set; }
        public DateTime DataInsercao { get; set; }
        public int CodigoUsuario { get; set; }

        public virtual Jogo CodigoJogoNavigation { get; set; }
        public virtual Usuario CodigoUsuarioNavigation { get; set; }
    }
}
