using Gesture_Go_v1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gesture_Go_v1.Controllers
{
    public class PostController : Controller
    {
        private Contexto db = new Contexto();

        [Authorize]
        public ActionResult Posts()
        {
            var posts = db.Posts.Where(x => x.Pos_Status).ToList();
            return View(posts);
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
                             return Json("ok");

                        } else { return Json("imgGrande"); }

                } else { return Json("semImg"); }

        }

    }
}