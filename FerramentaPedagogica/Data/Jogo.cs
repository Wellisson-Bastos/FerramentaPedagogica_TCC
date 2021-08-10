using System;
using System.Collections.Generic;

namespace FerramentaPedagogica.Data
{
    public partial class Jogo
    {
        public Jogo()
        {
            Favorito = new HashSet<Favorito>();
            Pergunta = new HashSet<Pergunta>();
            Sessao = new HashSet<Sessao>();
        }

        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public DateTime HoraCriacao { get; set; }
        public int PkUsuario { get; set; }
        public byte[] Capa { get; set; }
        public string Titulo { get; set; }

        public virtual Usuario PkUsuarioNavigation { get; set; }
        public virtual ICollection<Favorito> Favorito { get; set; }
        public virtual ICollection<Pergunta> Pergunta { get; set; }
        public virtual ICollection<Sessao> Sessao { get; set; }
    }
}
