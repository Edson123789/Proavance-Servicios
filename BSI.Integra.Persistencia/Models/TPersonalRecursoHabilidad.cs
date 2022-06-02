using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPersonalRecursoHabilidad
    {
        public int Id { get; set; }
        public int IdPersonalRecurso { get; set; }
        public int IdHabilidadSimulador { get; set; }
        public int Puntaje { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual THabilidadSimulador IdHabilidadSimuladorNavigation { get; set; }
        public virtual TPersonalRecurso IdPersonalRecursoNavigation { get; set; }
    }
}
