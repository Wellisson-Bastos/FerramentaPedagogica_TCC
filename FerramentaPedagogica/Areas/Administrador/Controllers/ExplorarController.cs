﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FerramentaPedagogica.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    [Route("Explorar")]
    public class ExplorarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
