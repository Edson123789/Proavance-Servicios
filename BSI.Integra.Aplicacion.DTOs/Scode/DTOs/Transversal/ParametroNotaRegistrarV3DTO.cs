using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ParametroNotaRegistrarV3DTO
    {
        public int Id { get; set; }
        [Required]
        public int IdPespecifico { get; set; }
        [Required]
        public int Grupo { get; set; }
        [Required]
        public int IdMatriculaCabecera { get; set; }
        [Required]
        public int IdEsquemaEvaluacionPGeneralDetalle { get; set; }
        [Required]
        public int IdParametroEvaluacion { get; set; }
        public int? IdEscalaCalificacionDetalle { get; set; }
        public int? PortalTareaEvaluacionTareaId { get; set; }

        public bool EsProyectoAnterior { get; set; }
        public int? IdProyectoAplicacionEnvioAnterior { get; set; }

        public string NombreArchivoRetroalimentacion { get; set; }
        public string UrlArchivoSubidoRetroalimentacion { get; set; }
    }
}
