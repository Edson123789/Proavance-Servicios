using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteAnalyticsFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public List<ReporteAnalyticsFiltroDetalleDTO> Detalle { get; set; }
    }
}
