using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Scode.BO{
    public class PGeneralCriterioEvaluacionHijoBO : BaseBO

    {
        public int IdPgeneral { get; set; }
        public bool ConsiderarNota { get; set; }
        public int? Porcentaje { get; set; }
        public int IdModalidadCurso { get; set; }
        public int IdTipoPromedio { get; set; }
        public int IdPgeneralHijo { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdCriterioEvaluacion { get; set; }
    }
}
