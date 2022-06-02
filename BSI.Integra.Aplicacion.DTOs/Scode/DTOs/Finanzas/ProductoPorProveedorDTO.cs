using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProductoPorProveedorDTO
    {
        public int Id { get; set; }
        public int IdHistoricoProveedorProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int IdMoneda { get; set; }
    }
}
