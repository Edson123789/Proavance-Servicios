using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteContactabilidadDataV2DTO
    {
        public List<ReporteContactabilidadAgrupadoDTO> ComparativoAsesor { get; set; }
        public List<ReporteContactabilidadDTO> AsesorContactabilidad { get; set; }
        public List<ReporteContactabilidadMinutosDTO> MinutosContactabilidad { get; set; }
    }
}
