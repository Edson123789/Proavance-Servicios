using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CoeficienteScoringAFormacionDTO
    {
        public List<ScoringAFormacionProgramaDTO> FormacionEscoring { get; set; }
        public List<ProgramaGeneralPerfilCoeficienteDTO> FormacionCoefiente { get; set; }
    }
}
