using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class PEspecificoConsumoBO : BaseBO
    {
        public int? IdPespecificoSesion { get; set; }
        public int? IdHistoricoProductoProveedor { get; set; }
        public decimal Cantidad { get; set; }
        public string Factor { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
