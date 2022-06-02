using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AgendaDocenteActividadDetalleDTO
    {
        public string Actividad { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaReal { get; set; }
        public string Comentario { get; set; }

    }
}
