using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EvaluacionListadoDTO
    {
        public int Id { get; set; }
        public int IdPEspecifico { get; set; }
        public int Grupo { get; set; }
        public string Nombre { get; set; }
        public int? IdCriterioEvaluacion { get; set; }
        public int Porcentaje { get; set; }
    }
}
