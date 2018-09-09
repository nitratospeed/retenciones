using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Retenciones.Models
{
    public class Ofrecimiento2
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Campo")]
        [StringLength(200)]
        public string Campo { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(200)]
        public string Nombre { get; set; }

        [Display(Name = "Accion a Tomar")]
        [StringLength(200)]
        public string AccionTomar { get; set; }

        [Display(Name = "Speech")]
        public string Speech { get; set; }

        [Display(Name = "Idea Fuerza")]
        [StringLength(200)]
        public string IdeaFuerza { get; set; }
    }
}