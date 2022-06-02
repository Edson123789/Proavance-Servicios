using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReportePendienteCobroCoordinadorDTO
    {
        public DateTime FechaVencimiento { get; set; }

        public string Periodo { get; set; }
        public string CentroCosto { get; set; }
        public string CoordinadoraCobranza { get; set; }
        public string Alumno { get; set; }
        public string CodigoMatricula { get; set; }
        public string Dni { get; set; }
        public string CodigoSemaforoActual { get; set; }
        public decimal SaldoPendienteDolares { get; set; }
		public int IdAlumno { get; set; }
		public int? IdSentinel { get; set; }
		public DateTime? FechaConsultaSentinel { get; set; }
		public string Documentacion { get; set; }
		public bool? Cronograma { get; set; }
		public bool? Convenio { get; set; }
		public bool? Pagare { get; set; }
		public bool? CartaAutorizacion { get; set; }
		public bool? HojaRequisitos { get; set; }
		public bool? OrdenCompra { get; set; }
		public bool? CartaCompromiso { get; set; }
		public bool? DocDNI { get; set; }
		public string Observaciones { get; set; }


	}
    public class ReportePendienteCobroAgrupadoDTO
    {
        public string Periodo { get; set; }
        public List<ReportePendienteCobroAgrupadoDetalleFechaDTO> DetalleFecha { get; set; }
    }

    public class ReportePendienteCobroAgrupadoDetalleFechaDTO
    {
        public string CentroCosto { get; set; }
        public string CoordinadoraCobranza { get; set; }
        public string Alumno { get; set; }
        public string CodigoMatricula { get; set; }
        public string Dni { get; set; }
        public decimal SaldoPendienteDolares { get; set; }
        public string CodigoSemaforoActual { get; set; }
		public string ColorSemaforo { get; set; }
        public int IdAlumno { get; set; }
		public int? IdSentinel { get; set; }
		public int? TiempoSentinel { get; set; }
		public string Documentos { get; set; }
		public int Cronograma { get; set; }
		public int Convenio { get; set; }
		public int Pagare { get; set; }
		public int CartaAutorizacion { get; set; }
		public int HojaRequisitos { get; set; }
		public int OrdenCompra { get; set; }
		public int CartaCompromiso { get; set; }
		public int DocDNI { get; set; }
		public string Observaciones { get; set; }
	}
}
