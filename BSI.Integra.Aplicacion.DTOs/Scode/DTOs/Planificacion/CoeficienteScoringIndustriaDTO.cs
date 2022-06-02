using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{ 
    public class CoeficienteScoringIndustriaDTO
    {
        public List<ScoringIndustriaProgramaDTO> IndustriaEscoring { get; set; }
        public List<ProgramaGeneralPerfilCoeficienteDTO> IndustriaCoefiente { get; set; }
    }
}
