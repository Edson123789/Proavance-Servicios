using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class CriterioEvaluacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<CriterioEvaluacionTipoProgramaDTO> IdTipoPrograma { get; set; }
        public List<CriterioEvaluacionModalidadCursoDTO> IdModalidadCurso { get; set; }
        public List<CriterioEvaluacionTipoPersonaDTO> IdTipoPersona { get; set; }
        public int IdCriterioEvaluacionCategoria { get; set; }

        public string Usuario { get; set; }
    }

  
}
