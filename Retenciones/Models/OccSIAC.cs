using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Retenciones.Models
{
    public class OccSIAC
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Código SGA")]
        [StringLength(200)]
        public string CodigoSGA { get; set; }

        [Display(Name = "Fecha Registro OCC")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaRegistro { get; set; }

        [Display(Name = "Concepto OCC")]
        [StringLength(200)]
        public string Concepto { get; set; }

        [Display(Name = "Monto OCC Sin IGV")]
        [StringLength(200)]
        public string MontoAjusteSinIGV { get; set; }

        [Display(Name = "Interacción")]
        [StringLength(200)]
        public string Interaccion { get; set; }

        [Display(Name = "Usuario Registro")]
        [StringLength(200)]
        public string UsuarioRegistro { get; set; }

        public int BaseId { get; set; }
    }
}