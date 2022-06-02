using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ExpositorPorAreaBO : BaseBO
    {
        public int IdExpositor { get; set; }
        public int IdArea { get; set; }
        public int? IdMigracion { get; set; }
    }
}
