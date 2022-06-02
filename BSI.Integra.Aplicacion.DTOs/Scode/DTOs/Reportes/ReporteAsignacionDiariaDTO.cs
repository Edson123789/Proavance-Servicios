using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteAsignacionDiariaDTO
    {
        public string Area { get; set; }
        public int IdAsesor { get; set; }
        public string Asesor { get; set; }
        public List<ProgramaPorAsesorDetalleDTO> ListaProgramas { get; set; }
    }
}
