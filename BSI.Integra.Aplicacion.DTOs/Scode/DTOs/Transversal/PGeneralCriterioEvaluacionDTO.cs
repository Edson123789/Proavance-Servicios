using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PGeneralCriterioEvaluacionDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public int IdModalidadCurso { get; set; }
        public string Nombre { get; set; }
        public int Porcentaje { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public int IdTipoPromedio { get; set; }

    }
}
