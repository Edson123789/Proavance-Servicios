using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ReporteChatMessengerDTO
	{
		public DateTime Fecha { get; set; }
		public string Asesor { get; set; }
		public int NumeroOportunidadesSeguimiento { get; set; }
		public int NumeroOportunidadesGeneradas { get; set; }
		public int NumeroChatsNuevosSinOportunidad { get; set; }
		public int NumeroChats { get; set; }
		public int NumeroChatsAtendidos { get; set; }
		public int NumeroChatsAtendidoSinOportunidadSeguimiento { get; set; }
		public int NumeroChatsAtendidoOportunidadSeguimiento { get; set; }
		public int NumeroChatsAtendidoOportunidadGenerada { get; set; }
		public decimal PromedioRespuestaUsuario { get; set; }
		public int PalabrasVisitante { get; set; }
	}

	public class ReporteChatMessengerAgrupadoDTO
	{
		public string Fecha { get; set; }
		public List<ReporteChatMessengerAgrupadoDetalleFechaDTO> DetalleFecha { get; set; }
	}

	public class ReporteChatMessengerAgrupadoDetalleFechaDTO
	{
		public string Asesor { get; set; }
		public int NumeroOportunidadesSeguimiento { get; set; }
		public int NumeroOportunidadesGeneradas { get; set; }
		public int NumeroChatsNuevosSinOportunidad { get; set; }
		public int NumeroChats { get; set; }
		public int NumeroChatsAtendidos { get; set; }
		public int NumeroChatsAtendidoSinOportunidadSeguimiento { get; set; }
		public int NumeroChatsAtendidoOportunidadSeguimiento { get; set; }
		public int NumeroChatsAtendidoOportunidadGenerada { get; set; }
		public decimal PromedioRespuestaUsuario { get; set; }
		public int PalabrasVisitante { get; set; }
	}
}
