using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProductoCuentaContableDTO
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public long CuentaEspecifica { get; set; }
        public int IdProductoPresentacion { get; set; }
    }
}
