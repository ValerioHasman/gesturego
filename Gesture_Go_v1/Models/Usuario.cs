using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gesture_Go_v1.Models
{
    public class Usuario
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(3)]
        public string Nome { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public string ImgPerfil { get; set; }

        public int PerfilId { get; set; }

        public virtual Perfil Perfil { get; set; }

    }
}