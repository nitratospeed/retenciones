using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Retenciones.Models
{
    public class Base
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(200)]
        [Required]
        public string Nombre { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Importación")]
        [Required]
        public DateTime FechaImportacion { get; set; }

        [Display(Name = "Tipo")]
        [StringLength(50)]
        public string Tipo { get; set; }

        [Display(Name = "Estado")]
        [StringLength(50)]
        [Required]
        public string Estado { get; set; }
    }
}