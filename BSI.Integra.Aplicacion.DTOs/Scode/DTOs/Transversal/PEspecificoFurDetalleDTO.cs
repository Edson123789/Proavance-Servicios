using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PEspecificoFurDetalleDTO
    {
        public int IdPEspecifico { get; set; }
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }
        public decimal? Monto{ get; set; }
        public decimal? Cantidad { get; set; }
    }
}
