using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteRN2FiltroDTO
    {
        public List<int> areas { get; set; }
        public List<int> subareas { get; set; }
        public List<int> pgeneral { get; set; }
        public List<int> pespecifico { get; set; }
        public List<string> modalidades { get; set; }
        public List<string> ciudades { get; set; }
        public List<int> categoriaOrigen { get; set; }
        public string anio { get; set; }
        public List<string> faseMaxima { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
