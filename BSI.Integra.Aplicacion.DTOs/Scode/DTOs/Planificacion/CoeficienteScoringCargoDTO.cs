using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CoeficienteScoringCargoDTO
    {
        public List<ScoringCargoProgramaDTO> CargoEscoring { get; set; }
        public List<ProgramaGeneralPerfilCoeficienteDTO> CargoCoefiente { get; set; }
    }
}
