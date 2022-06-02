using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCampaniaFacebook
    {
        public TCampaniaFacebook()
        {
            TConjuntoAnuncioFacebook = new HashSet<TConjuntoAnuncioFacebook>();
        }

        public int Id { get; set; }
        public string FacebookIdCampania { get; set; }
        public string FacebookNombreCampania { get; set; }
        public string FacebookIdCuenta { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TConjuntoAnuncioFacebook> TConjuntoAnuncioFacebook { get; set; }
    }
}
