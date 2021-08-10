using FerramentaPedagogica.Areas.Administrador.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Web;

public class MenuTopoViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var lMenuService = new MenuService();

        var teste = HttpContext.Request.Scheme;

        var lUsuario = Request.Query["Usuario"].ToString();

        var lModel = lMenuService.ObterDadosUsuario(Convert.ToInt32(lUsuario));

        return View(lModel);
    }
}