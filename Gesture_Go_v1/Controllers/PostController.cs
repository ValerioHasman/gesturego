using Gesture_Go_v1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Gesture_Go_v1.Controllers
{
    public class PostController : Controller
    {
        private Contexto db = new Contexto();

        [Authorize]
        public ActionResult Posts()
        {
            var posts = db.Posts.Where(x => x.Pos_Status).OrderByDescending(x => x.data).ToList();
            return View(posts);
        }


        [Authorize]
        public ActionResult MeusPosts()
        {
            int id = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
            var posts = db.Posts.Where(x => x.UsuarioId == id).ToList();
            return View(posts);
        }

        [Authorize]
        public ActionResult Comentarios(int id)
        {

            var comentarios = db.Comentarios.Where(x => x.PostsPos_id == id).ToList();
            return View(comentarios);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarCometario(Comentarios com)
        {
           com.UsuarioId = Convert.ToInt32(User.Identity.Name.Split('|')[0]);

            return View();
        }

        [Authorize]
        public ActionResult CriarPost(int? imgRef)
        {
            TempData["imgRefe"] = imgRef;
            return PartialView();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult CriarPost(string id, string titulo, HttpPostedFileBase imagem)
        {
            if(titulo.Length <= 10)
            {
                return Json("tpequeno");
            }

            Posts posts = new Posts();

            string nomearq, valor;
            
                if (imagem != null)
                {
                    Funcoes.CriarDiretorio();
                   
                    nomearq = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(imagem.FileName);
                    valor = Funcoes.UploadArquivo(imagem, nomearq);
                    if (valor == "sucesso")
                    {
                        posts.Pos_Titulo = titulo;
                        posts.ImagemId = Convert.ToInt32(id);
                        posts.UsuarioId = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
                        posts.data = DateTime.Now;
                        posts.Pos_imgUpload = nomearq;
                        posts.Pos_Status = true;

                        db.Posts.Add(posts);
                        db.SaveChanges();
                        ViewBag.toast = "post criado com sucesso";
                        return Json("ok");

                    } else { return Json("imgGrande"); }

                } else { return Json("semImg"); }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult HistoticoPratica(int qtdPratica, DateTime timer)
        {

            HistoricoPratica hist = new HistoricoPratica();
            hist.UsuarioId = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
            hist.data = DateTime.Now;
            hist.qtdImagens = qtdPratica;
            hist.tempoPratica = timer;
            db.HistoricoPratica.Add(hist);
            db.SaveChanges();

            return Json("");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult Comentar(string comentario, int post)
        {

            Comentarios com = new Comentarios();
            com.PostsPos_id = post;
            com.UsuarioId = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
            com.Data = DateTime.Now;
            com.Comentario = comentario;
            db.Comentarios.Add(com);
            db.SaveChanges();

            return Json("ok");
        }

        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}