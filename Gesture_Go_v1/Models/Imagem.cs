using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gesture_Go_v1.Models
{
    public class Imagem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Img_tipo { get; set; }

        [Required]
        public string Img_nome { get; set; }
  
    }
}