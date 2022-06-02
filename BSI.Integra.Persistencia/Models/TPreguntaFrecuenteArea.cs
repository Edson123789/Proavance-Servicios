using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPreguntaFrecuenteArea
    {
        public int Id { get; set; }
        public int? IdPreguntaFrecuente { get; set; }
        public int IdArea { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TAreaCapacitacion IdAreaNavigation { get; set; }
        public virtual TPreguntaFrecuente IdPreguntaFrecuenteNavigation { get; set; }
    }
}
