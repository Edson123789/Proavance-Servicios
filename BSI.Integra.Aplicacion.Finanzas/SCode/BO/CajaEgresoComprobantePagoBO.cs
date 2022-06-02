using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class CajaEgresoComprobantePagoBO : BaseBO
    {
        public int Id { get; set; }
        public int IdCajaEgreso { get; set; }
        public int IdComprobantePago { get; set; }
        
    }
}
