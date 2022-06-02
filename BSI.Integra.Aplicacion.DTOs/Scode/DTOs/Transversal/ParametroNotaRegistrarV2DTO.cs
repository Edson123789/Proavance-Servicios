using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ParametroNotaRegistrarV2DTO
    {
        public int Id { get; set; }
        [Required]
        public int IdPespecifico { get; set; }
        [Required] 
        public int Grupo { get; set; }
        [Required]
        public int IdMatriculaCabecera { get; set; }
        [Required] 
        public int IdParametroEvaluacion { get; set; }
        public int? IdEscalaCalificacionDetalle { get; set; }
    }
}
