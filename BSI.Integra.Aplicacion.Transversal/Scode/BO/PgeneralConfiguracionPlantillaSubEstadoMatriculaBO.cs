using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PgeneralConfiguracionPlantillaSubEstadoMatriculaBO : BaseBO
    {
        public int IdPgeneralConfiguracionPlantillaDetalle { get; set; }
        public int IdSubEstadoMatricula { get; set; }
        public int? IdMigracion { get; set; }
    }
}
