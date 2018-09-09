using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Retenciones.Models
{
    public class AbonadosActivos
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Código Cliente")]
        [StringLength(200)]
        public string CodigoCliente { get; set; }

        [Display(Name = "Num SLC")]
        [StringLength(200)]
        public string NumSLC { get; set; }

        [Display(Name = "Nombre Cliente")]
        [StringLength(200)]
        public string NombreCliente { get; set; }

        [Display(Name = "Solución")]
        [StringLength(200)]
        public string Solucion { get; set; }

        [Display(Name = "Fecha Inicio")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaInicio { get; set; }

        [Display(Name = "Customer ID")]
        [StringLength(200)]
        public string CustomerID { get; set; }

        public int BaseId { get; set; }
    }
}