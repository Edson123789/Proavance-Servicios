using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEstructuraEspecificaEncuesta
    {
        public int Id { get; set; }
        public int IdEstructuraEspecifica { get; set; }
        public int IdEncuesta { get; set; }
        public string NombreEncuesta { get; set; }
        public int OrdenCapitulo { get; set; }
        public int IdDocumentoSeccionPw { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
