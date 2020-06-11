using FerramentaPedagogica.Models;
using Microsoft.AspNetCore.Mvc;

namespace FerramentaPedagogica.Controllers
{
    [Area("Administrador")]
    [Route("Home")]
    public class HomeController : Controller
    {
        [HttpGet("/Home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
