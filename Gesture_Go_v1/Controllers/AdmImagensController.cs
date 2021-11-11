using System;
using System.Web.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gesture_Go_v1.Models;
using System.IO;

namespace Gesture_Go_v1.Controllers
{
    public class AdmImagensController : Controller
    {
        private Contexto db = new Contexto();

        [Authorize (Roles = "1")]
        public ActionResult AddImagens()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddImagens(Imagem img,HttpPostedFileBase arquivo)
        {
            string nomearq, valor;

            if (arquivo != null)
            {
                Funcoes.CriarDiretorio();

                nomearq = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(arquivo.FileName);
                valor = Funcoes.UploadArquivoImagensRef(arquivo, nomearq);
                if (valor == "sucesso")
                {       
                    
                    img.Img_nome = nomearq;

                    db.Imagem.Add(img);
                    db.SaveChanges();
                    return View();

                }
                else { return View("imgGrande"); }

            }
            else { return View("semImg"); }
        }
    }
}