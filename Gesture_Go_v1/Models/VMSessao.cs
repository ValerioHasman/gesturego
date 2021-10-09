using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gesture_Go_v1.Models
{
    public class VMSessao
    {
        [Required]
        public DateTime ses_timer { get; set; }
        [Required]
        public int ses_qtdImages { get; set; }
        [Required]
        public DateTime ses_Totaltimer { get; set; }
        [Required]
        public Imagem[] ses_images { get; set; }
        public virtual Imagem Imagem { get; set; }


    }
}