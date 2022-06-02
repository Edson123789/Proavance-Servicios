using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class InteraccionEnlaceDetalleMailChimpBO : BaseBO
    {
        public int IdInteraccionEnlaceMailChimp { get; set; }
        public int CantidadClicks { get; set; }
        public DateTime Fecha { get; set; }
        public int? IdMigracion { get; set; }
    }
}
