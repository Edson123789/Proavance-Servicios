using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteAmbienteDTO
    {
        public string Modalidad { get; set; }
        public string CentroCosto { get; set; }
        public string EstadoCentroCosto { get; set; }
        public string Curso { get; set; }
        public string Anio { get; set; }
        public string SemanaCalendario { get; set; }
        public string Fecha { get; set; }
        public string Horario { get; set; }
        public string Sede { get; set; }
        public string Aula { get; set; }
        public string NroSesión { get; set; }
        public string Docente { get; set; }
        public string Coordinador { get; set; }
        public string NroAmbientesProgramados { get; set; }
        public string NroAmbientesDisponibles { get; set; }
    }
}
