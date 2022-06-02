using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAnuncioFacebook
    {
        public TAnuncioFacebook()
        {
            TAnuncioFacebookMetrica = new HashSet<TAnuncioFacebookMetrica>();
            TOportunidad = new HashSet<TOportunidad>();
        }

        public int Id { get; set; }
        public string FacebookIdAnuncio { get; set; }
        public string FacebookNombreAnuncio { get; set; }
        public string FacebookIdConjuntoAnuncio { get; set; }
        public int? IdConjuntoAnuncioFacebook { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TConjuntoAnuncioFacebook IdConjuntoAnuncioFacebookNavigation { get; set; }
        public virtual ICollection<TAnuncioFacebookMetrica> TAnuncioFacebookMetrica { get; set; }
        public virtual ICollection<TOportunidad> TOportunidad { get; set; }
    }
}
