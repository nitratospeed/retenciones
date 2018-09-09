using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Retenciones.Models
{
    public class MotivoInicial
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Motivo Inicial")]
        public string Nombre { get; set; }
    }
}