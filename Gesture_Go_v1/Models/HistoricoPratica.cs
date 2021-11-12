using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gesture_Go_v1.Models
{
    public class HistoricoPratica
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public DateTime data { get; set; }

        public DateTime tempoPratica { get; set; }

        public int qtdImagens { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}