using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PgeneralProyectoAplicacionModalidadBO : BaseBO
    {
        public int IdPgeneralProyectoAplicacion { get; set; }
        public int IdModalidadCurso { get; set; }
        public int? IdMigracion { get; set; }
    }
}
