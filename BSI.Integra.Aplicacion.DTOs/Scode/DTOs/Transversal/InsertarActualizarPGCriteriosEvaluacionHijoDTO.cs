using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class InsertarActualizarPGCriteriosEvaluacionHijoDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public bool ConsiderarNota { get; set; }
        public int Porcentaje { get; set; }
        public int IdModalidadCurso { get; set; }
        public int IdTipoPromedio { get; set; }
        public int IdPGeneralHijo { get; set; }
        public int EsCurso { get; set; }
        public string Usuario { get; set; }
    }
}
