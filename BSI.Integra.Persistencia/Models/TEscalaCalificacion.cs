using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEscalaCalificacion
    {
        public TEscalaCalificacion()
        {
            TEscalaCalificacionDetalle = new HashSet<TEscalaCalificacionDetalle>();
            TParametroEvaluacion = new HashSet<TParametroEvaluacion>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TEscalaCalificacionDetalle> TEscalaCalificacionDetalle { get; set; }
        public virtual ICollection<TParametroEvaluacion> TParametroEvaluacion { get; set; }
    }
}
