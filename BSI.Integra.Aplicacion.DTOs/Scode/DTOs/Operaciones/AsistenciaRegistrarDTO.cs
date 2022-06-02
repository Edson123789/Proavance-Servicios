using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Operaciones
{
    public class AsistenciaRegistrarDTO
    {
        public int Id { get; set; }
        [Required]
        public int IdPEspecificoSesion { get; set; }
        [Required]
        public int IdMatriculaCabecera { get; set; }
        [Required]
        public bool Asistio { get; set; }
        public bool Justifico { get; set; }
    }
}
