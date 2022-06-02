using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PreguntaFrecuenteFiltroDTO
    {
        public List<int> areas { get; set; }
        public List<int> subareas { get; set; }
        public List<int> pgenerales { get; set; }
        public List<int> tipos { get; set; }
        public string _areas { get; set; }
        public string _subareas { get; set; }
        public string _pgenerales { get; set; }
        public string _tipos { get; set; }
        public int pageSize { get; set; }
        public int skip { get; set; }
        public GridFiltersDTO FiltroKendo { get; set; }
    }
}
