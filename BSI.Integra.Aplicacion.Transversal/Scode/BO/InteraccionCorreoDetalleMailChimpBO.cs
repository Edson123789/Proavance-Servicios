using System;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class InteraccionCorreoDetalleMailChimpBO : BaseBO
    {
        public int IdInteraccionCorreoMailChimp { get; set; }
        public DateTime Fecha { get; set; }
        public int? IdMigracion { get; set; }
    }
}
