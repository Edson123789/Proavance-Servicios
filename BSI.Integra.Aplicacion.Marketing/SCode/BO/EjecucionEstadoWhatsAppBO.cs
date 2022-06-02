using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class EjecucionEstadoWhatsAppBO :BaseBO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int CantidadTiempoFrecuencia { get; set; }
        public int IdTiempoFrecuencia { get; set; }
    }
}
