using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AsignarAsesorManualDTO
    {
        public int[] IdOportunidades{ get; set; }
        public int? IdAsesor { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public int? IdCentroCosto { get; set; }
    }
}
