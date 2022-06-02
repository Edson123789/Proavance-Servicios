using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConjuntoAnuncioTipoObjetivo
    {
        public TConjuntoAnuncioTipoObjetivo()
        {
            TConjuntoAnuncio = new HashSet<TConjuntoAnuncio>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdConjuntoAnuncioFuente { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TConjuntoAnuncioFuente IdConjuntoAnuncioFuenteNavigation { get; set; }
        public virtual ICollection<TConjuntoAnuncio> TConjuntoAnuncio { get; set; }
    }
}
