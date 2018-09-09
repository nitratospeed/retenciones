using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Retenciones.Models
{
    public class PromoSGA
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Código Cliente")]
        [StringLength(200)]
        public string CodigoCliente { get; set; }

        [Display(Name = "Fecha Registro")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaRegistro { get; set; }

        [Display(Name = "UGI")]
        [StringLength(200)]
        public string Descripcion { get; set; }

        [Display(Name = "Promoción")]
        [StringLength(200)]
        public string Promocion { get; set; }

        [Display(Name = "Usuario")]
        [StringLength(200)]
        public string Usuario { get; set; }

        public int BaseId { get; set; }
    }
}