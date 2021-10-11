using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Gesture_Go_v1.Controllers
{
    public class PaginaInicialController : Controller
    {
        [Authorize]
        public ActionResult PaginaInicialIndex()
        {
            return View();
        }

        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        
    }
}