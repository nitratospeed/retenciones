using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Retenciones.Models
{
    public class GestionCarruseles
    {
        [Key]
        public int Id { get; set; }

        //BASE

        [Display(Name = "Fecha Generación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [StringLength(200)]
        public string FechaGeneracion { get; set; }

        [Display(Name = "SOT Baja")]
        [StringLength(200)]
        public string SOTBaja { get; set; }

        //GESTIÓN

        [Display(Name = "Código SGA (Antiguo)")]
        [StringLength(200)]
        public string CodigoSGAAntiguo { get; set; }

        [Display(Name = "Cliente (Antiguo)")]
        [StringLength(200)]
        public string ClienteAntiguo { get; set; }

        [Display(Name = "Dirección (Antiguo)")]
        [StringLength(500)]
        public string DireccionAntiguo { get; set; }

        [Display(Name = "Distrito (Antiguo)")]
        [StringLength(200)]
        public string DistritoAntiguo { get; set; }

        [Display(Name = "Departamento (Antiguo)")]
        [StringLength(200)]
        public string DepartamentoAntiguo { get; set; }

        [Display(Name = "Proyecto (Antiguo)")]
        [StringLength(200)]
        public string ProyectoAntiguo { get; set; }

        [Display(Name = "Tipo Aplicativo (Antiguo)")]
        [StringLength(200)]
        public string TipoAplicativoAntiguo { get; set; }

        [Display(Name = "Incidencia (Antiguo)")]
        [StringLength(200)]
        public string IncidenciaAntiguo { get; set; }

        [Display(Name = "Interaccion (Antiguo)")]
        [StringLength(200)]
        public string InteraccionAntiguo { get; set; }

        [Display(Name = "Fecha Instalación (Nuevo)")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaInstalacionNuevo { get; set; }

        [Display(Name = "Código SGA (Nuevo)")]
        [StringLength(200)]
        public string CodigoSGANuevo { get; set; }

        [Display(Name = "Cliente (Nuevo)")]
        [StringLength(200)]
        public string ClienteNuevo { get; set; }

        [Display(Name = "SOT Alta (Nuevo)")]
        [StringLength(200)]
        public string SOTAltaNuevo { get; set; }

        [Display(Name = "Dirección (Nuevo)")]
        [StringLength(500)]
        public string DireccionNuevo { get; set; }

        [Display(Name = "Distrito (Nuevo)")]
        [StringLength(200)]
        public string DistritoNuevo { get; set; }

        [Display(Name = "Departamento (Nuevo)")]
        [StringLength(200)]
        public string DepartamentoNuevo { get; set; }

        [Display(Name = "Proyecto (Nuevo)")]
        [StringLength(200)]
        public string ProyectoNuevo { get; set; }

        [Display(Name = "Parentesco (Nuevo)")]
        [StringLength(200)]
        public string ParentescoNuevo { get; set; }

        [Display(Name = "Carrusel")]
        public bool Carrusel { get; set; }

        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        public string Observacion { get; set; }

        [Display(Name = "Pendiente")]
        public bool Pendiente { get; set; }

        [Display(Name = "Asesor")]
        [StringLength(200)]
        public string Asesor { get; set; }

        [Display(Name = "IP")]
        [StringLength(200)]
        public string IP { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Gestión")]
        public DateTime? FechaGestion { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Modificación")]
        public DateTime? FechaModificacion { get; set; }

        public int BaseId { get; set; }

    }
}