using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEventoCalendarioProyectoPersonal
    {
        public int Id { get; set; }
        public int IdEventoCalendarioProyecto { get; set; }
        public int IdPersonalRecurso { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TEventoCalendarioProyecto IdEventoCalendarioProyectoNavigation { get; set; }
        public virtual TPersonalRecurso IdPersonalRecursoNavigation { get; set; }
    }
}
