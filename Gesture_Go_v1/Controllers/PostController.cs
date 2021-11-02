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


        public ActionResult CriarPost(int? imgRef)
        {
            TempData["imgRefe"] = imgRef;
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarPost(Posts posts, HttpPostedFileBase[] arquivos)
        {


            string nomearq, valor;
            if (ModelState.IsValid)
            {
                if (arquivos != null)
                {
                    Funcoes.CriarDiretorio();
                    foreach (HttpPostedFileBase flb in arquivos)
                    {
                        nomearq = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(flb.FileName);
                        valor = Funcoes.UploadArquivo(flb, nomearq);
                        if (valor == "sucesso")
                        {
                            posts.ImagemId = 2;
                            posts.UsuarioId = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
                            posts.data = DateTime.Now;
                            posts.Pos_imgUpload = nomearq;
                            posts.Pos_Status = true;

                            db.Posts.Add(posts);
                            db.SaveChanges();
                        }
                    }
                }
            }
           
            return View();
        }

    }
}