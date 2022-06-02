using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Reportes
{
    public class ParticipacionExpositorFiltroDTO
    {
        public int IdExpositor { get; set; }

        public string IdArea { get; set; }
        public string IdSubArea { get; set; }
        public string IdPGeneral { get; set; }
        public string IdProgramaEspecifico { get; set; }
        public string IdCentroCosto { get; set; }

        public string IdEstadoPEspecifico { get; set; }
        public string IdCodigoBSCiudad { get; set; }
        public string IdModalidadCurso { get; set; }


        public int? IdCentroCostoD { get; set; }

        public string IdProveedorPlanificacion { get; set; }
        public string IdProveedorOperaciones { get; set; }
        public string IdProveedorFur { get; set; }
        public bool? SinNotaAprobada { get; set; }
        public bool? SinAsistenciaAprobada { get; set; }
    }
}
