using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class CalculoMensajesOportunidadesMessengerDTO
	{
		public int? IdPersonal { get; set; }
		public int IdMessengerUsuario { get; set; }
		public int? IdOportunidad { get; set; }
		public string Mensaje { get; set; }
		public string Remitente { get; set; }
		public DateTime FechaInteraccion { get; set; }
		public DateTime FechaChat { get; set; }
		public int? TipoOportunidad { get; set; }
		public int? IdAlumno { get; set; }
	}

	public class DesagregadoMessengerDiarioDTO
	{
		public int IdMessengerUsuario { get; set; }
		public List<DesagregadoPorOportunidadMessengerDTO> DetalleMessengerUsuario { get; set; }
	}
	public class DesagregadoPorOportunidadMessengerDTO
	{
		public int? IdOportunidad { get; set; }
		public List<ChatMessengerDesagregadoPorMessengerUsuarioDiarioDTO> DetalleOportunidad { get; set; }
	}
	public class ChatMessengerDesagregadoPorMessengerUsuarioDiarioDTO
	{
		public int? IdPersonal { get; set; }
		public string Mensaje { get; set; }
		public string Remitente { get; set; }
		public DateTime FechaInteraccion { get; set; }
		public DateTime FechaChat { get; set; }
		public int? TipoOportunidad { get; set; }
		public int? IdAlumno { get; set; }
	}
}
