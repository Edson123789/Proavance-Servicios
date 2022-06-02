using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatoNotaAulaVirtualDTO
    {
        public int IdCurso { get; set; }
        public string NombreCurso { get; set; }
        public string TipoCurso { get; set; }
        public string Seccion { get; set; }
        public decimal? Promedio { get; set; }
        public List<DatoEvaluacionNotaAulaVirtualDTO> ListadoEvaluaciones { get; set; }
    }
}
