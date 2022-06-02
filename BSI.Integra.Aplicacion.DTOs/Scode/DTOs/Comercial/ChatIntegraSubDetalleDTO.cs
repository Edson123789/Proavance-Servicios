using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ChatIntegraSubDetalleDTO
	{
		public string Asesor { get; set; }
		public string Area { get; set; }
		public int Oportunidades { get; set; }
		public int Chats { get; set; }
		public int Promedio { get; set; }
		public int PalabrasVisitante { get; set; }
		public int Logueados { get; set; }
		public int ClickEmpezar { get; set; }
		public int Atendidos { get; set; }
		public int NoAtendidos { get; set; }
		public decimal ClienteTiempoEspera { get; set; }
	}
}
