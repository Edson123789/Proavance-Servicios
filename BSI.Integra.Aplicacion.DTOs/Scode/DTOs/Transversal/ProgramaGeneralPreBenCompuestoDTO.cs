using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProgramaGeneralPreBenCompuestoDTO
    {
        public OportunidadCompetidorDTO OportunidadCompetidor { get; set; }
        public List<ProgramaGeneralPrerequisitoOportunidadDTO> ListaPreGeneral { get; set; }
        public List<ProgramaGeneralPrerequisitoOportunidadDTO> ListaPreEspecifico { get; set; }
        public List<ProgramaGeneralBeneficioOportunidadDTO> ListaBeneficios { get; set; }
        public List<EmpresaFiltroDTO> ListaCompetidores { get; set; }
    }
}
