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
        public int img_id { get; set; }

        [Required]
        public string img_tipo { get; set; }

        [Required]
        public string img_nome { get; set; }
    }
}