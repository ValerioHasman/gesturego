﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gesture_Go_v1.Models
{
    public class Posts
    {
        [Key]
        public int Pos_id { get; set; }

        public int UsuarioId { get; set; }

        [MaxLength(200)]
        [MinLength(10)]
        public string Pos_Titulo { get; set; }

        public string ImagemId { get; set; }

        public string Pos_imgUpload { get; set; }

        public bool Pos_Status { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Imagem Imagem { get; set; }

    }
}