using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gesture_Go_v1.Models
{
    public class VMSessao
    {
       
        public int Id { get; set; }

        [Display(Name ="Timer")]
        [Required]
        public DateTime ses_timer { get; set; }
        [Display(Name = "Qtd. Images")]
        [Required]
        public int ses_qtdImages { get; set; }

        [Required]
        public DateTime ses_Totaltimer { get; set; }

        [Required]
        public string ses_tipo { get; set; }

        [Required]
        public Imagem[] ses_images { get; set; }

        public virtual Imagem Imagem { get; set; }

        public virtual ICollection<Imagem> Imagems { get; set; }

    }

}