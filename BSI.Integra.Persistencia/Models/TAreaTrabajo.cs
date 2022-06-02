using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAreaTrabajo
    {
        public TAreaTrabajo()
        {
            TModeloGeneralAtrabajo = new HashSet<TModeloGeneralAtrabajo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TModeloGeneralAtrabajo> TModeloGeneralAtrabajo { get; set; }
    }
}
