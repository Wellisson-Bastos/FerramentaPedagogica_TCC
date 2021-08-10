﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FerramentaPedagogica.Areas.Jogador.Controllers
{
    [Area("Jogador")]
    [Route("Finalizacao")]
    public class FinalizacaoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
