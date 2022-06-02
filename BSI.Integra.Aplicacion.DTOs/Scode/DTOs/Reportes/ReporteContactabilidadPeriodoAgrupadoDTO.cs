using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteContactabilidadPeriodoAgrupadoDTO
    {
        public int IdCodigoPais { get; set; }
        public List<ReporteContactabilidadPeriodoDTO>  Lista { get; set; }
    }
}
