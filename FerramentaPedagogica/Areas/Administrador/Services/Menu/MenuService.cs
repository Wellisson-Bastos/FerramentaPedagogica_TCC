using FerramentaPedagogica.Areas.Administrador.Models;
using FerramentaPedagogica.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerramentaPedagogica.Areas.Administrador.Services
{
    public class MenuService
    {
        public MenuTopoViewModel ObterDadosUsuario(int liUsuario)
        {
            using (var db = new studiesContext())
            {
                return (from USU in db.Usuario
                        where
                            USU.Codigo == liUsuario
                        select new MenuTopoViewModel
                        {
                            Codigo = USU.Codigo,
                            Usuario = USU.Usuario1,
                            Foto = USU.Foto
                        }).FirstOrDefault();
            }
        }

        public void AtualizarDadosUsuario(DadosPessoaisViewModel lModel) 
        {
            using (var db = new studiesContext())
            {
                var lUsuario = db.Usuario.FirstOrDefault(p => p.Codigo == lModel.UsuarioCodigo);

                if (!string.IsNullOrEmpty(lModel.Senha))
                {
                    lUsuario.Senha = CreateMD5(lModel.Senha);
                }

                lUsuario.Foto = TrataBase64String(lModel.FotoBase64);

                db.SaveChanges();
            }
        }

        public async Task<CriacaoViewModel> CriarJogo(CriacaoViewModel pJogo)
        {
            Jogo lJogo = new Jogo()
            {
                Titulo = pJogo.Titulo,
                Descricao = pJogo.Descricao,
                PkUsuario = pJogo.UsuarioCodigo,
                HoraCriacao = DateTime.Now,
            };

            if (!string.IsNullOrEmpty(pJogo.CapaBase64))
            {
                lJogo.Capa = TrataBase64String(pJogo.CapaBase64);
            }

            using (var db = new studiesContext())
            {
                db.Jogo.Add(lJogo);
                await db.SaveChangesAsync();

                pJogo.Codigo = lJogo.Codigo;
                return pJogo;
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

        private byte[] TrataBase64String(string psBase64)
        {
            var lRetorno = new byte[] { };

            if (!string.IsNullOrEmpty(psBase64))
            {
                // O base64 deverá ser obtido após a virgula: data:image/jpeg;base64,
                int liPosInicial = psBase64.IndexOf(",") + 1;

                if (liPosInicial > -1)
                {
                    psBase64 = psBase64.Substring(liPosInicial, psBase64.Length - liPosInicial);
                }
                lRetorno = Convert.FromBase64String(psBase64);
            }

            return lRetorno;
        }

        #endregion
    }
}
