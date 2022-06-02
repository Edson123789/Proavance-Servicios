using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class LlamadaActividadDTO
    {
        public int? Id { get; set; }
        public int IdActividadDetalle { get; set; }
        public int IdAsesor { get; set; }
        public int IdLlamada { get; set; }
        public bool? EstadoProgramado { get; set; }
        public string Tag { get; set; }
        public DateTime? FechaInicioLlamada { get; set; }
        public DateTime? FechaFinLlamada { get; set; }
        public int? IdAgendaTab { get; set; }
        public bool? Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
