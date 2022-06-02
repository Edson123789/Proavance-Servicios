using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public  class ReporteContactabilidadDataDTO
    {
        public  List<ReporteContactabilidadAsesorAgrupadoDTO> ComparativoAsesor { get; set; }
        public List<ReporteContactabilidadDTO> AsesorContactabilidad { get; set; }
    }
}
