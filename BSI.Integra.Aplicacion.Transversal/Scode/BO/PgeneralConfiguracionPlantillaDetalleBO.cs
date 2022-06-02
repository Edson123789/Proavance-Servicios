using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PgeneralConfiguracionPlantillaDetalleBO : BaseBO
    {
        public int IdPgeneralConfiguracionPlantilla { get; set; }
        public int IdModalidadCurso { get; set; }
        public int? IdOperadorComparacion { get; set; }
        public decimal? NotaAprobatoria { get; set; }
        public bool DeudaPendiente { get; set; }
    }
}
