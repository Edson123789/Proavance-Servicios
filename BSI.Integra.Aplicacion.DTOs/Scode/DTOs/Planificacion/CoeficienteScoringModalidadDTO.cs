using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CoeficienteScoringModalidadDTO
    {
        public List<ScoringModalidadProgramaDTO> ModalidadEscoring { get; set; }
        public List<ProgramaGeneralPerfilCoeficienteDTO> ModalidadCoefiente { get; set; }
    }
}
