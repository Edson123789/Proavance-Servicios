
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class InformacionChatSoporteAlumnoDTO
    {
        public string CodigoMatricula { get; set; }
        public string ProgramaGeneralSoporte { get; set; }
        public string ProgramaGeneralCurso { get; set; }
        public string CentroCosto { get; set; }
        public string Coordinadora { get; set; }
        public string Capitulo { get; set; }
        public string Sesion { get; set; }
        public string NombreAlumno { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }

    }

    public class InformacionLogChatSoporteAlumnoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdAlumno { get; set; }
        public int IdProgramaGeneral_Padre { get; set; }
        public int IdProgramaGeneral_Hijo { get; set; }
        public int IdCapitulo { get; set; }
        public int IdSesion { get; set; }
    }
}
