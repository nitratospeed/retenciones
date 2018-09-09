using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Retenciones.Models
{
    public class UsuarioDeriva
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Usuario Deriva")]
        public string Nombre { get; set; }

        public int MotivoInicialId { get; set; }
    }
}