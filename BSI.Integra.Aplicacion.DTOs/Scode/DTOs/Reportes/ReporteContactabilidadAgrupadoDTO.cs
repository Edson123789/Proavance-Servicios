using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteContactabilidadAgrupadoDTO
    {
        public int IdAsesor { get; set; }
        public List<ReporteContactabilidadAsesorIndicadoresDTO> Lista { get; set; }
    }
}
