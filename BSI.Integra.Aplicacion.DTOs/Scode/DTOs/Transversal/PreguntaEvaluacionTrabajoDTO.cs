using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PreguntaEvaluacionTrabajoDTO
    {
    }

    public class registroPreguntaEvaluacionTrabajoDTO
    {
        public int Id { get; set; }
        public int IdConfigurarEvaluacionTrabajo { get; set; }
        public int IdPregunta { get; set; }
    }
}
