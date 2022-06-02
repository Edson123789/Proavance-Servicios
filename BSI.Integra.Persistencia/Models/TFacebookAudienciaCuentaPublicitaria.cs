using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFacebookAudienciaCuentaPublicitaria
    {
        public int Id { get; set; }
        public int IdFacebookAudiencia { get; set; }
        public int IdFacebookCuentaPublicitaria { get; set; }
        public string Subtipo { get; set; }
        public string Origen { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdConjuntoListaDetalle { get; set; }
    }
}
