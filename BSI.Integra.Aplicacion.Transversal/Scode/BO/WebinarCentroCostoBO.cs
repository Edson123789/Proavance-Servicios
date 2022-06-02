using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class WebinarCentroCostoBO : BaseBO
    {
        public int IdWebinar { get; set; }
        public int IdCentroCosto { get; set; }
        public bool Activo { get; set; }
        public int? IdMigracion { get; set; }
    }
}
