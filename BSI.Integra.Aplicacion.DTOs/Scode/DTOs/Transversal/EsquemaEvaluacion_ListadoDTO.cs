using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EsquemaEvaluacion_ListadoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdFormaCalculoEvaluacion { get; set; }
        public string FormaCalculoEvaluacion { get; set; }
    }
    public class EsquemaEvaluacionCongelado_ListadoDTO
    {
        public int Id { get; set; }
        public int? IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno { get; set; }

        public int IdEsquemaEvaluacionPGeneral { get; set; }
        public string NombreEsquema { get; set; }
        public int IdFormaCalculoEvaluacion { get; set; }
        public string FormaCalculoEvaluacion { get; set; }
        public List<EsquemaEvaluacionDetalle_CongeladoDTO> EsquemasEvaluacionDetalle { get; set; }
    }
}
