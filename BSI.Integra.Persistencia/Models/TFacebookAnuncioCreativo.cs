using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFacebookAnuncioCreativo
    {
        public int Id { get; set; }
        public string FacebookId { get; set; }
        public int IdPaginaFacebook { get; set; }
        public string TipoObjetivo { get; set; }
        public string Mensaje { get; set; }
        public int IdFacebookCuentaPublicitaria { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
