using System;
using System.Collections.Generic;

namespace FerramentaPedagogica.Data
{
    public partial class Resposta
    {
        public int Codigo { get; set; }
        public string Texto { get; set; }
        public bool RespostaCorreta { get; set; }
        public int CodigoPergunta { get; set; }
        public string Opcao { get; set; }

        public virtual Pergunta CodigoPerguntaNavigation { get; set; }
    }
}
