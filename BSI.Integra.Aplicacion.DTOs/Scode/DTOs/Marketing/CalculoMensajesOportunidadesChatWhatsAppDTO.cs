using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class CalculoMensajesOportunidadesChatWhatsAppDTO
	{
		public int? IdOportunidad { get; set; }
		public int IdPersonal { get; set; }
		public int? IdAlumno { get; set; }
		public string Numero { get; set; }
		public DateTime FechaMensaje { get; set; }
		public int IdPais { get; set; }
		public string FechaCreacionOportunidad { get; set; }
		public int? IdFaseOportunidad { get; set; }
		public int? TipoOportunidad { get; set; }
		public string Remitente { get; set; }
	}
	public class DesagregadoWhatsAppDiarioDTO
	{
		public string Numero { get; set; }
		public List<DesagregadoPorOportunidad> DetalleNumero { get; set; }
	}
	public class DesagregadoPorOportunidad
	{
		public int? IdOportunidad { get; set; }
		public List<ChatWhatsAppDesagregadoPorNumeroDiarioDTO> DetalleOportunidad { get; set; }
	}
	public class ChatWhatsAppDesagregadoPorNumeroDiarioDTO
	{
		public int IdPersonal { get; set; }
		public int? IdAlumno { get; set; }
		public DateTime FechaMensaje { get; set; }
		public int IdPais { get; set; }
		public string FechaCreacionOportunidad { get; set; }
		public int? IdFaseOportunidad { get; set; }
		public int? TipoOportunidad { get; set; }
		public string Remitente { get; set; }
	}
}
