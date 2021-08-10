using System.ComponentModel.DataAnnotations;

namespace FerramentaPedagogica.Areas.Administrador.Models
{
    public class CriacaoViewModel
    {
        public int UsuarioCodigo { get; set; }

        public int Codigo { get; set; }

        #region DataAnnotations
        [Display(Name = "Título*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Título é obrigatório.")]
        [StringLength(70, ErrorMessage = "O nome de usuário deve conter no máximo 100 caracteres.")]
        #endregion
        public string Titulo { get; set; }

        #region DataAnnotations
        [Display(Name = "Descrição*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Descrição é obrigatória")]
        #endregion
        public string Descricao { get; set; }

        public string CapaBase64 { get; set; }
    }

    public class PerguntaViewModel 
    {
        public int CodigoJogo { get; set; }

        public int CodigoUsuario { get; set; }

        public DTOPerguntaLista[] Perguntas { get; set; }

        public bool UsuarioCriador { get; set; }

        public bool JogoFavoritado { get; set; }
    }

    public class DTOPerguntaLista 
    {
        public int Codigo { get; set; }

        public string Texto { get; set; }
    }

    public class DTOPergunta
    {
        public int CodigoUsuario { get; set; }

        public int CodigoJogo { get; set; }

        public int? Codigo { get; set; }

        public int Ordem { get; set; } = 1;

        [StringLength(100, ErrorMessage = "A pergunta deve conter no máximo 100 caracteres.")]
        public string Descricao { get; set; }

        public int Pontuacao { get; set; }

        public int Tempo { get; set; }

        public byte[] ImagemByte { get; set; }

        public string Imagem { get; set; }

        [StringLength(100, ErrorMessage = "Url do Youtube deve conter no máximo 100 caracteres.")]
        public string URLYoutube { get; set; }

        [StringLength(100, ErrorMessage = "A resposta A deve conter no máximo 100 caracteres.")]
        public string RespostaA { get; set; }

        public bool RespostaACorreta { get; set; }

        [StringLength(100, ErrorMessage = "A resposta B deve conter no máximo 100 caracteres.")]
        public string RespostaB { get; set; }

        public bool RespostaBCorreta { get; set; }

        [StringLength(100, ErrorMessage = "A resposta C deve conter no máximo 100 caracteres.")]
        public string RespostaC { get; set; }

        public bool RespostaCCorreta { get; set; }

        [StringLength(100, ErrorMessage = "A resposta D deve conter no máximo 100 caracteres.")]
        public string RespostaD { get; set; }

        public bool RespostaDCorreta { get; set; }

        public bool Novo { get; set; }

        public bool UsuarioCriador { get; set; }
    }
}
