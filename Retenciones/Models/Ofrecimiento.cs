using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Retenciones.Models
{
    public class Ofrecimiento
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Ofrecimiento")]
        public string Nombre { get; set; }

        public int PromocionId { get; set; }
    }
}