using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TTableroComercialCategoriaAsesor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal MontoVenta { get; set; }
        public int IdMonedaVenta { get; set; }
        public int IdTableroComercialUnidadVenta { get; set; }
        public decimal MontoPremio { get; set; }
        public int IdMonedaPremio { get; set; }
        public bool VisualizarMonedaLocal { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
