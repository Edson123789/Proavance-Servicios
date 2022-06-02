using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PgeneralConfiguracionPlantillaSubEstadoMatriculaDTO
    {
        public int Id { get; set; }
        public int IdSubEstadoMatricula { get; set; }
        public int? IdPgeneralConfiguracionPlantillaDetalle { get; set; }
    }
}
