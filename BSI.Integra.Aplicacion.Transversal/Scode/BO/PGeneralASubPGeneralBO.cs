using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PGeneralASubPGeneralBO :BaseBO
    {
        public int IdPgeneralPadre { get; set; }
        public int IdPgeneralHijo { get; set; }
        public int? Orden { get; set; }
    }
}
