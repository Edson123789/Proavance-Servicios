using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteTasaConversionHistoricaFiltroDTO
    {
        public List<int> areas { get; set; }
        public List<int> subareas { get; set; }
        public List<int> pgeneral { get; set; }
        public List<int> pespecifico { get; set; }
        public List<string> probabilidad { get; set; }
        public List<string> categoriaDato { get; set; }
        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }
        public DateTime? fechaInicioTCH { get; set; }
        public DateTime? fechaFinTCH { get; set; }
        public Paginador paginador { get; set; }
        public GridFilters filter { get; set; }

    }
}
