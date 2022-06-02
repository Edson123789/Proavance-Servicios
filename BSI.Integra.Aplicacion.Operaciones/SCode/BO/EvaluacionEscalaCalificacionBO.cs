using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public class EvaluacionEscalaCalificacionBO : BaseBO
    {
        public int IdModalidadCurso { get; set; }
        public string CodigoCiudad { get; set; }
        public decimal EscalaCalificacion { get; set; }
        public decimal NotaAprobatoria { get; set; }
        public int RedondeoDecimales { get; set; }
        public string EscalaTexto { get; set; }
        public string NotaAprobatoriaTexto { get; set; }
    }
}
