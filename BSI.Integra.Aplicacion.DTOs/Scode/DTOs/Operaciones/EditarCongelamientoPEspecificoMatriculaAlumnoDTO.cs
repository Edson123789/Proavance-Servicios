using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EditarCongelamientoPEspecificoMatriculaAlumnoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecifico { get; set; }
        public int idEsquemaEvaluacionGeneral { get; set; }
    }
}
