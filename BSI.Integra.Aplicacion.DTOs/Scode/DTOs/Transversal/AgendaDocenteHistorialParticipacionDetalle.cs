using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AgendaDocenteHistorialParticipacionDetalle
    {
        public int IdOportunidadHijo { get; set; }
        public string Programa { get; set; }
        public string Curso { get; set; }
        public string Modalidad { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public string Ciudad { get; set; }
        public string EstadoP { get; set; }
        public string Actividad { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaReal { get; set; }
        public string Comentario { get; set; }
    }
}
