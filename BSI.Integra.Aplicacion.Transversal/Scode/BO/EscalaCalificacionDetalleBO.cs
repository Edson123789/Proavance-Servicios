using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class EscalaCalificacionDetalleBO : BaseBO
    {
        public int IdEscalaCalificacion { get; set; }
        public string Nombre { get; set; }
        public decimal Valor { get; set; }
    }
}
