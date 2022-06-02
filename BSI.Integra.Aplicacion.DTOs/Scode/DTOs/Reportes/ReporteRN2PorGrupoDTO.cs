using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteRN2PorGrupoDTO
    {
        public int IdGrupo { get; set; }
        public string Grupo { get; set; }
        public List<ReporteRN2PorAsesorDTO> ListaAsesores { get; set; }
    }
}
