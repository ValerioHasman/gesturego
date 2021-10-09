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
        public int img_local { get; set; }

        public enum tipo
        {
            [Display(Name = "Figura Humana")]
            Humano = 1, 
            Animais = 2, 
            Estruturas = 3
        }

        [Required]
        public tipo img_tipo { get; set; }

    }
}