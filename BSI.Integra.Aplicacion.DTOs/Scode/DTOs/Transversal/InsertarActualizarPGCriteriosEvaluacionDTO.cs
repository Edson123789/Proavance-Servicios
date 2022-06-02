using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class InsertarActualizarPGCriteriosEvaluacionDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public bool ConsiderarNota { get; set; }
        public int Porcentaje { get; set; }
        public string Nombre { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public int IdModalidadCurso { get; set; }
        public int IdTipoPromedio { get; set; }
       
        public string usuario { get; set; }
    }
}
