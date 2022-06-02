using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteAsistenciaOnlineTempDTO
    {
        public string NombreCentroCosto { get;set; }
        public string NombreCurso { get; set; }
        public DateTime? HoraInicio { get; set; }
        public string NombreProgramaEspecifico { get; set; }
        public bool? Asistio { get; set; }
    }
}
