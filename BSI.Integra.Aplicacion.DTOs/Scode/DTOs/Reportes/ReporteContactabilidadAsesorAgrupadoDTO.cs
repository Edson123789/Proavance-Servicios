using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteContactabilidadAsesorAgrupadoDTO
    {
        public int IdAsesor { get; set; }
        public string NombreAsesor { get; set; }
        public List<TasaContactabilidadDTO> Lista { get; set; }
    }
}
