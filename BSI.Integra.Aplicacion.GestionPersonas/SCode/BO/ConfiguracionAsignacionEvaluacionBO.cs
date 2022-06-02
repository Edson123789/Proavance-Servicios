using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class ConfiguracionAsignacionEvaluacionBO : BaseBO
    {
        public int? IdProcesoSeleccionEtapa { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int IdEvaluacion { get; set; }
        public int NroOrden { get; set; }
        public int? IdMigracion { get; set; }
    }
}
