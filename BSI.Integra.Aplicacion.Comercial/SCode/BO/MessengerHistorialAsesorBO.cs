using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class MessengerHistorialAsesorBO : BaseEntity
    {
        public int IdMessengerAsesorDetalle { get; set; }
        public int IdMessengerAsesor { get; set; }
        public DateTime Fecha { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
