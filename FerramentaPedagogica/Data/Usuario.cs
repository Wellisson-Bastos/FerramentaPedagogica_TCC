using System;
using System.Collections.Generic;

namespace FerramentaPedagogica.Data
{
    public partial class Usuario
    {
        public Usuario()
        {
            Favorito = new HashSet<Favorito>();
            Jogo = new HashSet<Jogo>();
        }

        public int Codigo { get; set; }
        public string Usuario1 { get; set; }
        public byte[] Foto { get; set; }
        public string Senha { get; set; }

        public virtual ICollection<Favorito> Favorito { get; set; }
        public virtual ICollection<Jogo> Jogo { get; set; }
    }
}
