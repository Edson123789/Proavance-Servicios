using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProductoFurDTO
    {
        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public string CuentaContable { get; set; }
        public string Cuenta { get; set; }
        public int IdCantidad { get; set; }
        public string Cantidad { get; set; }
        public int IdMoneda { get; set; }
        public decimal CostoOriginal { get; set; }
        public decimal CostoDolares { get; set; }
        public decimal PrecioProducto { get; set; }
        public int IdCondicionTipoPago{ get; set; }
    }
}
