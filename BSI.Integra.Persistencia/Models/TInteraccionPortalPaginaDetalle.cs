using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TInteraccionPortalPaginaDetalle
    {
        public int Id { get; set; }
        public int IdInteraccionPortalPagina { get; set; }
        public decimal Tiempo { get; set; }
        public int EsFiltro { get; set; }
        public int EsAccion { get; set; }
        public string Filtros { get; set; }
        public string Acciones { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
