using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CoeficienteScoringCategoriaDTO
    {
        public List<ScoringCategoriaProgramaDTO> CategoriaEscoring { get; set; }
        public List<ProgramaGeneralPerfilCoeficienteDTO> CategoriaCoefiente { get; set; }
    }
}
