using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSemaforoFinancieroDetalleVariable
    {
        public int Id { get; set; }
        public int IdSemaforoFinancieroDetalle { get; set; }
        public int IdSemaforoFinancieroVariable { get; set; }
        public decimal? ValorMinimo { get; set; }
        public int? IdMoneda { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public decimal? ValorMaximo { get; set; }
    }
}
