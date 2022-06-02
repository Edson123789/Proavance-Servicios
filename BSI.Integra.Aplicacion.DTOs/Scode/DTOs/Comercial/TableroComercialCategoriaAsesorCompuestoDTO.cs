using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TableroComercialCategoriaAsesorCompuestoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal MontoVenta { get; set; }
        public int IdVisualizacionMonedaVenta { get; set; }
        public string VisualizacionMonedaVenta { get; set; }
        public decimal MontoPremio { get; set; }
        public bool VisualizacionMontoPremio { get; set; }
        public int IdMonedaVenta { get; set; }
        public string CodigoMonedaVenta { get; set; }
        public int IdMonedaPremio { get; set; }
        public string CodigoMonedaPremio { get; set; }
        public bool VisualizarMonedaLocal { get; set; }
    }
}