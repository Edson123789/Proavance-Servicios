using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TInteraccionPortalPaginaFormularioCampo
    {
        public int Id { get; set; }
        public int IdInteraccionPortalPaginaFormulario { get; set; }
        public string Tag { get; set; }
        public string TagId { get; set; }
        public string Tipo { get; set; }
        public string Valor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
