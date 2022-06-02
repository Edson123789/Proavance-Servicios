using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosMatriculaCabeceraDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
    }

    public class DatosAlumnoCoordinadorMatriculaCabeceraDTO
    {
        public int IdAlumno { get; set; }
        public string UsuarioCoordinadorAcademico { get; set; }
    }

    public class InformacionMatriculaCabeceraDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdPaquete { get; set; }
        public int IdCronograma { get; set; }
        public int IdMatriculaCabecera { get; set; }
    }
}
