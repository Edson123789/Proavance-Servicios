using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPublicidadWeb
    {
        public TPublicidadWeb()
        {
            TPublicidadWebFormulario = new HashSet<TPublicidadWebFormulario>();
            TPublicidadWebPrograma = new HashSet<TPublicidadWebPrograma>();
        }

        public int Id { get; set; }
        public int IdTipoPublicidadWeb { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool Popup { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Tiempo { get; set; }
        public int? IdChatZoopim { get; set; }
        public int? IdPespecifico { get; set; }
        public string UrlImagen { get; set; }
        public string UrlBrochure { get; set; }
        public string UrlVideo { get; set; }
        public bool? EsRegistroAdicional { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TPublicidadWebFormulario> TPublicidadWebFormulario { get; set; }
        public virtual ICollection<TPublicidadWebPrograma> TPublicidadWebPrograma { get; set; }
    }
}
