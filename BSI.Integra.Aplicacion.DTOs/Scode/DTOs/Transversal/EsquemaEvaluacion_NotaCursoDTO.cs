using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EsquemaEvaluacion_NotaCursoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecifico { get; set; }
        public int Grupo { get; set; }
        public List<EsquemaEvaluacion_DetalleCalificacionDTO> DetalleCalificacion { get; set; }
        public decimal? NotaCurso { get; set; }
    }
}
