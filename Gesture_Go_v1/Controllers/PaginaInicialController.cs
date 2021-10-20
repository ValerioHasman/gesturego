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

        [Authorize]
        public ActionResult PaginaInicialIndex()
        {
            return View();
        }


        [Authorize]
        public ActionResult Teste(VMSessao ses)
        {
            ViewBag.sessao = ses.ses_tipo;
            return View(db.Imagem.Where(x => x.img_tipo == ses.ses_tipo).ToList());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaginaInicialIndex(VMSessao ses)
        {

            return RedirectToAction("Teste", ses);
        }


        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        
    }
}