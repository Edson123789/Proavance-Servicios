using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMailChimpListaId
    {
        public int Id { get; set; }
        public int? IdCampaniaMailingLista { get; set; }
        public string AsuntoLista { get; set; }
        public string IdCampaniaMailchimp { get; set; }
        public string IdListaMailchimp { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
