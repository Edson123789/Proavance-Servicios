using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.BO
{
    public class ConfiguracionAsignacionExamenBO : BaseBO
    {
        public int IdProcesoSeleccion { get; set; }
        public int IdExamen { get; set; }
        public int? NroOrden { get; set; }
        public int? IdMigracion { get; set; }
    }
}
