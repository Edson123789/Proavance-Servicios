using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TWebinarDetalle
    {
        public TWebinarDetalle()
        {
            TWebinarAsistencia = new HashSet<TWebinarAsistencia>();
        }

        public int Id { get; set; }
        public int IdWebinar { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Grupo { get; set; }
        public string Link { get; set; }
        public bool EsCancelado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TWebinar IdWebinarNavigation { get; set; }
        public virtual ICollection<TWebinarAsistencia> TWebinarAsistencia { get; set; }
    }
}
