using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class MontoPagoProgramaDTO
	{
		public int Id { get; set; }
		public double Precio { get; set; }
		public string PrecioLetras { get; set; }
		public int IdMoneda { get; set; }
		public string SimboloMoneda { get; set; }
		public double Matricula { get; set; }
		public double? Cuotas { get; set; }
		public int? NroCuotas { get; set; }
		public int IdPrograma { get; set; }
		public string NombrePrograma { get; set; }
		public string DuracionPrograma { get; set; }
		public int? IdTipoPago { get; set; }
		public string TipoPago { get; set; }
		public int? IdPais { get; set; }
		public string Descripcion { get; set; }
		public bool? VisibleWeb { get; set; }
		public int? Paquete { get; set; }
		public int? IdArea { get; set; }
		public int? IdSubArea { get; set; }
	}

	public class MontoProgramaAgrupadoDTO
	{
		public int IdPrograma { get; set; }
		public string NombrePrograma { get; set; }
		public int? IdArea { get; set; }
		public int? IdSubArea { get; set; }
		public string Duracion { get; set; }
		public ProgramaGeneralSeccionDocumentoDTO SeccionCertificadoV2 { get; set; }
		public SeccionDocumentoDTO SeccionCertificadoV1 { get; set; }
		public List<MontoProgramaDetalleDTO> MontoDetalle { get; set; }
	}

	public class MontoProgramaDetalleDTO
	{
		public string Version { get; set; }
		public List<MontoProgramaVersionDetalle> VersionDetalle { get; set; }
	}

	public class MontoProgramaVersionDetalle
	{
		public int? IdTipoPago { get; set; }
		public string TipoPago { get; set; }
		public string SimboloMoneda { get; set; }
		public double Matricula { get; set; }
		public double? Cuotas { get; set; }
		public int? NroCuotas { get; set; }
	}

	public class ResumenProgramaV2DTO
	{
		public int? IdArea { get; set; }
		public int? IdSubArea { get; set; }
		public string NombrePrograma { get; set; }
		public string Duracion { get; set; }
		public string Inversion { get; set; }
		public string Certificacion { get; set; }
	}
}
