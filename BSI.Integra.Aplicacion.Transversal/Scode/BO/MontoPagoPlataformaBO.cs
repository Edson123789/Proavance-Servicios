using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MontoPagoPlataformaBO : BaseBO
    {
        public int IdMontoPago { get; set; }
        public int IdPlataformaPago { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
