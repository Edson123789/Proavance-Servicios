using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Operaciones
{
    public class PEspecificoSilaboRegistrarDTO
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public string ObjetivoAprendizaje { get; set; }
        public string PautaComplementaria { get; set; }
        public string PublicoObjetivo { get; set; }
        public string Material { get; set; }
        public string Bibliografia { get; set; }

        public string Usuario { get; set; }
        public int IdProveedor { get; set; }

        public List<EvaluacionListadoDTO> ListadoCriteriosEvaluacion { get; set; }
    }
}
