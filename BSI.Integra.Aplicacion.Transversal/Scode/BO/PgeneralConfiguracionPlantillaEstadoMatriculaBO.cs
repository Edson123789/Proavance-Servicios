using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PgeneralConfiguracionPlantillaEstadoMatriculaBO :BaseBO
    {
        public int IdPgeneralConfiguracionPlantillaDetalle { get; set; }
        public int IdEstadoMatricula { get; set; }
        public int? IdMigracion { get; set; }
    }
}
