using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Retenciones.Models
{
    public class Edificio
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Dirección")]
        [StringLength(200)]
        public string Direccion { get; set; }

        [Display(Name = "Nodo")]
        [StringLength(200)]
        public string Nodo { get; set; }

        [Display(Name = "Edificio")]
        [StringLength(200)]
        public string EdificioCodigo { get; set; }

        [Display(Name = "Distrito")]
        [StringLength(200)]
        public string Distrito { get; set; }

        [Display(Name = "Fecha de Activación")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaActivacion { get; set; }

        public int BaseId { get; set; }
    }
}