using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Retenciones.Models
{
    public class MotivoFinal
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Motivo Final")]
        public string Nombre { get; set; }

        public int MotivoInicialId { get; set; }
    }
}