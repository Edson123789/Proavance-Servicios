using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteSeguimientoOportunidadCombosDTO
    {
        public List<FiltroDTO> CentroCostos { get; set; }
        public List<FaseOportunidadFiltroDTO> FaseOportunidades {get;set;}
        public List<PersonalAsignadoDTO> Asesores { get; set; }
        public List<EstadosMatriculaDTO> Estados { get; set; }

    }
    public class ReporteSeguimientoOportunidadCombosGeneralDTO
    {
        public List<FiltroDTO> CentroCostos { get; set; }
        public List<FaseOportunidadFiltroDTO> FaseOportunidades { get; set; }
        public List<AsesorNombreFiltroDTO> Asesores { get; set; }
        public List<EstadosMatriculaDTO> Estados { get; set; }

    }
}
