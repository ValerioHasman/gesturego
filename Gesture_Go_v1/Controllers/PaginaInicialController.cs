﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
        public ActionResult Progresso()
        {
            return View();
        }

        [Authorize]
        public ActionResult ProgressoPgInicial()
        {
            ViewBag.aa = "Funfanfo";
            return View();
        }

        [Authorize]
        public ActionResult PaginaInicialIndex()
        {
            int id = Convert.ToInt32(User.Identity.Name.Split('|')[0]);


            var diaAtual = DateTime.Now;
            var hojeSemana = DateTime.Now.AddDays(((double)DateTime.Now.DayOfWeek) * -1);
            var listaTeste = db.HistoricoPratica.Where(x => x.data.Day == diaAtual.Day && x.data.Month == diaAtual.Month && x.data.Year == diaAtual.Year && x.UsuarioId == id).ToList().Select(g => new { g.tempoPratica, g.qtdImagens }).ToList();
            var ListaSemana = db.HistoricoPratica.Where(x => x.data.Day >= hojeSemana.Day && x.data.Month >= hojeSemana.Month && x.data.Year >= hojeSemana.Year && x.UsuarioId == id).Select(g => new { g.tempoPratica, g.qtdImagens }).ToList();
            int a = 0;
            int b = 0;

            foreach (var item in listaTeste)
            {
                a += Funcoes.ConverteSegudos(item.tempoPratica) * item.qtdImagens;
            }

            foreach (var item in ListaSemana)
            {
                b += Funcoes.ConverteSegudos(item.tempoPratica) * item.qtdImagens;
            }

            TimeSpan time = TimeSpan.FromSeconds(a);
            TimeSpan timeS = TimeSpan.FromSeconds(b);

            ViewBag.a = Convert.ToInt32(((double)a / 3600) * 100);
            ViewBag.b = Convert.ToInt32(((double)b / (3600 * 7)) * 100); ;

            if (a >= 3600) { ViewBag.horaCompleto = "Completo"; }
            if (b >= (3600 * 7)) { ViewBag.semanaCompleto = "Completo"; }

            ViewBag.hora = time.ToString(@"h\:mm\:ss");
            ViewBag.semana = timeS.ToString(@"h\:mm\:ss");
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

        [Authorize]
        public ActionResult EditarPerfil()
        {
            int id = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
            Usuario usu = new Usuario();
            usu = db.Usuario.Find(id);
            return View(usu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPerfil(Usuario usu, HttpPostedFileBase arquivo)
        {
            if (ModelState.IsValid)
            {
                if (arquivo != null)
                {
                    string nomearq, valor;

                    nomearq = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(arquivo.FileName);
                    valor = Funcoes.UploadArquivoImagensPerfil(arquivo, nomearq);
                    if (valor == "sucesso")
                    {
                        Funcoes.ExcluirArquivo(db.Usuario.Find(usu.Id).ImgPerfil);
                        usu.ImgPerfil = nomearq;
                        //db.Usuario.Where(x => x.Id == usu.Id).add
                    }
                }
                db.Entry(usu).State = EntityState.Modified;
                db.SaveChanges();
                return View(usu);
            }
            TempData["Msg"]="Erro: Campo inválido";
            return View(usu);
        }
    }
}