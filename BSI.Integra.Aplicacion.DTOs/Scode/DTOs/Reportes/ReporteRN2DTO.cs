using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteRN2DTO
    {
        public List<ReporteRN2PorAsesorDTO> ListaGeneral { get; set; }
        public List<ReporteRN2PorGrupoDTO> ListaPorGrupo { get; set; }

    }
}
