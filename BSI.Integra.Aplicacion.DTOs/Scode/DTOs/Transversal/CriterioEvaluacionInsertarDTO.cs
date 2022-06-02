using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class CriterioEvaluacionInsertarDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<int> IdTipoPrograma { get; set; }
        public List<int> IdModalidadCurso { get; set; }
        public List<int> IdTipoPersona { get; set; }
        public int IdCriterioEvaluacionCategoria { get; set; }

        public int? IdFormaCalculoEvaluacion { get; set; }
        public int? IdFormaCalificacionEvaluacion { get; set; }
        public int? IdFormaCalculoEvaluacionParametro { get; set; }

        public List<ParametroEvaluacion_RegistrarDTO> ListadoParametro { get; set; }

        public string Usuario { get; set; }
    }
}
