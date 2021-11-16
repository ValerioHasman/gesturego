using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Gesture_Go_v1.Models;

namespace Gesture_Go_v1.Controllers
{
    public class PaginaInicialController : Controller
    {
        private Contexto db = new Contexto();

        public ActionResult Index()
        {
            return View("PaginaInicialIndex");
        }

        [Authorize]
        public ActionResult PaginaInicialIndex()
        {
            return View();
        }

        [Authorize]
        public ActionResult Perfil()
        {
            return View();
        }

        [Authorize]
        public ActionResult Teste(VMSessao ses)
        {
            ViewBag.segundos = Funcoes.ConverteSegudos(ses.ses_timer);
            ViewBag.timer = ses.ses_timer;
            var lista = Funcoes.Randomize(db.Imagem.Where(x => x.Img_tipo == ses.ses_tipo).ToList());
            return View(lista);
        }

        [Authorize]
        public ActionResult Pratica(VMSessao ses)
        {
            if (TempData["Postado"] != null)
            {
                ViewBag.toast = true;
            }

            ViewBag.timer = ses.ses_timer;
            ViewBag.segundos = Funcoes.ConverteSegudos(ses.ses_timer);
            var lista = Funcoes.Randomize(db.Imagem.Where(x => x.Img_tipo == ses.ses_tipo).ToList());
            return View(lista);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaginaInicialIndex(VMSessao ses)
        {
            return RedirectToAction("Pratica", ses);
        }


        public ActionResult Sair()
        {
            Session["id"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        
    }
}