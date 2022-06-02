using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReportePresupuestoFiltroDTO
    {
        public int? sede { get; set; }
        public int? cuenta { get; set; }
        public int? usuarioCreacion { get; set; }
        public int? rubro { get; set; }
        public int? tipoPedido { get; set; }
        public int? centroCosto { get; set; }
        public int? proveedor { get; set; }
        public int? codigoFur { get; set; }
        public int? faseFur { get; set; }
        public int? subFaseFur { get; set; }
        public int? estadoComprobante { get; set; }
        public List<int> periodoProgramacionOriginal { get; set; }
        public List<int> periodoProgramacionActual { get; set; }
        public List<int> periodoFechaLimiteFur { get; set; }
        public List<int> periodoFechaCobroBanco { get; set; }

    }
}
