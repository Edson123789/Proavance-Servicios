using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PgeneralTipoDescuentoBO : BaseBO
    {
        public int IdPgeneral { get; set; }
        public int? IdTipoDescuento { get; set; }
        public bool? FlagPromocion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
