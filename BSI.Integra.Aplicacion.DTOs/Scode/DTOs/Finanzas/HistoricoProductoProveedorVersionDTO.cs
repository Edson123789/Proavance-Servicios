using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class HistoricoProductoProveedorVersionDTO
    {
        public int Id { get; set; }
        public string Producto { get; set; }
        public int IdProducto { get; set; }
        public string Proveedor { get; set; }
        public int IdProveedor { get; set; }
        public int? IdCondicionPago { get; set; }
        public string CondicionPago { get; set; }
        public string Moneda { get; set; }
        public int IdMoneda { get; set; }
        public decimal Precio { get; set; }
        public int IdTipoPago { get; set; }
        public string TipoPago { get; set; }
        public string Observaciones { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
    }
}
