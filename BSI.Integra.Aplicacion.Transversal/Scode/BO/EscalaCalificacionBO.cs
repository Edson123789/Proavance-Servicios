using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class EscalaCalificacionBO : BaseBO
    {
        public string Nombre { get; set; }

        public List<EscalaCalificacionDetalleBO> ListadoDetalle { get; set; }
    }
}
