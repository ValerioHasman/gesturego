using Gesture_Go_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Gesture_Go_v1.Controllers
{
   
    public class HomeController : Controller
    {
        private Contexto db = new Contexto();
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Sobre o Gesture Go.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Nossos contatos.";

            return View();
        }


        public ActionResult Acesso()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Acesso(Acesso ace)
        {
            if (ModelState.IsValid)
            {
                string senhacrip = Funcoes.HashTexto(ace.Senha, "SHA512");
                var usu = db.Usuario.Where(x => x.Email == ace.Email && x.Senha == senhacrip).ToList().FirstOrDefault();
                if (usu == null)
                {
                    ModelState.AddModelError("", "Usuário e/ou senha inválidos");
                    return View(ace);
                }
                FormsAuthentication.SetAuthCookie(usu.Id + "|" + usu.Nome, false);
                string permissoes = usu.PerfilId.ToString();
                
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, usu.Id + "|" + usu.Email, DateTime.Now, DateTime.Now.AddMinutes(30), false, permissoes);
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                Response.Cookies.Add(cookie);
                return RedirectToAction("About", "Home");
            }

            return View("Index", ace);

        }

        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(Cadastro cad)
        {
            if (ModelState.IsValid)
            {
                if (db.Usuario.Where(x => x.Email == cad.Email).ToList().Count > 0)
                {
                    ModelState.AddModelError("Email", "E-mail já cadastrado");
                    return View(cad);
                }
                Usuario usu = new Usuario();
                usu.Nome = cad.Nome;
                usu.Email = cad.Email;
                usu.Senha = Funcoes.HashTexto(cad.Senha, "SHA512");
                usu.ImgPerfil = cad.ImgPerfil;
                usu.Perfil = new Perfil();
                usu.PerfilId = 2;//Usuário Comum
                

                db.Usuario.Add(usu);
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
                
                return RedirectToAction("About");
            }

            return View("Index", cad);

        }

        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

    }
}