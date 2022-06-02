using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class PGeneralCursoCriterioHijoVistaDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralHijo { get; set; }
        public string Nombre { get; set; }
        public bool ConsiderarNota { get; set; }
        public int IdPGeneral_Padre { get; set; }
        public int Porcentaje { get; set; }
        public int IdModalidadCurso { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public int EsCurso { get; set; }



    }
}
