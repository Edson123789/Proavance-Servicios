using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public class FlujoPorPespecificoBO : BaseBO
    {
        public int IdPespecifico { get; set; }
        public int IdFlujoActividad { get; set; }
        public int? IdFlujoOcurrencia { get; set; }
        public int IdClasificacionPersona { get; set; }
        public DateTime? FechaEjecucion { get; set; }
        public DateTime? FechaSeguimiento { get; set; }
    }
}
