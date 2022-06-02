using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ReporteChatWhatsAppDTO
	{
		public string Pais { get; set; }
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
	public class ReporteChatWhatsAppAgrupadoDTO
	{
		public string Pais { get; set; }
		public List<ReporteChatWhatsAppAgrupadoDetallePaisDTO> DetallePais { get; set; }

	}
	public class ReporteChatWhatsAppAgrupadoDetallePaisDTO
	{
		public string Fecha { get; set; }
		public List<ReporteChatWhatsAppAgrupadoDetalleFechaDTO> DetalleFecha { get; set; }
	}

	public class ReporteChatWhatsAppAgrupadoDetalleFechaDTO
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
