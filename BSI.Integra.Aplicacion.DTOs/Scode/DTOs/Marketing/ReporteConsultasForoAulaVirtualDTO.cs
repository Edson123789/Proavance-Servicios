using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteConsultasForoAulaVirtualDTO
    {
        public string Programa { get; set; }
        public string Curso { get; set; }
        public string Docente { get; set; }
        public string CodigoMatricula { get; set; }
        public string Alumno { get; set; }
        public string Tema { get; set; }
        public string Consulta { get; set; }
        public string FechaConsulta { get; set; }
        public string HoraConsulta { get; set; }
        public string Respuesta { get; set; }
        public string FechaRespuesta { get; set; }
        public string HoraRespuesta { get; set; }

    }
    public class ReporteConsultasForoFiltroDTO
    {
        public List<int> Programa { get; set; }
        public List<int> Docente { get; set; }
        public List<int> Curso { get; set; }

        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }

    }
}
