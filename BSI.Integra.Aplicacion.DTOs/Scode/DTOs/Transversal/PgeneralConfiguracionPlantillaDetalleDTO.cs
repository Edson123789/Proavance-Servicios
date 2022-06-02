using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PgeneralConfiguracionPlantillaDetalleDTO
    {
        public int Id { get; set; }
        public int IdPgeneralConfiguracionPlantilla { get; set; }
        public int IdModalidadCurso { get; set; }
        public List<PgeneralConfiguracionPlantillaEstadoMatriculaDTO> IdEstadoMatricula { get; set; }
        public List<PgeneralConfiguracionPlantillaSubEstadoMatriculaDTO> IdSubEstadoMatricula { get; set; }
        public int? IdOperadorComparacion { get; set; }
        public decimal? NotaAprobatoria { get; set; }
        public bool DeudaPendiente { get; set; }
    }
}
