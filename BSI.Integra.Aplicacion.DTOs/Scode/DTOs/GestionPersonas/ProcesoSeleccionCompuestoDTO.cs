using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ProcesoSeleccionCompuestoDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Codigo { get; set; }
		public List<ConvocatoriaPersonalDetalleDTO> DetalleConvocatoria { get; set; }
	}
	public class ProcesoSeleccionConvocatoriaDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Codigo { get; set; }
		public int? IdConvocatoriaPersonal { get; set; }
		public string CodigoConvocatoriaPersonal { get; set; }
	}

	public class ConvocatoriaPersonalDetalleDTO
	{
		public int? IdConvocatoriaPersonal { get; set; }
		public string CodigoConvocatoriaPersonal { get; set; }
		public int? UltimaSecuencia { get; set; }
	}
}
