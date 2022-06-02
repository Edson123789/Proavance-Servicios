using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class EsquemaEvaluacionPgeneralBO : BaseBO
    {
        public int IdEsquemaEvaluacion { get; set; }
        public int IdPgeneral { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool EsquemaPredeterminado { get; set; }
        public List<EsquemaEvaluacionPgeneralDetalleBO> ListadoDetalle { get; set; }
        public List<EsquemaEvaluacionPgeneralModalidadBO> ListadoModalidad{ get; set; }
        public List<EsquemaEvaluacionPgeneralProveedorBO> ListadoProveedor { get; set; }
    }
}
