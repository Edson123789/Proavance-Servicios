using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ChatAgrupadoDTO
	{
		public string Pais { get; set; }
		public DateTime Fecha { get; set; }
		public List<ChatIntegraDetalleDTO> Detalle { get; set; }
	}
}
