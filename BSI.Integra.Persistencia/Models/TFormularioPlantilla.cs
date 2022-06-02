using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFormularioPlantilla
    {
        public TFormularioPlantilla()
        {
            TConjuntoAnuncio = new HashSet<TConjuntoAnuncio>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdFormularioSolicitud { get; set; }
        public int? IdFormularioLandingPage { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TFormularioLandingPage IdFormularioLandingPageNavigation { get; set; }
        public virtual TFormularioSolicitud IdFormularioSolicitudNavigation { get; set; }
        public virtual ICollection<TConjuntoAnuncio> TConjuntoAnuncio { get; set; }
    }
}
