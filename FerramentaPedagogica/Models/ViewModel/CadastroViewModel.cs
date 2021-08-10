using System.ComponentModel.DataAnnotations;

namespace FerramentaPedagogica.Models
{
    public class CadastroViewModel
    {
        public int Codigo { get; set; }

        #region DataAnnotations
        [Display(Name = "Usuário")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome de usuário é obrigatório")]
        [StringLength(60, ErrorMessage = "O nome de usuário deve conter no máximo 60 caracteres.")]
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
