using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteAsistenciaOnlineDTO
    {
        public string NombreCentroCosto { get; set; }
        public string NombrePrograma { get; set; }
        public string NombreCurso { get; set; }
        public DateTime? FechaSesion { get; set; }
        public string Asistio { get; set; }
    }
}
