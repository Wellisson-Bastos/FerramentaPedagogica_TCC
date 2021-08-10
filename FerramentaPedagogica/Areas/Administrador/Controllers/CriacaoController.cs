using FerramentaPedagogica.Areas.Administrador.Models;
using FerramentaPedagogica.Areas.Administrador.Services.Criacao;
using Microsoft.AspNetCore.Mvc;

namespace FerramentaPedagogica.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    [Route("Criacao")]
    public class CriacaoController : Controller
    {
        [Route("Index")]
        public IActionResult Index(int Usuario, int Jogo)
        {
            var lService = new CriacaoService();
            var lModel = new PerguntaViewModel();

            lModel.CodigoJogo = Jogo;
            lModel.CodigoUsuario = Usuario;
            lModel.Perguntas = lService.ObterPerguntas(Jogo);
            lModel.UsuarioCriador = lService.UsuarioCriador(Usuario, Jogo);

            if (!lModel.UsuarioCriador)
            {
                lModel.JogoFavoritado = lService.JogoFavoritado(Usuario, Jogo);
            }

            return View(lModel);
        }

        [HttpGet]
        public PartialViewResult _EditarPergunta(int Usuario, int Jogo, int? Pergunta)
        {
            var lService = new CriacaoService();
            var lModel = new DTOPergunta();

            if (Pergunta.HasValue)
            {
                lModel = lService.ObterDadosPergunta(Jogo, Pergunta.Value, Usuario);
                lModel.Novo = false;
            }
            else
            {
                lModel.CodigoUsuario = Usuario;
                lModel.CodigoJogo = Jogo;
                lModel.UsuarioCriador = lService.UsuarioCriador(Usuario, Jogo);
                lModel.Novo = true;
            }

            return PartialView("_Pergunta", lModel);
        }

        [HttpPost]
        public JsonResult EditarPergunta(DTOPergunta lModel)
        {
            var lService = new CriacaoService();
            var lMensagem = lService.ValidarPergunta(lModel);

            if (lMensagem == "")
            {
                if (lModel.Novo)
                {
                    var lRetorno = lService.AdicionarPergunta(lModel);

                    return Json(new { success = true, nova = true, responseText = "Pergunta adicionada com sucesso.", perguntaCodigo = lRetorno.Codigo, descricao = lRetorno.Descricao });
                }
                else
                {
                    lService.EditarPergunta(lModel);

                    return Json(new { success = true, nova = false, responseText = "Pergunta atualizada com sucesso.", descricao = lModel.Descricao });

                }
            }
            else
            {
                return Json(new { success = false, responseText = lMensagem });
            }
        }

        [HttpPost]
        [Route("/Excluir/{Pergunta}")]
        public IActionResult ExcluirPergunta(int Pergunta)
        {
            var lService = new CriacaoService();

            var lbExcluidoComSucesso = lService.ExcluirPergunta(Pergunta);

            if (lbExcluidoComSucesso.Result)
            {
                return Json(new { success = true, responseText = "Exclusão realizada com sucesso!" });
            }
            else
            {
                return Json(new { success = false, responseText = "Houve um erro na solicitação, tente novamente." });
            }
        }

        [HttpPost]
        [Route("/Favoritar/{Usuario}/{Jogo}/{Favoritar}")]
        public JsonResult FavoritarJogo(bool Favoritar, int Usuario, int Jogo)
        {
            var lService = new CriacaoService();

            lService.FavoritarJogo(Favoritar, Usuario, Jogo);

            if (Favoritar)
            {
                return Json(new { success = false, responseText = "Adicionado aos favoritos com sucesso!" });
            }
            else
            {
                return Json(new { success = false, responseText = "Removido dos favoritos com sucesso!" });
            }
        }
    }
}
