using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ActividadCalificableDetalleDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPespecifico { get; set; }
        public int Grupo { get; set; }
        public string Alumno { get; set; }
        public string Pespecifico { get; set; }

        public int IdParametroEvaluacion { get; set; }
        public string NombreParametroEvaluacion { get; set; }
        public int IdEscalaEvaluacionAplicable { get; set; }
        public int? IdEscalaCalificacionDetalle { get; set; }
        public decimal? ValorEscala { get; set; }

        public int IdEsquemaEvaluacionPGeneralDetalle { get; set; }
        public string NombreEsquemaEvaluacionPGeneralDetalle { get; set; }

        public string Retroalimentacion { get; set; }

        public int? PortalTareaEvaluacionTareaId { get; set; }
        public string NombreArchivoSubido { get; set; }
        public string UrlArchivoSubido { get; set; }

        public string NombreArchivoRetroalimentacion { get; set; }
        public string UrlArchivoSubidoRetroalimentacion { get; set; }
        public bool EsProyectoAnterior { get; set; }
        public int? IdProyectoAplicacionEnvioAnterior { get; set; }

        public decimal? NotaCriterioPGeneralDetalle { get; set; }

        public string PespecificoPadre { get; set; }
        public string PespecificoHijo { get; set; }
    }
}
