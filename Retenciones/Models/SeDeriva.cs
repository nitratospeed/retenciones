using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Retenciones.Models
{
    public class SeDeriva
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Se Deriva")]
        public string Nombre { get; set; }
    }
}