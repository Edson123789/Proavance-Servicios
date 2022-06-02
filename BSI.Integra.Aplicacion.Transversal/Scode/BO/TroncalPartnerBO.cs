using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class TroncalPartnerBO : BaseBO
    {
        public string Nombre { get; set; }
        public int? EstadoDocumento { get; set; }
        public int? IdMigracion { get; set; }
    }
}
