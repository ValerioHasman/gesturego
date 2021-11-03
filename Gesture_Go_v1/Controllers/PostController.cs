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



        [HttpPost]
        public ActionResult CriarPost(string imgRefe, string Titulo, HttpPostedFileBase arquivo)
        {
            Posts posts = new Posts();
          



            string nomearq, valor;
            if (ModelState.IsValid)
            {
                if (arquivo != null)
                {
                    Funcoes.CriarDiretorio();
                   
                        nomearq = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(arquivo.FileName);
                        valor = Funcoes.UploadArquivo(arquivo, nomearq);
                        if (valor == "sucesso")
                        {
                            posts.Pos_Titulo = Titulo;
                            posts.ImagemId = Convert.ToInt32(imgRefe);
                            posts.UsuarioId = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
                            posts.data = DateTime.Now;
                            posts.Pos_imgUpload = nomearq;
                            posts.Pos_Status = true;

                            db.Posts.Add(posts);
                            db.SaveChanges();
                        
                    }
                }
            }
           
            return View();
        }

    }
}