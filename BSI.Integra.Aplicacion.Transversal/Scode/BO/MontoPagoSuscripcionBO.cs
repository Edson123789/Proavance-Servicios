using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MontoPagoSuscripcionBO : BaseBO
    {
        public int IdMontoPago { get; set; }
        public int IdSuscripcionProgramaGeneral { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
