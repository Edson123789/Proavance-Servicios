using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PEspecificoMatriculaAlumnoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPespecifico { get; set; }
        public int IdPEspecificoRecuperacion { get; set; } //programa especifico que va a ser recuperado
        public int Grupo { get; set; } //Grupo al que pertence el curso
        public int IdAlumno { get; set; } //IdAlumno
        public int IdOportunidad { get; set; }//Oportunidad del alumno
        public string Usuario { get; set; }
    }
}
