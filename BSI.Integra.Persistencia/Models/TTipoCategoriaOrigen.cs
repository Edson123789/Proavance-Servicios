using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TTipoCategoriaOrigen
    {
        public TTipoCategoriaOrigen()
        {
            TAsesorTipoCategoriaOrigenDetalle = new HashSet<TAsesorTipoCategoriaOrigenDetalle>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Meta { get; set; }
        public int? Orden { get; set; }
        public int OportunidadMaxima { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TAsesorTipoCategoriaOrigenDetalle> TAsesorTipoCategoriaOrigenDetalle { get; set; }
    }
}
