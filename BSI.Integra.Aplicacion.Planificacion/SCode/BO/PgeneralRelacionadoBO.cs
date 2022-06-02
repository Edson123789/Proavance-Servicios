using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class PgeneralRelacionadoBO : BaseBO
    {
        public int IdPgeneral { get; set; }
        public int IdPgeneralRelacionado { get; set; }
    }
}
