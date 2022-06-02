using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class MontoPagoBeneficiosDTO
	{
		public int? IdProgramaGeneral { get; set; }
		public int? Paquete { get; set; }
		public string NombrePaquete { get; set; }
		public string Beneficios { get; set; }
		public int? IdPais { get; set; }
		public int? IdMoneda { get; set; }
	}
}
