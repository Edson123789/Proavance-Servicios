using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PGeneralCriterioEvaluacionBO : BaseBO
    {
        public int IdPgeneral { get; set; }
        public int IdModalidadCurso { get; set; }
        public string Nombre { get; set; }
        public int Porcentaje { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdCriterioEvaluacion { get; set; }
        public int? IdTipoPromedio { get; set; }
    }
}
