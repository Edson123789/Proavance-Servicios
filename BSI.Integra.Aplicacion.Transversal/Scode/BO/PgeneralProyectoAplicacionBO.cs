using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PgeneralProyectoAplicacionBO : BaseBO
    {
        public int IdPgeneral { get; set; }
        public int? IdMigracion { get; set; }
    }
}
