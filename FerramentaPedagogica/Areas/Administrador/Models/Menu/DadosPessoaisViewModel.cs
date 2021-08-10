using System.ComponentModel.DataAnnotations;

namespace FerramentaPedagogica.Areas.Administrador.Models
{
    public class DadosPessoaisViewModel
    {
        public int UsuarioCodigo { get; set; }

        #region DataAnnotations
        [Display(Name = "Nova senha")]
        #endregion
        public string Senha { get; set; }

        #region DataAnnotations
        [Display(Name = "Confirmação senha")]
        [Compare("Senha", ErrorMessage = "A senha e a confirmação não conferem")]
        #endregion
        public string ConfirmacaoSenha { get; set; }

        #region DataAnnotations
        [Display(Name = "Foto de perfil")]
        #endregion
        public string FotoBase64 { get; set; }
    }
}
