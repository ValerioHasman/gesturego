using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gesture_Go_v1.Models
{
    public class Comentarios
    {
        public int Id { get; set; }

        public int PostsPos_id { get; set; }

        public int UsuarioId { get; set; }

        public string Comentario { get; set; }

        public DateTime Data { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Posts Posts { get; set; }

        public ICollection<Comentarios> comentarios { get; set; }
    }
}