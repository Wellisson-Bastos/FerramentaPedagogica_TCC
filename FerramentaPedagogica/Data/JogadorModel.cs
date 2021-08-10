using System;
using System.Collections.Generic;

namespace FerramentaPedagogica.Data
{
    public partial class JogadorModel
    {
        public int Codigo { get; set; }
        public string Jogador { get; set; }
        public int CodigoSessao { get; set; }
        public int Pontuacao { get; set; }

        public virtual Sessao CodigoSessaoNavigation { get; set; }
    }
}
