using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReportePagosDiaPeriodoFiltroDTO
    {
        public int Periodo { get; set; }
        public List<string> Coordinadora { get; set; }
    }
}
