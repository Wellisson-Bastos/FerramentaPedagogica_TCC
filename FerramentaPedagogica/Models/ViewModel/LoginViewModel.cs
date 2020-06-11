using System.ComponentModel.DataAnnotations;

namespace FerramentaPedagogica.Models
{
    public class LoginViewModel
    {
        #region DataAnnotations
        [Display(Name = "Usuário")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome de usuário é obrigatório")]
        #endregion
        public string Usuario { get; set; }

        #region DataAnnotations
        [Display(Name = "Senha")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Senha é obrigatória")]
        #endregion
        public string Senha { get; set; }
    }
}
