using System;
using FerramentaPedagogica.Areas.Administrador.Models;
using FerramentaPedagogica.Areas.Administrador.Services;
using Microsoft.AspNetCore.Mvc;

namespace FerramentaPedagogica.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult _DadosUsuario(int Usuario) 
        {
            var lMenuService = new MenuService();
            var lModel = new DadosPessoaisViewModel();

            var lSemImagem = Convert.ToBase64String(FerramentaPedagogica.Properties.Resources.sem_imagem);

            var lUsuario = lMenuService.ObterDadosUsuario(Usuario);

            lModel.UsuarioCodigo = Usuario;
            lModel.FotoBase64 = lUsuario.Foto == null ? lSemImagem : Convert.ToBase64String(lUsuario.Foto);

            return PartialView("_DadosUsuario", lModel);
        }

        [HttpPost]
        public JsonResult AtualizarDados(DadosPessoaisViewModel lModel) 
        {
            var lMenuService = new MenuService();

            if (ModelState.IsValid)
            {
                lMenuService.AtualizarDadosUsuario(lModel);
                return Json(new { success = true, responseText = "Dados atualizados com sucesso!" });
            }
                
            return Json(new { success = false, responseText = "Houve um erro na solicitação, tente novamente." });
        }

        [HttpGet]
        public PartialViewResult _CriarJogo(int Usuario)
        {
            var lModel = new CriacaoViewModel();

            lModel.UsuarioCodigo = Usuario;

            return PartialView("_CriarJogo", lModel);
        }

        [HttpPost]
        public JsonResult CriarJogo(CriacaoViewModel lModel)
        {
            var lMenuService = new MenuService();

            if (ModelState.IsValid)
            {
                var lJogo = lMenuService.CriarJogo(lModel);

                return Json(new { success = true, url = Url.Action("Index", "Criacao", new { Usuario = lJogo.Result.UsuarioCodigo, Jogo = lJogo.Result.Codigo }) });
            }
            else
            {
                return Json(new { success = false, responseText = "Preencha todos os campos para prosseguir." });
            }
        }
    }
}
