using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CoeficienteScoringATrabajoDTO
    {
        public List<ScoringTrabajoProgramaDTO> TrabajoEscoring { get; set; }
        public List<ProgramaGeneralPerfilCoeficienteDTO> TrabajoCoefiente { get; set; }
    }
}
