using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPublicidadWebFormulario
    {
        public TPublicidadWebFormulario()
        {
            TPublicidadWebFormularioCampo = new HashSet<TPublicidadWebFormularioCampo>();
        }

        public int Id { get; set; }
        public int? IdPublicidadWeb { get; set; }
        public int IdFormularioSolicitudTextoBoton { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string TextoBoton { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TPublicidadWeb IdPublicidadWebNavigation { get; set; }
        public virtual ICollection<TPublicidadWebFormularioCampo> TPublicidadWebFormularioCampo { get; set; }
    }
}
