using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAnuncio
    {
        public TAnuncio()
        {
            TAnuncioElemento = new HashSet<TAnuncioElemento>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string IdAnuncioFacebook { get; set; }
        public int IdConjuntoAnuncio { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdCreativoPublicidad { get; set; }
        public string EnlaceFormulario { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? NroAnuncioCorrelativo { get; set; }

        public virtual TConjuntoAnuncio IdConjuntoAnuncioNavigation { get; set; }
        public virtual TCreativoPublicidad IdCreativoPublicidadNavigation { get; set; }
        public virtual ICollection<TAnuncioElemento> TAnuncioElemento { get; set; }
    }
}
