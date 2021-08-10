using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerramentaPedagogica.Data;
using FerramentaPedagogica.Models;
using FerramentaPedagogica.Models.Implementacoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;

namespace FerramentaPedagogica.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogarUsuario(LoginViewModel Model)
        {
            var lServicoLogin = new LoginService();

            Task<Usuario> lUsuario = lServicoLogin.Buscar(Model);

            if (lUsuario.Result != null && ModelState.IsValid)
            {
                return Json(new { success = true, url = Url.Action("Index", "Home", new { Area = "Administrador", Usuario = lUsuario.Result.Codigo}) });
            }
            else if (string.IsNullOrEmpty(Model.Senha) || string.IsNullOrEmpty(Model.Usuario))
            {
                return Json(new { success = false, responseText = "Preencha usuário e senha para realizar o login." });
            }
            else
            {
                return Json(new { success = false, responseText = "Usuário e/ou senha incorretos." });
            }
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(CadastroViewModel Model)
        {
            var lServicoLogin = new LoginService();

            bool ExisteUsuario = lServicoLogin.PossuiUsuarioCriado(Model);

            if (!ExisteUsuario && ModelState.IsValid)
            {
                Task<CadastroViewModel> lUsuario = lServicoLogin.Adicionar(Model);

                return Json(new { success = true, url = Url.Action("Index", "Home", new { Area = "Administrador", Usuario = lUsuario.Result.Codigo}) });
            }
            else if (ExisteUsuario)
            {
                return Json(new { success = false, responseText = "O usuário escolhido já está em uso." });
            }

            return Json(new { success = false, responseText = "Houve um erro na solicitação, tente novamente." });
        }
    }
}
