using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class FiltrosChatWhatsAppReporteDTO
	{
		public DateTime FechaInicio { get; set; }
		public DateTime FechaFin { get; set; }
		public string Asesor { get; set; }
		public string Pais { get; set; }
		public int Desglose { get; set; }
	}
}
