using Gesture_Go_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gesture_Go_v1.Controllers
{
    public class PostController : Controller
    {
        private Contexto db = new Contexto();

        public ActionResult CriarPost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarPost(Posts posts)
        {
            posts.UsuarioId = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
            posts.Pos_Status = true;

            if (ModelState.IsValid)
            {
                db.Posts.Add(posts);
                db.SaveChanges();
            }

                return View();
        }
    }
}