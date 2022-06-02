using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TWebinar
    {
        public TWebinar()
        {
            TWebinarCentroCosto = new HashSet<TWebinarCentroCosto>();
            TWebinarDetalle = new HashSet<TWebinarDetalle>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreCursoCompleto { get; set; }
        public int IdExpositor { get; set; }
        public int? IdWebinarCategoriaConfirmacionAsistencia { get; set; }
        public int IdPersonal { get; set; }
        public int IdFrecuencia { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string LinkAulaVirtual { get; set; }
        public bool Activo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TExpositor IdExpositorNavigation { get; set; }
        public virtual TWebinarCategoriaConfirmacionAsistencia IdWebinarCategoriaConfirmacionAsistenciaNavigation { get; set; }
        public virtual ICollection<TWebinarCentroCosto> TWebinarCentroCosto { get; set; }
        public virtual ICollection<TWebinarDetalle> TWebinarDetalle { get; set; }
    }
}
