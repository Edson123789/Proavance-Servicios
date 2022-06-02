using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReportePendienteCobroPorCoordinadorDTO
    {
        public DateTime PeriodoInicio { get; set; }
        public DateTime PeriodoFin { get; set; }
        public List<string> Coordinadora { get; set; }
        public List<string> Modalidad { get; set; }
        public List<int> AreaCapacitacion { get; set; }
        public List<int> SubAreaCapacitacion { get; set; }
        public List<int> ProgramaGeneral { get; set; }
		//public List<int> ProgramaEspecifico { get; set; }
		public List<int> CentroCosto { get; set; }
		public List<int> Sede { get; set; }
        public string ValorTipoSaldo { get; set; }
    }
}
