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
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,10})", ErrorMessage = "Senha de 6 a 10 caracteres. Obrigatório Número, letra maiúscula e minúscula")]
        public string Senha { get; set; }

        public string ImgPerfil { get; set; }

        public int PerfilId { get; set; }

        public virtual Perfil Perfil { get; set; }

    }
}