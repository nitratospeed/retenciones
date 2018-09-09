using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Retenciones.Models
{
    public class NcSIAC
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Código SGA")]
        [StringLength(200)]
        public string CodigoSGA { get; set; }

        [Display(Name = "Fecha Emisión")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaEmision { get; set; }

        [Display(Name = "Tipo")]
        [StringLength(200)]
        public string TipoRegistro { get; set; }

        [Display(Name = "Monto N/C sin IGV")]
        [StringLength(200)]
        public string MontoImputado { get; set; }

        [Display(Name = "Documento")]
        [StringLength(200)]
        public string DocRef { get; set; }

        [Display(Name = "Usuario Generador")]
        [StringLength(200)]
        public string UsuarioGenerador { get; set; }

        public int BaseId { get; set; }
    }
}