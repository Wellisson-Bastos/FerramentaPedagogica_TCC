using System.ComponentModel.DataAnnotations;

namespace FerramentaPedagogica.Areas.Jogador.Models
{
    public class JogarViewModel
    {
        #region DataAnnotations
        [Display(Name = "Jogador")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Insira um nome de jogador")]
        #endregion
        public string Jogador { get; set; }

        #region DataAnnotations
        [Display(Name = "Código do jogo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o código do jogo")]
        #endregion
        public int? CodigoSessao { get; set; }
    }
}
