using System.ComponentModel.DataAnnotations;

namespace FerramentaPedagogica.Models
{
    public class CadastroViewModel
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

        #region DataAnnotations
        [Display(Name = "Confirmação senha")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirmação de senha é obrigatória")]
        [Compare("Senha", ErrorMessage = "A senha e a confirmação não conferem")]
        #endregion
        public string ConfirmacaoSenha { get; set; }
    }
}
