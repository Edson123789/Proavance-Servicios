using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TInteraccionPortalPagina
    {
        public int Id { get; set; }
        public int IdInteraccionPortal { get; set; }
        public string UrlAnterior { get; set; }
        public string UrlActual { get; set; }
        public decimal Tiempo { get; set; }
        public string Filtros { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
