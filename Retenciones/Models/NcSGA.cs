using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Retenciones.Models
{
    public class NcSGA
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Código Cliente")]
        [StringLength(200)]
        public string CodigoCliente { get; set; }

        [Display(Name = "Fecha Emisión")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaEmision { get; set; }

        [Display(Name = "Incidencia")]
        [StringLength(200)]
        public string NroIncidencia { get; set; }

        [Display(Name = "Monto N/C sin IGV")]
        [StringLength(200)]
        public string SubTotal { get; set; }

        [Display(Name = "Documento")]
        [StringLength(200)]
        public string Documento { get; set; }

        [Display(Name = "Área a Imputar")]
        [StringLength(200)]
        public string AreaResponsable { get; set; }

        [Display(Name = "Usuario Emisor")]
        [StringLength(200)]
        public string UsuarioEmisor { get; set; }

        public int BaseId { get; set; }
    }
}