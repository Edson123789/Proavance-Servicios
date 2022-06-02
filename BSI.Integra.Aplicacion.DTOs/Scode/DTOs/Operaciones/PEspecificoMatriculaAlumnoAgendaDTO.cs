using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PEspecificoMatriculaAlumnoAgendaDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecifico { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string TipoMatricula { get; set; }
        public bool Estado { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string Promedio { get; set; }
        public int TipoPrograma { get; set; }

    }
}
