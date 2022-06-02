using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class THistoricoProductoProveedor
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }
        public decimal CostoMonedaOrigen { get; set; }
        public decimal CostoDolares { get; set; }
        public int IdMoneda { get; set; }
        public decimal Precio { get; set; }
        public decimal TipoCambio { get; set; }
        public int? IdCondicionPago { get; set; }
        public int IdCondicionTipoPago { get; set; }
        public int Version { get; set; }
        public string Observaciones { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
