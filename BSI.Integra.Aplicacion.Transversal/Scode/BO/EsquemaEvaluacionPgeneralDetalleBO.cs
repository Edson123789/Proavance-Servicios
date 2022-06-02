using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class EsquemaEvaluacionPgeneralDetalleBO : BaseBO
    {
        public int IdEsquemaEvaluacionPgeneral { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public int? IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string UrlArchivoInstrucciones { get; set; }
    }
}
