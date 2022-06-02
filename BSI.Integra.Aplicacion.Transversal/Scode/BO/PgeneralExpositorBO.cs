using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PgeneralExpositorBO : BaseBO
    {
        public int IdPgeneral { get; set; }
        public int IdExpositor { get; set; }
        public int Posicion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
