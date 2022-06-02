using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ChatIntegraHistorialAsesorDTO
	{
		public int Id { get; set; }
		public int IdAsesorChatDetalle { get; set; }
		public int? IdPersonal { get; set; }
		public DateTime FechaAsignacion { get; set; }
	}
}
