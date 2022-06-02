using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEventoCalendarioProyecto
    {
        public TEventoCalendarioProyecto()
        {
            TEventoCalendarioProyectoPersonal = new HashSet<TEventoCalendarioProyectoPersonal>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public DateTime FechaEvento { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TEventoCalendarioProyectoPersonal> TEventoCalendarioProyectoPersonal { get; set; }
    }
}
