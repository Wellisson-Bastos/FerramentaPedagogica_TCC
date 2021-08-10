using System;
using System.Collections.Generic;

namespace FerramentaPedagogica.Data
{
    public partial class Pergunta
    {
        public Pergunta()
        {
            Resposta = new HashSet<Resposta>();
        }

        public int Codigo { get; set; }
        public byte[] Imagem { get; set; }
        public string LinkVideo { get; set; }
        public string Texto { get; set; }
        public int Tempo { get; set; }
        public int? Ordem { get; set; }
        public int? CodigoJogo { get; set; }
        public int? Pontuacao { get; set; }
        public string RespostaCorreta { get; set; }

        public virtual Jogo CodigoJogoNavigation { get; set; }
        public virtual ICollection<Resposta> Resposta { get; set; }
    }
}
