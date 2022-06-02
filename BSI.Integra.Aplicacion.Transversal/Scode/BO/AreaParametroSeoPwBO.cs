using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class AreaParametroSeoPwBO : BaseBO
    {
        public string Descripcion { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public int IdParametroSeopw { get; set; }
    }
}
