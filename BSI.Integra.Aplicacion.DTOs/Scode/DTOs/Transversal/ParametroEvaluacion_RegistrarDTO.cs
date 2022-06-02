using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ParametroEvaluacion_RegistrarDTO
    {
        public int Id { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public int IdEscalaCalificacion { get; set; }
        public string Nombre { get; set; }
        public int Ponderacion { get; set; }
    }
}
