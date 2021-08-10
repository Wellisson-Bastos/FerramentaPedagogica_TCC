using FerramentaPedagogica.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerramentaPedagogica.Models.Implementacoes
{
    public class LoginService
    {
        public async Task<Usuario> Buscar(LoginViewModel lLogin)
        {
            using (var db = new studiesContext())
            {
                var lSenha = CreateMD5(lLogin.Senha);

                return await db.Usuario.FirstOrDefaultAsync(p => p.Usuario1 == lLogin.Usuario && p.Senha == lSenha);
            }
        }

        public bool PossuiUsuarioCriado(CadastroViewModel lLogin)
        {
            using (var db = new studiesContext())
            {
                return db.Usuario.FirstOrDefault(p => p.Usuario1 == lLogin.Usuario) != null ? true : false;
            }
        }

        public async Task<CadastroViewModel> Adicionar(CadastroViewModel pUsuario)
        {
            Usuario lUsuario = new Usuario()
            {
                Usuario1 = pUsuario.Usuario,
                Senha =  CreateMD5(pUsuario.Senha)
            };

            using (var db = new studiesContext())
            {
                db.Usuario.Add(lUsuario);
                await db.SaveChangesAsync();

                pUsuario.Codigo = lUsuario.Codigo;

                return pUsuario;
            }
        }

        #region Métodos auxiliares

        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        #endregion
    }
}
