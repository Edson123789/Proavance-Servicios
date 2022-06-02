using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteEncuestaAulaVirtualDTO
    {
        public int Id { get; set; }
        public string IdPGeneral { get; set; }
        public string Programa { get; set; }
        public string IdPEspecifico { get; set; }
        public string Curso { get; set; }
        public string Docente { get; set; }
        public string CentroCosto { get; set; }
        public string AsistenteAC { get; set; }
        public string Fecha { get; set; }
        public string CodigoMatricula { get; set; }
        public string IdAlumno { get; set; }
        public string Alumno { get; set; }
        public string TipoEncuesta { get; set; }
    }

    public class ReporteEncuestaAulaVirtualFiltroDTO
    {
        public List<int> Programa { get; set; }
        public List<int> Curso { get; set; }
        public List<int> Docente { get; set; }
        public string Matricula { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }
        public List<string> AsistenteAC { get; set; }
        public int OrigenDato { get;set; }
        public int TipoEncuesta { get; set; }
    }
}
