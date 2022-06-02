using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PgeneralConfiguracionPlantillaEstadoMatriculaDTO
    {
        public int Id { get; set; }
        public int IdEstadoMatricula { get; set; }
        public int? IdPgeneralConfiguracionPlantillaDetalle { get; set; }
    }
}
