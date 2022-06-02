using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroReporteEmbudoDTO
    {
        public DateTime FechaInicio { get; set;}
        public DateTime FechaFin { get; set;}
        public List<FiltroValorEmbudoDTO> ListaArea { get; set; }
        public List<FiltroValorEmbudoDTO> ListaSubArea { get; set; }
        public List<FiltroValorEmbudoDTO> ListaProgramaGeneral { get; set; }
        public List<FiltroValorEmbudoDTO> ListaPais { get; set; }
        public List<FiltroValorEmbudoDTO> ListaTipoCategoriaOrigen { get; set; }
    }
}
