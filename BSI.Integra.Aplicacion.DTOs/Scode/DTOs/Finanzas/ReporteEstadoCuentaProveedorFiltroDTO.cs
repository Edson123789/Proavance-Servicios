using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteEstadoCuentaProveedorFiltroDTO
    {
        public string IdsSede { get; set; }
        public string IdsProveedor { get; set; }
        public string IdsPlanContable { get; set; }
        public string CodigoFur { get; set; }
        public string Estado { get; set; }
    }
}
