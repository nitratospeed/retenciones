using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Retenciones.Models
{
    public class Gestion
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tipo Cliente")]
        public bool TipoCliente { get; set; }

        [Display(Name = "Código SGA")]
        [StringLength(200)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Use solo caracteres numéricos.")]
        public string CodigoSGA { get; set; }

        [Display(Name = "Customer ID")]
        [StringLength(200)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Use solo caracteres numéricos.")]
        public string CustomerID { get; set; }

        [Display(Name = "Nombre Cliente")]
        [StringLength(200)]
        public string NombreCliente { get; set; }

        [Display(Name = "DNI")]
        [StringLength(200)]
        public string DNI { get; set; }

        [Display(Name = "Teléfono Contacto 1")]
        [StringLength(9)]
        [MaxLength(9, ErrorMessage = "Use máximo 9 dígitos.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Use solo caracteres numéricos.")]
        public string TelefonoContacto1 { get; set; }

        [Display(Name = "Teléfono Contacto 2")]
        [StringLength(9)]
        [MaxLength(9, ErrorMessage = "Use máximo 9 dígitos.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Use solo caracteres numéricos.")]
        public string TelefonoContacto2 { get; set; }

        [Display(Name = "Teléfono Contacto 3")]
        [StringLength(9)]
        [MaxLength(9, ErrorMessage = "Use máximo 9 dígitos.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Use solo caracteres numéricos.")]
        public string TelefonoContacto3 { get; set; }

        [Display(Name = "Tiene Email?")]
        [StringLength(200)]
        public string TieneEmail { get; set; }

        [Display(Name = "Email")]
        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Contrato")]
        [StringLength(200)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Use solo caracteres numéricos.")]
        public string Contrato { get; set; }

        [Required]
        [Display(Name = "Resultado Retención")]
        [StringLength(200)]
        public string ResultadoRetencion { get; set; }

        [Display(Name = "Tipo de Baja")]
        [StringLength(200)]
        public string TipoBaja { get; set; }

        [Display(Name = "Motivo Inicial")]
        public int? MotivoInicialId { get; set; }

        [ForeignKey("MotivoInicialId")]
        public virtual MotivoInicial MotivoInicial { get; set; }

        [Display(Name = "Motivo Final")]
        public int? MotivoFinalId { get; set; }

        [ForeignKey("MotivoFinalId")]
        public virtual MotivoFinal MotivoFinal { get; set; }

        [Display(Name = "Tipo Solicitud")]
        [StringLength(200)]
        public string TipoSolicitud { get; set; }

        [Display(Name = "Promoción")]
        [StringLength(200)]
        public string PromocionId { get; set; }

        [Display(Name = "Ofrecimiento")]
        [StringLength(200)]
        public string OfrecimientoId { get; set; }

        [Display(Name = "Fecha Instalación")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaInstalacion { get; set; }

        [Display(Name = "Cambio Plan/Migracion")]
        [StringLength(200)]
        public string CambioPlanMigracion { get; set; }

        [Display(Name = "Decos")]
        [StringLength(200)]
        public string Decos { get; set; }

        [Display(Name = "Canales")]
        [StringLength(200)]
        public string Canales { get; set; }

        [Display(Name = "Nuevo Precio")]
        [StringLength(200)]
        public string NuevoPrecio { get; set; }

        [Display(Name = "TieneOfertaOtro")]
        [StringLength(200)]
        public string TieneOfertaOtro { get; set; }

        [Display(Name = "Plan Ofrecido")]
        [StringLength(200)]
        public string PlanOfrecido { get; set; }

        [Display(Name = "Se Deriva")]
        public int? SeDerivaId { get; set; }

        [ForeignKey("SeDerivaId")]
        public virtual SeDeriva SeDeriva { get; set; }

        [Display(Name = "Usuario Deriva")]
        public int? UsuarioDerivaId { get; set; }

        [ForeignKey("UsuarioDerivaId")]
        public virtual UsuarioDeriva UsuarioDeriva { get; set; }

        [Display(Name = "¿Fue Retenido Coach/Supervisor?")]
        [StringLength(200)]
        public string FueRetenido { get; set; }

        [Display(Name = "Escoja")]
        [StringLength(200)]
        public string Escoja { get; set; }

        [Display(Name = "¿Por qué?")]
        [DataType(DataType.MultilineText)]
        public string Porque { get; set; }

        [Display(Name = "Origen")]
        [StringLength(200)]
        public string Origen { get; set; }

        [Display(Name = "Locación")]
        [StringLength(200)]
        public string Locacion { get; set; }

        [Required]
        [Display(Name = "Observaciones")]
        [DataType(DataType.MultilineText)]
        public string Observaciones { get; set; }

        [Required]
        [Display(Name = "Nro Interacción")]
        [StringLength(200)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Use solo caracteres numéricos.")]
        public string NroInteraccion { get; set; }

        [Display(Name = "Pendiente")]
        public bool Pendiente { get; set; }

        [Display(Name = "Plantilla Migración")]
        [DataType(DataType.MultilineText)]
        public string PlantillaMigracion { get; set; }

        [Display(Name = "SOT Migración")]
        [StringLength(200)]
        public string SOTMigracion { get; set; }

        [Display(Name = "Estado SOT Migración")]
        [StringLength(200)]
        public string EstadoSOTMigracion { get; set; }

        [Display(Name = "Plantilla Visita Técnica")]
        [DataType(DataType.MultilineText)]
        public string PlantillaVisitaTecnica { get; set; }

        [Display(Name = "SOT Visita Técnica")]
        [StringLength(200)]
        public string SOTVisitaTecnica { get; set; }

        [Display(Name = "Estado SOT Visita Técnica")]
        [StringLength(200)]
        public string EstadoSOTVisitaTecnica { get; set; }

        [Display(Name = "Plantilla Traslado Externo (SGA)")]
        [DataType(DataType.MultilineText)]
        public string PlantillaTrasladoExternoSGA { get; set; }

        [Display(Name = "SOT Traslado Externo (SGA)")]
        [StringLength(200)]
        public string SOTTrasladoExternoSGA { get; set; }

        [Display(Name = "Estado SOT Traslado Externo (SGA)")]
        [StringLength(200)]
        public string EstadoSOTTrasladoExternoSGA { get; set; }

        [Display(Name = "Plantilla Promoción (SGA)")]
        [DataType(DataType.MultilineText)]
        public string PlantillaPromocionSGA { get; set; }

        [Display(Name = "Plantilla Suspensión Temporal")]
        [DataType(DataType.MultilineText)]
        public string PlantillaSuspensionTemporal { get; set; }

        [Display(Name = "Plantilla Registro Retenidos")]
        [DataType(DataType.MultilineText)]
        public string PlantillaRegistroRetenidos { get; set; }

        [Display(Name = "Plantilla Seguimiento SOT")]
        [DataType(DataType.MultilineText)]
        public string PlantillaSeguimientoSOT { get; set; }

        [Display(Name = "Cargo Fijo")]
        [StringLength(200)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Use solo caracteres numéricos.")]
        public string CargoFijoCalc { get; set; }

        [Display(Name = "% Dscto")]
        [StringLength(200)]
        public string DescuentoCalc { get; set; }

        [Display(Name = "Mes a Aplicar")]
        [StringLength(200)]
        public string MesAplicarCalc { get; set; }

        [Display(Name = "Ciclo")]
        [StringLength(200)]
        public string CicloCalc { get; set; }

        [Display(Name = "Concepto")]
        [StringLength(200)]
        public string ConceptoCalc { get; set; }

        [Display(Name = "Servicio")]
        [StringLength(200)]
        public string ServicioCalc { get; set; }

        [Display(Name = "Motivo de Promoción Adelantada")]
        [StringLength(200)]
        public string MotivoProm { get; set; }

        [Display(Name = "Nro de Factura")]
        [StringLength(200)]
        public string NroFacturaProm { get; set; }

        [Display(Name = "Detalle de Motivo de Dscto")]
        [DataType(DataType.MultilineText)]
        public string DetalleMotivoProm { get; set; }

        [Display(Name = "Plantilla Promoción Adelantada N/C")]
        [DataType(DataType.MultilineText)]
        public string PlantillaPromocionAdelantadaNC { get; set; }

        [Display(Name = "Paquete 1")]
        [StringLength(200)]
        public string PaquetesPaq1 { get; set; }

        [Display(Name = "Costo Paquete 1")]
        [StringLength(200)]
        public string CostoPaq1 { get; set; }

        [Display(Name = "Paquete 2")]
        [StringLength(200)]
        public string PaquetesPaq2 { get; set; }

        [Display(Name = "Costo Paquete 2")]
        [StringLength(200)]
        public string CostoPaq2 { get; set; }

        [Display(Name = "Paquete 3")]
        [StringLength(200)]
        public string PaquetesPaq3 { get; set; }

        [Display(Name = "Costo Paquete 3")]
        [StringLength(200)]
        public string CostoPaq3 { get; set; }

        [Display(Name = "Mes de Paquete Activo")]
        [StringLength(200)]
        public string MesPaq { get; set; }

        [Display(Name = "CF Antiguo")]
        [StringLength(200)]
        public string CFCamb { get; set; }

        [Display(Name = "Plantilla Promoción SIAC")]
        [DataType(DataType.MultilineText)]
        public string PlantillaPromocionSIAC { get; set; }

        [Display(Name = "Nro Caso")]
        [StringLength(200)]
        public string NroCaso { get; set; }

        [Display(Name = "Interacc Prom Anterior")]
        [StringLength(200)]
        public string InteraccPromAnterior { get; set; }

        [Display(Name = "Proyecto")]
        [StringLength(200)]
        public string ProyectoProm { get; set; }

        [Display(Name = "Antigüedad")]
        [StringLength(200)]
        public string AntiguedadProm { get; set; }

        [Display(Name = "Motivo de Baja")]
        [StringLength(200)]
        public string MotivoBajaOfre { get; set; }

        [Display(Name = "Antigüedad")]
        [StringLength(200)]
        public string AntiguedadOfre { get; set; }

        [Display(Name = "Problemas Técnicos")]
        [StringLength(200)]
        public string ProblemasTecOfre { get; set; }

        [Display(Name = "Reportes Facturación")]
        [StringLength(200)]
        public string ReportesFactOfre { get; set; }

        [Display(Name = "Servicios")]
        [StringLength(200)]
        public string ServiciosOfre { get; set; }

        [Display(Name = "DsctoRetenciones")]
        [StringLength(200)]
        public string DsctoRetencionesOfre { get; set; }

        [Display(Name = "Estado de Cuenta")]
        [StringLength(200)]
        public string EstadoCuentaOfre { get; set; }

        [Display(Name = "Asesor")]
        [StringLength(200)]
        public string Asesor { get; set; }

        [Display(Name = "IP")]
        [StringLength(200)]
        public string IP { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Gestión")]
        public DateTime FechaGestion { get; set; }

    }
}