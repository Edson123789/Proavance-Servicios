using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class TipoDisponibilidadPersonalBO:BaseBO
    {
        public string Nombre { get; set; }
        public bool GeneraCosto { get; set; }
    }
}
