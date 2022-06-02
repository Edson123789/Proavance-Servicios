using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ChatIntegraDetalleDTO
	{
		public string Fecha { get; set; }
		public List<ChatIntegraSubDetalleDTO> Detalle { get; set; }
	}
}
