﻿using System;
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
            var lista = db.Imagem.ToList();
            return View("ExibirImagens", lista);
        }

        [Authorize(Roles = "1")]
        public ActionResult ExibirImagens()
        {
            var lista = db.Imagem.ToList();
            return View(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddImagens(Imagem img,HttpPostedFileBase arquivo)
        {
            string nomearq, valor;
            var lista = db.Imagem.ToList();

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
                    ViewBag.msg = "Imagem Inserida";
                    lista = db.Imagem.ToList();
                    return View("ExibirImagens", lista);

                }
                else if(valor == "Tamanho Máximo permitido é de  2000  kb!") {
                    ViewBag.msg = "Imagem muito grande";
                    return View("ExibirImagens", lista); 
                }
                else if (valor == "Extensão inválida, só são permitidas .png e .jpg!")
                {
                    ViewBag.msg = "Formato de arquivo Invalido !";
                    return View("ExibirImagens", lista);
                }
                else
                {
                    ViewBag.msg = "Erro !";
                    return View("ExibirImagens", lista);
                }
            }
            else {
                ViewBag.msg = "Insira a Imagem";
                return View("ExibirImagens", lista); 
            }
        }
    }
}