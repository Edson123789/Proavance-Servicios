using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroScrapingPortalEmpleoDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin{ get; set; }
        public string Puesto { get; set; }
        public string Descripcion { get; set; }
        public List<int> IdScrapingPagina { get; set; }
        public int EstadoClasificacion { get; set; }
    }
}
