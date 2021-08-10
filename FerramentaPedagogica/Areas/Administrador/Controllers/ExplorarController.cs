using FerramentaPedagogica.Areas.Administrador.Models;
using FerramentaPedagogica.Areas.Administrador.Services.Explorar;
using Microsoft.AspNetCore.Mvc;

namespace FerramentaPedagogica.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    [Route("Explorar")]
    public class ExplorarController : Controller
    {
        [Route("Index")]
        public IActionResult Index(int Usuario)
        {
            var lModel = new ExplorarViewModel();

            var lExplorarService = new ExplorarService();

            lModel.CodigoUsuario = Usuario;
            lModel.Jogos = lExplorarService.ObterListaJogos(Usuario);

            ViewData["Usuario"] = Usuario;

            return View(lModel);
        }

        public PartialViewResult _ListaJogos(int Usuario, string TermoPesquisa) 
        {
            var lExplorarService = new ExplorarService();

            var lJogos = lExplorarService.ObterListaJogos(Usuario, TermoPesquisa);

            return PartialView(lJogos);
        }
    }
}
