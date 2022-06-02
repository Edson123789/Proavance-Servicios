using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteTasaConversionConsolidadaGraficaDTO
    {
        public List<CoordinadorFiltroDTO> Coordinadores { get; set; }
        public List<AsesorNombreFiltroDTO> Asesores { get; set; }
        public List<PeriodoFiltroDTO> Periodos { get; set; }
    }
}
