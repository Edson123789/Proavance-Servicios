using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSeccionPw
    {
        public TSeccionPw()
        {
            TConfigurarExamenPrograma = new HashSet<TConfigurarExamenPrograma>();
            TConfigurarExamenesEncuestasEstructura = new HashSet<TConfigurarExamenesEncuestasEstructura>();
            TDocumentoSeccionPw = new HashSet<TDocumentoSeccionPw>();
            TSeccionTipoDetallePw = new HashSet<TSeccionTipoDetallePw>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Contenido { get; set; }
        public int IdPlantillaPw { get; set; }
        public bool VisibleWeb { get; set; }
        public int ZonaWeb { get; set; }
        public int OrdenEeb { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdSeccionTipoContenido { get; set; }

        public virtual TPlantillaPw IdPlantillaPwNavigation { get; set; }
        public virtual TSeccionTipoContenidoPw IdSeccionTipoContenidoNavigation { get; set; }
        public virtual ICollection<TConfigurarExamenPrograma> TConfigurarExamenPrograma { get; set; }
        public virtual ICollection<TConfigurarExamenesEncuestasEstructura> TConfigurarExamenesEncuestasEstructura { get; set; }
        public virtual ICollection<TDocumentoSeccionPw> TDocumentoSeccionPw { get; set; }
        public virtual ICollection<TSeccionTipoDetallePw> TSeccionTipoDetallePw { get; set; }
    }
}
