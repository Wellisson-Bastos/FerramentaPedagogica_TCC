using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerramentaPedagogica.Models;
using FerramentaPedagogica.Models.Implementacoes;
using Microsoft.AspNetCore.Mvc;

namespace FerramentaPedagogica.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CadastrarUsuario(CadastroViewModel Model) 
        {
            if (ModelState.IsValid)
            {
                //var lServicoLogin = new ServicoLogin();

                //lServicoLogin.Adicionar(Model);

                return Json(Url.Action("Index", "Home"));
            }

            return RedirectToAction("Index", Model);
        }
    }
}
