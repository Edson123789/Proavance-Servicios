using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoBusquedaBO: BaseBO
    {
        public DateTime FechaConsulta { get; set; }
        public string CodigoSeguridad { get; set; }
        public int TipoIdentificacion { get; set; }
        public string NroDocumento { get; set; }
    }
}
