using System.ComponentModel.DataAnnotations;

namespace FerramentaPedagogica.Models
{
    public class LoginViewModel
    {
        #region DataAnnotations
        [Display(Name = "Usuário")]
        #endregion
        public string Usuario { get; set; }

        #region DataAnnotations
        [Display(Name = "Senha")]
        #endregion
        public string Senha { get; set; }
    }
}
