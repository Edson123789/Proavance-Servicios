using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CoeficienteScoringCiudadDTO
    {
        public List<ScoringCiudadProgramaDTO> CiudadEscoring { get; set; }
        public List<ProgramaGeneralPerfilCoeficienteDTO> CiudadCoefiente { get; set; }
    }
}
