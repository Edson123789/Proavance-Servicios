using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class CompromisoAlumnoBO : BaseBO
    {
        public int IdCronogramaPagoDetalleFinal { get; set; }
        public DateTime FechaCompromiso { get; set; }
        public DateTime FechaGeneracionCompromiso { get; set; }
        public decimal Monto { get; set; }
        public int? IdMoneda { get; set; }
        public int Version { get; set; }
    }
}
