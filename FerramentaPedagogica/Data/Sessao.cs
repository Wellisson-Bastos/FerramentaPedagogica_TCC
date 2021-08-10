using System;
using System.Collections.Generic;

namespace FerramentaPedagogica.Data
{
    public partial class Sessao
    {
        public Sessao()
        {
            Jogador = new HashSet<JogadorModel>();
        }

        public int Codigo { get; set; }
        public int CodigoJogo { get; set; }
        public int PerguntaAtual { get; set; }
        public bool? Finalizada { get; set; }

        public virtual Jogo CodigoJogoNavigation { get; set; }
        public virtual ICollection<JogadorModel> Jogador { get; set; }
    }
}
