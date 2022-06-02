using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProductoHistoricoDTO
    {
        public HistoricoProductoProveedorVersionDTO Historico { get; set; }
        public ProductoDTO Producto { get; set; }
    }
}
