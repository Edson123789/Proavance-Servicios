using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class SedeTrabajoBO : BaseBO
    {
        public string Nombre { get; set; }
        public string IpCentral { get; set; }
        public string Comentarios { get; set; }
        public int? IdMigracion { get; set; }
    }
}
